using NpgsqlTypes;

namespace JobJuggler.Domain.MetaModels.Enums;

public enum ProductType
{
    [PgName("subscription")] Subscription,
    [PgName("one_time")]  OneTime,
    [PgName("metered")]  Metered,
    [PgName("legacy")] Legacy
}