using NetArchTest.Rules;
using PartsTracker.ArchitectureTests.Abstractions;
using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Modules.Parts.Infrastructure;
using PartsTracker.Modules.Users.Domain.Users;
using PartsTracker.Modules.Users.Infrastructure;
using System.Reflection;

namespace PartsTracker.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    [Fact]
    public void UsersModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [PartsNamespace];
        string[] integrationEventsModules = [PartsIntegrationEventsNamespace];

        List<Assembly> usersAssemblies =
        [
            typeof(User).Assembly,
            Modules.Users.Application.AssemblyReference.Assembly,
            Modules.Users.Presentation.AssemblyReference.Assembly,
            typeof(UsersModule).Assembly
        ];

        Types.InAssemblies(usersAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void PartsModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace];
        string[] integrationEventsModules = [UsersIntegrationEventsNamespace];

        List<Assembly> usersAssemblies =
        [
            typeof(Part).Assembly,
            Modules.Parts.Application.AssemblyReference.Assembly,
            Modules.Parts.Presentation.AssemblyReference.Assembly,
            typeof(PartsModule).Assembly
        ];

        Types.InAssemblies(usersAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }
}
