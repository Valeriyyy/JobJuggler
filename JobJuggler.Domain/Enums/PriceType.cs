using NpgsqlTypes;

namespace JobJuggler.Domain.Enums;

public enum PriceType {
    [PgName("none")] None,
    [PgName("per_unit")] PerUnit,
    [PgName("flat_rate")] FlatRate,
}
