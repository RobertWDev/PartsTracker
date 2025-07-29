using System.Data.Common;
using Dapper;
using PartsTracker.Modules.Parts.Application.Parts.GetPart;
using PartsTracker.Shared.Application.Data;
using PartsTracker.Shared.Application.Messaging;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Application.Parts.GetParts;

internal sealed class GetPartsQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetPartsQuery, IEnumerable<PartDto>>
{
    public async Task<Result<IEnumerable<PartDto>>> Handle(GetPartsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT 
                 p.part_number AS {nameof(PartDto.PartNumber)},
                 p.description AS {nameof(PartDto.Description)},
                 p.quantity_on_hand AS {nameof(PartDto.QuantityOnHand)},
                 p.location_code AS {nameof(PartDto.LocationCode)},
                 p.last_stock_take AS {nameof(PartDto.LastStockTake)}
             FROM parts.parts p
             WHERE p.is_deleted = FALSE
             ORDER BY p.part_number
             """;

        List<PartDto> parts = (await connection.QueryAsync<PartDto>(sql, request)).AsList();

        return parts;
    }
}
