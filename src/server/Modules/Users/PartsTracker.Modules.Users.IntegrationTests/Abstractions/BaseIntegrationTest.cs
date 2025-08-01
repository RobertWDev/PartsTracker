﻿using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Bogus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PartsTracker.Modules.Users.Infrastructure.Database;
using PartsTracker.Modules.Users.Infrastructure.Identity;

namespace PartsTracker.Modules.Users.IntegrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
#pragma warning disable CA1515 // Consider making public types internal
public abstract class BaseIntegrationTest : IDisposable
#pragma warning restore CA1515 // Consider making public types internal
{
    protected static readonly Faker Faker = new();
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly UsersDbContext DbContext;
    protected readonly HttpClient HttpClient;
    private readonly KeyCloakOptions _options;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        HttpClient = factory.CreateClient();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        _options = factory.Services.GetRequiredService<IOptions<KeyCloakOptions>>().Value;
    }

    protected async Task CleanDatabaseAsync()
    {
        await DbContext.Database.ExecuteSqlRawAsync(
            """
            DELETE FROM users.inbox_message_consumers;
            DELETE FROM users.inbox_messages;
            DELETE FROM users.outbox_message_consumers;
            DELETE FROM users.outbox_messages;
            DELETE FROM users.users;
            DELETE FROM users.user_roles;
            """);
    }

    protected async Task<string> GetAccessTokenAsync(string email, string password)
    {
        using var client = new HttpClient();

        var authRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _options.PublicClientId),
            new("scope", "openid"),
            new("grant_type", "password"),
            new("username", email),
            new("password", password)
        };

        using var authRequestContent = new FormUrlEncodedContent(authRequestParameters);

        using var authRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_options.TokenUrl));
        authRequest.Content = authRequestContent;

        using HttpResponseMessage authorizationResponse = await client.SendAsync(authRequest);

        authorizationResponse.EnsureSuccessStatusCode();

        AuthToken authToken = await authorizationResponse.Content.ReadFromJsonAsync<AuthToken>();

        return authToken!.AccessToken;
    }

    internal sealed class AuthToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
