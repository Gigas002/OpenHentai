namespace OpenHentai.Tests.Integration;

public enum SqliteConnectionDesiredState
{
    OpenNew = 0,
    StayOpen = 1,
    Close = 2,
    Reopen = 3
}
