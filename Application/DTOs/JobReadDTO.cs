namespace Application.DTOs;
public record JobReadDTO(
    int Id,
    Guid Guid,
    decimal Price,
    string? Notes,
    bool? IsCompleted,
    bool? IsCanceled,
    string? CancelReason,
    DateTime ScheduledDate,
    DateTime ScheduledArrivalStartDate,
    DateTime ScheduledArrivalEndDate,
    DateTime? StartedDate,
    DateTime? CompletedDate,
    DateTime? CanceledDate,
    ClientDTO Client,
    LocationDTO Location,
    InvoiceDTO Invoice
);