using PartsTracker.Shared.Application.Clock;

namespace PartsTracker.Shared.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
