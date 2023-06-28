namespace OpenHentai.Tests.Integration;

public enum TestKind
{
    PushAuthors,
    PushCircles,
    PushManga,
    PushCharacters,
    PushTags,

    // depends on PushTags(1)
    PushTagsRelations,

    // depends on PushAuthors(1)
    PushAuthorsRelations,

    // depends on PushCharactersTest(1)
    PushCharactersRelations,

    // depends on PushManga(1)
    PushCreationsRelations,

    // depends on PushAuthors(1)
    // depends on PushCircles(1)
    PushAuthorsCircles,

    // depends on PushAuthors(1)
    // depends on PushManga(1)
    PushAuthorsCreations,

    // depends on PushCharacters(1)
    // depends on PushManga(1)
    PushCharactersCreations,

    // depends on PushTags(1)
    // depends on PushAuthors(1)
    PushAuthorsTags,

    // depends on PushTags(1)
    // depends on PushCharacters(1)
    PushCharactersTags,

    // depends on PushManga(1)
    // depends on PushCircles(1)
    PushCreationsCircles,

    // depends on PushManga(1)
    // depends on PushTags(1)
    PushCreationsTags,

    // depends on PushCircles(1)
    // depends on PushTags(1)
    PushCirclesTags,

    ReadAuthors,
    ReadCharacters,
    ReadCircles,
    ReadManga,
    ReadTags
}