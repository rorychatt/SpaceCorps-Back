namespace SpaceCorps.Business.Db;

public enum DbErrorCode
{
    FatalError,
    Ok,
    UserAlreadyExists,
    UserCreated,
    UserNotFound,
    WrongPassword
}