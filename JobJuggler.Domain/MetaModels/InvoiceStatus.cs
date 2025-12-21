using NpgsqlTypes;

namespace JobJuggler.Domain.MetaModels;

public enum InvoiceStatus
{
    [PgName("none")] None,
    [PgName("pending")] Pending,
    [PgName("paid")] Paid,
    [PgName("failed")] Failed,
    [PgName("canceled")] Canceled
}