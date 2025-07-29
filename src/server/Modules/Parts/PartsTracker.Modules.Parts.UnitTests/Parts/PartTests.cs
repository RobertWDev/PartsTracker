using FluentAssertions;
using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Modules.Parts.UnitTests.Abstractions;

namespace PartsTracker.Modules.Parts.UnitTests.Parts;
public class PartTests : BaseTest
{
    [Fact]
    public void Create_ShouldReturnPart()
    {
        var part = Domain.Parts.Part.Create(
            Faker.Random.AlphaNumeric(10),
            Faker.Random.AlphaNumeric(150),
            Faker.Random.Int(0, 1000),
            Faker.Random.AlphaNumeric(5)
        );

        part.Should().NotBeNull();
    }

    [Fact]
    public void Create_ShouldRaiseDomainEvent_WhenPartCreated()
    {
        // Act
        var part = Domain.Parts.Part.Create(
            Faker.Random.AlphaNumeric(10),
            Faker.Random.AlphaNumeric(150),
            Faker.Random.Int(0, 1000),
            Faker.Random.AlphaNumeric(5)
        );

        // Assert
        PartCreatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<PartCreatedDomainEvent>(part);

        domainEvent.PartNumber.Should().Be(part.PartNumber);
    }

    [Fact]
    public void Update_ShouldRaiseDomainEvent_WhenPartLocationUpdated()
    {
        // Arrange
        var part = Domain.Parts.Part.Create(
            Faker.Random.AlphaNumeric(10),
            Faker.Random.AlphaNumeric(150),
            Faker.Random.Int(0, 1000),
            Faker.Random.AlphaNumeric(5)
        );

        // Act
        part.UpdateLocationCode(Faker.Random.AlphaNumeric(5));

        // Assert
        PartLocationCodeUpdatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<PartLocationCodeUpdatedDomainEvent>(part);

        domainEvent.PartNumber.Should().Be(part.PartNumber);
    }

    [Fact]
    public void Update_ShouldRaiseDomainEvent_WhenPartQuantityUpdated()
    {
        // Arrange
        var part = Domain.Parts.Part.Create(
            Faker.Random.AlphaNumeric(10),
            Faker.Random.AlphaNumeric(150),
            Faker.Random.Int(0, 1000),
            Faker.Random.AlphaNumeric(5)
        );

        // Act
        part.UpdateQuantityOnHand(Faker.Random.Int(0, 1000));

        // Assert
        PartQuantityOnHandUpdatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<PartQuantityOnHandUpdatedDomainEvent>(part);

        domainEvent.PartNumber.Should().Be(part.PartNumber);
    }
}
