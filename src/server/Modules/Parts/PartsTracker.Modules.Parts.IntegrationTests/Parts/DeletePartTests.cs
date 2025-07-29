using System.Net;
using FluentAssertions;
using PartsTracker.Modules.Parts.IntegrationTests.Abstractions;

namespace PartsTracker.Modules.Parts.IntegrationTests.Parts;
public class DeletePartTests : BaseIntegrationTest
{
    public DeletePartTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenPartDoesNotExist()
    {
        // Arrange
        string partNumber = Faker.Random.AlphaNumeric(10);
        // Act
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/api/parts/{partNumber}");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_DeletePart_WhenPartExists()
    {
        // Arrange
        var part = Domain.Parts.Part.Create(
            Faker.Random.AlphaNumeric(10),
            Faker.Random.AlphaNumeric(150),
            Faker.Random.Int(0, 1000),
            Faker.Random.AlphaNumeric(5)
        );
        await DbContext.AddAsync(part);
        await DbContext.SaveChangesAsync();
        // Act
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/api/parts/{part.PartNumber}");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
