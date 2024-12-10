namespace JobJuggler.Application;

public interface IUserAccessor
{
    string GetUsername();
    int GetUserId();
}