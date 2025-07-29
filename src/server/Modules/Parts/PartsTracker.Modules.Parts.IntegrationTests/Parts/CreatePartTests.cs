using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PartsTracker.Modules.Parts.IntegrationTests.Abstractions;

namespace PartsTracker.Modules.Parts.IntegrationTests.Parts;
public class CreatePartTests : BaseIntegrationTest
{
    public CreatePartTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    public static readonly TheoryData<string, string, int, string> InvalidRequests = new()
    {
        { "", Faker.Random.AlphaNumeric(150), Faker.Random.Int(0, 1000), Faker.Random.AlphaNumeric(5) },
        { Faker.Random.AlphaNumeric(10), "", Faker.Random.Int(0, 1000), Faker.Random.AlphaNumeric(5) },
        { Faker.Random.AlphaNumeric(10), Faker.Random.AlphaNumeric(500), Faker.Random.Int(0, 1000), Faker.Random.AlphaNumeric(5) },
        { Faker.Random.AlphaNumeric(10), Faker.Random.AlphaNumeric(150), -1, Faker.Random.AlphaNumeric(5) },
        { Faker.Random.AlphaNumeric(10), Faker.Random.AlphaNumeric(150), Faker.Random.Int(0, 1000), Faker.Random.AlphaNumeric(500) }
    };


    [Theory]
    [MemberData(nameof(InvalidRequests))]
    public async Task Should_ReturnBadRequest_WhenRequestIsNotValid(
        string partNumber,
        string description,
        int quantity,
        string locationCode)
    {
        // Arrange
        var request = new
        {
            PartNumber = partNumber,
            Description = description,
            QuantityOnHand = quantity,
            LocationCode = locationCode
        };

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/parts", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenPartAlreadyExists()
    {
        // Arrange
        string partNumber = Faker.Random.AlphaNumeric(10);
        string description = Faker.Random.AlphaNumeric(150);
        int quantity = Faker.Random.Int(0, 1000);
        string locationCode = Faker.Random.AlphaNumeric(5);
        var existingPart = Domain.Parts.Part.Create(partNumber, description, quantity, locationCode);
        await DbContext.AddAsync(existingPart);
        await DbContext.SaveChangesAsync();
        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/parts", new
        {
            PartNumber = partNumber,
            Description = description,
            QuantityOnHand = quantity,
            LocationCode = locationCode
        });
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Should_CreatePart_WhenValidDataProvided()
    {
        // Arrange
        string partNumber = Faker.Random.AlphaNumeric(10);
        string description = Faker.Random.AlphaNumeric(150);
        int quantity = Faker.Random.Int(0, 1000);
        string locationCode = Faker.Random.AlphaNumeric(5);
        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/parts", new
        {
            PartNumber = partNumber,
            Description = description,
            QuantityOnHand = quantity,
            LocationCode = locationCode
        });
        // Assert
        response.EnsureSuccessStatusCode();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }
}
