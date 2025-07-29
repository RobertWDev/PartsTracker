using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PartsTracker.Modules.Parts.IntegrationTests.Abstractions;

namespace PartsTracker.Modules.Parts.IntegrationTests.Parts;
public class UpdatePartTests : BaseIntegrationTest
{
    public UpdatePartTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    public static readonly TheoryData<string, string, int, string, DateTime?> InvalidRequests = new()
    {
        { "", Faker.Random.AlphaNumeric(150), Faker.Random.Int(0, 1000), Faker.Random.AlphaNumeric(5), null },
        { Faker.Random.AlphaNumeric(10), "", Faker.Random.Int(0, 1000), Faker.Random.AlphaNumeric(5), null },
        { Faker.Random.AlphaNumeric(10), Faker.Random.AlphaNumeric(500), Faker.Random.Int(0, 1000), Faker.Random.AlphaNumeric(5), null },
        { Faker.Random.AlphaNumeric(10), Faker.Random.AlphaNumeric(150), -1, Faker.Random.AlphaNumeric(5), null },
        { Faker.Random.AlphaNumeric(10), Faker.Random.AlphaNumeric(150), Faker.Random.Int(0, 1000), Faker.Random.AlphaNumeric(500), DateTime.Now.AddDays(2) }
    };

    [Theory]
    [MemberData(nameof(InvalidRequests))]
    public async Task Should_ReturnError_WhenRequestIsNotValid(
        string partNumber,
        string description,
        int quantity,
        string locationCode,
        DateTime? lastStockTake)
    {
        // Arrange
        var request = new
        {
            PartNumber = partNumber,
            Description = description,
            QuantityOnHand = quantity,
            LocationCode = locationCode,
            LastStockTake = lastStockTake
        };

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("/api/parts", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
