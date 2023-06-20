# roadmap

File for tracking the progress on controllers API implementation

**Included response data**

TODO: decide, what should be `Include()` by default

## Basic API implementation

### Author

Request root path: `/authors`

#### GET

**GetAuthors**

Path: `/`

Info: Get all authors

- [x] API
- [ ] Docs

**GetAuthor**

Path: `/id`

Info: Get author by id. Only basic properties are returned

- [x] API
- [ ] Docs

**GetAuthorsNames**

Path: `/authors_names`

Info: Get all authors names

- [x] API
- [ ] Docs

**GetAuthorNames**

Path: `/id/author_names`

Info: Get author names

- [x] API
- [ ] Docs

**GetCircles**

Path: `/id/circles`

Info: Get author circles

- [x] API
- [ ] Docs

**GetCreations**

Path: `/id/creations`

Info: Get author creations

- [x] API
- [ ] Docs

**GetNames**

Path: `/id/names`

Info: Get author's real names. Inherited from `ICreatureController`

- [x] API
- [ ] Docs

**GetTags**

Path: `/id/tags`

Info: Get author's tags. Inherited from `ICreatureController`

- [x] API
- [ ] Docs

**GetRelations**

Path: `/id/relations`

Info: Get author's relations. Inherited from `ICreatureController`

- [x] API
- [ ] Docs

#### POST

**PostAuthor**

Path: `/`

Info: Creates and pushes new `Author` object with basic properties

- [x] API
- [ ] Docs

**PostAuthorNames**

Path: `/id/author_names`

Info: Creates and pushes new `AuthorsNames` object into corresponding table. `Clear`s previous property value

- [x] API
- [ ] Docs

**PostNames**

Path: `/id/names`

Info: Creates and pushes new `CreaturesNames` object into corresponding table. `Clear`s previous property value

- [x] API
- [ ] Docs

#### PUT

**PutAuthorNames**

Path: `/id/author_names`

Info: Creates a new link between **existing** `Author` and `Name` objects by known `id`s in a `authors_names` table. Expands previous value

**Important**: since one name can have only **one** author, it **overrides** previously selected `Author`!

- [x] API
- [ ] Docs

**PutCircles**

Path: `/id/circles`

Info: Same as corresponding `POST` method, but doesn't override existing value, expanding it instead

- [x] API
- [ ] Docs

**PutCreations**

Path: `/id/creations`

Info: Same as corresponding `POST` method, but doesn't override existing value, expanding it instead

- [x] API
- [ ] Docs

**PutNames**

Path: `/id/names`

Info: Creates a new link between **existing** `Creature` and `Name` objects by known `id`s in a `creatures_names` table. Expands previous value

**Important**: since one name can have only **one** creature, it **overrides** previously selected `Creature`!

- [x] API
- [ ] Docs

**PutTags**

Path: `/id/tags`

Info: Same as corresponding `POST` method, but doesn't override existing value, expanding it instead

- [x] API
- [ ] Docs

**PutRelations**

Path: `/id/relations`

Info: Same as corresponding `POST` method, but doesn't override existing value, expanding it instead

- [x] API
- [ ] Docs

#### DELETE

**DeleteAuthor**

Path: `/id`

Info: Deletes the specified user entry from database. Removes corresponding depending values as well (e.g. names -- TODO: need testing)

- [x] API
- [ ] Docs

**DeleteAuthorNames**

Path: `/id/author_names`

Info: Deletes list of specified `author_name`s. Since `author_names` table is dependent on `authors`, removing values from `Author.AuthorNames` collection **will** also **purge** them from `author_names` table

- [x] API
- [ ] Docs

**DeleteCircles**

Path: `/id/circles`

Info: Deletes the link between `Author` and `Circle` objects

- [x] API
- [ ] Docs

**DeleteCreations**

Path: `/id/creations`

Info: Deletes the link between `Author` and `Creation` objects

- [x] API
- [ ] Docs

**DeleteNames**

Path: `/id/names`

Info: Deletes list of specified `name`s. Since `creatures_names` table is dependent on `creatures`, removing values from `Author.CreaturesNames` collection **will** also **purge** them from `creatures_names` table

- [x] API
- [ ] Docs

**DeleteTags**

Path: `/id/tags`

Info: Deletes the link between `Author` and `Tag` objects

- [x] API
- [ ] Docs

**DeleteRelations**

Path: `/id/relations`

Info: Deletes the link between two `Creature` objects

- [x] API
- [ ] Docs

#### PATCH

**PatchAuthor**

Path: `/id`

Info: Updates existing `Author` entry with data, specified in `json-patch` format. Theoretically can update **any** property of `Author`, but needs more testing, as `Client` (will merge into tests later) app grows

- [x] API
- [ ] Docs

### Circle

#### GET

#### POST

#### PUT

#### DELETE

#### PATCH

### Character

#### GET

#### POST

#### PUT

#### DELETE

#### PATCH

### Manga

#### GET

#### POST

#### PUT

#### DELETE

#### PATCH

### Tag

#### GET

#### POST

#### PUT

#### DELETE

#### PATCH
