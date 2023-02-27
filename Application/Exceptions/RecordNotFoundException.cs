namespace Application.Exceptions;
public class RecordNotFoundException : Exception {
    public Type? ClassType { get; set; }
    public int? Id { get; set; }
    public Guid? Guid { get; set; }
    public RecordNotFoundException() { }
    public RecordNotFoundException(string? message) : base(message) { }
    public RecordNotFoundException(string message, Exception inner) : base(message, inner) { }

    /// <summary>
    /// Throws a specific error message notifying that a record of a specific type with a 
    /// specific id was not found.
    /// </summary>
    /// <param name="classType"></param>
    /// <param name="id"></param>
    public RecordNotFoundException(Type classType, int id) : base($"No {classType.Name} record found with id {id}") { }

    /// <summary>
    /// Throws a specific error message notifying that a record of a specific type with a 
    /// specific id was not found.
    /// </summary>
    /// <param name="classType">ClassType</param>
    /// <param name="guid">Guid</param>
    public RecordNotFoundException(Type classType, Guid guid) : base($"No {classType.Name} record found with guid {guid}") { }
}
