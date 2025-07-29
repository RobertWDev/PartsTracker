using Bogus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PartsTracker.Modules.Parts.Infrastructure.Database;

namespace PartsTracker.Modules.Parts.IntegrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
#pragma warning disable CA1515 // Consider making public types internal
public abstract class BaseIntegrationTest : IDisposable
#pragma warning restore CA1515 // Consider making public types internal
{
    protected static readonly Faker Faker = new();
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly PartsDbContext DbContext;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        HttpClient = factory.CreateClient();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<PartsDbContext>();
    }

    protected async Task CleanDatabaseAsync()
    {
        await DbContext.Database.ExecuteSqlRawAsync(
            """
            DELETE FROM parts.inbox_message_consumers;
            DELETE FROM parts.inbox_messages;
            DELETE FROM parts.outbox_message_consumers;
            DELETE FROM parts.outbox_messages;
            DELETE FROM parts.parts;
            """);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
