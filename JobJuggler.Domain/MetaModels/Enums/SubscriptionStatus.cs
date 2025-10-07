using NpgsqlTypes;

namespace JobJuggler.Domain.MetaModels.Enums;

public enum SubscriptionStatus
{
    [PgName("active")] Active,
    [PgName("inactive")] Inactive,
    [PgName("past_due")] PastDue,
    [PgName("canceled")] Canceled,
    [PgName("terminated")] Terminated,
}