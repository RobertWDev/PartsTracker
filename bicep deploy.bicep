param location string = 'southafricanorth'
param postgresAdmin string = 'ptadmin'
@secure()
param postgresPassword string
param postgresDbName string = 'partstrackerdb'

param acrName string = 'ptacr${uniqueString(resourceGroup().id)}'
param apiImage string = 'partstracker-api:latest'
param webImage string = 'partstracker-web:latest'

resource acr 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: acrName
  location: location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
  }
}

resource postgres 'Microsoft.DBforPostgreSQL/flexibleServers@2022-12-01' = {
  name: 'pt-postgres'
  location: location
  properties: {
    administratorLogin: postgresAdmin
    administratorLoginPassword: postgresPassword
    version: '16'
    storage: {
      storageSizeGB: 32
    }
    network: {
      publicNetworkAccess: 'Enabled'
    }
    highAvailability: {
      mode: 'Disabled'
    }
    backup: {
      backupRetentionDays: 7
      geoRedundantBackup: 'Disabled'
    }
    createMode: 'Default'
    availabilityZone: '1'
    databaseName: postgresDbName
  }
  sku: {
    name: 'Standard_B1ms'
    tier: 'Burstable'
    capacity: 1
    family: 'B'
  }
}

resource postgresDb 'Microsoft.DBforPostgreSQL/flexibleServers/databases@2022-12-01' = {
  name: '${postgres.name}/${postgresDbName}'
  properties: {}
}

resource containerEnv 'Microsoft.App/managedEnvironments@2023-05-01' = {
  name: 'pt-container-env'
  location: location
  properties: {}
}

resource apiApp 'Microsoft.App/containerApps@2023-05-01' = {
  name: 'pt-api'
  location: location
  properties: {
    managedEnvironmentId: containerEnv.id
    configuration: {
      ingress: {
        external: true
        targetPort: 80
      }
      registries: [
        {
          server: acr.properties.loginServer
          username: acr.listCredentials().username
          passwordSecretRef: 'acr-password'
        }
      ]
      secrets: [
        {
          name: 'acr-password'
          value: acr.listCredentials().passwords[0].value
        }
        {
          name: 'postgres-connection'
          value: 'Host=${postgres.properties.fullyQualifiedDomainName};Database=${postgresDbName};Username=${postgresAdmin};Password=${postgresPassword};'
        }
      ]
      environmentVariables: [
        {
          name: 'ConnectionStrings__Database'
          value: 'Host=${postgres.properties.fullyQualifiedDomainName};Database=${postgresDbName};Username=${postgresAdmin};Password=${postgresPassword};'
        }
      ]
    }
    template: {
      containers: [
        {
          image: '${acr.properties.loginServer}/${apiImage}'
          name: 'api'
          resources: {
            cpu: 0.5
            memory: '1Gi'
          }
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 5
        rules: [
          {
            name: 'http-scaling'
            custom: {
              type: 'http'
              metadata: {
                concurrentRequests: '50'
              }
            }
          }
        ]
      }
    }
  }
}

resource webApp 'Microsoft.App/containerApps@2023-05-01' = {
  name: 'pt-web'
  location: location
  properties: {
    managedEnvironmentId: containerEnv.id
    configuration: {
      ingress: {
        external: true
        targetPort: 80
      }
      registries: [
        {
          server: acr.properties.loginServer
          username: acr.listCredentials().username
          passwordSecretRef: 'acr-password'
        }
      ]
      secrets: [
        {
          name: 'acr-password'
          value: acr.listCredentials().passwords[0].value
        }
      ]
    }
    template: {
      containers: [
        {
          image: '${acr.properties.loginServer}/${webImage}'
          name: 'web'
          resources: {
            cpu: 0.25
            memory: '0.5Gi'
          }
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 3
        rules: [
          {
            name: 'http-scaling'
            custom: {
              type: 'http'
              metadata: {
                concurrentRequests: '30'
              }
            }
          }
        ]
      }
    }
  }
}