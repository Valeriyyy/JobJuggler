using NpgsqlTypes;

namespace JobJuggler.Domain.MetaModels.Enums;

public enum BillingPeriod
{
    [PgName("monthly")] Monthly,
    [PgName("yearly")] Yearly,
    [PgName("one_time")] OneTime
}