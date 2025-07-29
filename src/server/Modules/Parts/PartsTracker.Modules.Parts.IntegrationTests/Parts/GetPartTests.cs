using FluentAssertions;
using PartsTracker.Modules.Parts.Application.Parts.GetPart;
using PartsTracker.Modules.Parts.Application.Parts.GetParts;
using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Modules.Parts.IntegrationTests.Abstractions;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.IntegrationTests.Parts;
public class GetPartTests : BaseIntegrationTest
{
    public GetPartTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenPartDoesNotExist()
    {
        // Arrange
        string partNumber = Faker.Random.AlphaNumeric(10);

        // Act
        Result<PartDto?> partResult = await Sender.Send(new GetPartQuery(partNumber));

        // Assert
        partResult.Error.Should().Be(PartErrors.NotFound(partNumber));
    }

    [Fact]
    public async Task Should_ReturnEmptyList_WhenNoPartsAvailable()
    {
        // Arrange
        await CleanDatabaseAsync();

        // Act
        Result<IEnumerable<PartDto>> partResult = await Sender.Send(new GetPartsQuery());

        // Assert
        partResult.IsSuccess.Should().BeTrue();
        partResult.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_ReturnList_WhenPartsAvailable()
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
        Result<IEnumerable<PartDto>> partResult = await Sender.Send(new GetPartsQuery());

        // Assert
        partResult.IsSuccess.Should().BeTrue();
        partResult.Value.Should().NotBeEmpty();
    }

}
