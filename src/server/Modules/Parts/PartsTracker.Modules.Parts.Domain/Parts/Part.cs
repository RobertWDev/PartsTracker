using System.ComponentModel.DataAnnotations;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Domain.Parts;

public class Part : Entity
{
    [Key]
    public string PartNumber { get; private set; }
    public string Description { get; private set; }
    public int QuantityOnHand { get; private set; }
    public string LocationCode { get; private set; }
    public DateTime? LastStockTake { get; private set; }
    public bool IsDeleted { get; private set; }

    private Part()
    {

    }

    public static Part Create(
        string partNumber,
        string description,
        int quantityOnHand,
        string locationCode)
    {
        var part = new Part
        {
            PartNumber = partNumber,
            Description = description,
            QuantityOnHand = quantityOnHand,
            LocationCode = locationCode,
            LastStockTake = null,
            IsDeleted = false
        };

        part.Raise(new PartCreatedDomainEvent(part.PartNumber));

        return part;
    }

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description) || Description.Equals(description, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }
        Description = description;
    }

    public void UpdateQuantityOnHand(int quantityOnHand)
    {
        if (quantityOnHand < 0 || QuantityOnHand == quantityOnHand)
        {
            return;
        }
        QuantityOnHand = quantityOnHand;
        Raise(new PartQuantityOnHandUpdatedDomainEvent(PartNumber, quantityOnHand));
    }

    public void UpdateLocationCode(string locationCode)
    {
        if (string.IsNullOrWhiteSpace(locationCode) || LocationCode.Equals(locationCode, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }
        LocationCode = locationCode;
        Raise(new PartLocationCodeUpdatedDomainEvent(PartNumber, locationCode));
    }

    public void UpdateLastStockTake(DateTime? lastStockTake)
    {
        if (lastStockTake.HasValue && LastStockTake.HasValue && LastStockTake.Value >= lastStockTake.Value)
        {
            return;
        }
        LastStockTake = lastStockTake;
    }

    public void MarkAsDeleted()
    {
        if (IsDeleted)
        {
            return;
        }
        IsDeleted = true;
        Raise(new PartDeletedDomainEvent(PartNumber));
    }
}
