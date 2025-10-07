using NpgsqlTypes;

namespace JobJuggler.Domain.MetaModels;

public enum InvoiceStatus
{
    [PgName("pending")] Pending,
    [PgName("sent")] Sent,
    [PgName("paid")] Paid,
}