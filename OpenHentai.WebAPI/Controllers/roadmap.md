# roadmap

File for tracking the progress on controllers API implementation

## Philosophy

Regarding strategy for updating database entries

`POST` methods are related to creating **new** entries or **overriding** current values, wiping previous ones completely

`PUT` methods are used **only to update** values, without overriding them

`PATCH` method is used **only to override basic** (*non-relative*) properties

Regarding usage of `object`s vs `id`s in queries (*example showing addition of `Circle` to `Author.Circles`):        

**objects**

client:

1. ask server for a `Circle` object from search or id (serv_req_count = 1, db_req_count = 1)
2. send `PUT` request to change `Author`, adding `Circle` (serv_req_count = 2, db_req_count = 2)

server:

`Author.Circles.Add(circle)` but in fact only `Circle.Id` is needed

**ids**

client:

1. ask server for a `Circle` object by search in case no id beforehand (serv_req_count = 1/0, db_req_count = 1/0)
2. send request to change `Author`, adding `Circle`, sending only `id` (serv_req_count = 2/1, db_req_count = 2/1)

server:

1. create new `Circle` using `id`
2. add this circle object to `Author.Circles` collection

These two are pretty much the same, but the second one doesn't send the whole `Circle` object, making query lighter. Generally, we don't need to send `object`s in cases, where we don't need them. When updating exsiting entries, sending only `id`s is enough

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

**GetAuthorCircles**

Path: `/id/circles`

Info: Get author circles

- [x] API
- [ ] Docs

**GetAuthorCreations**

Path: `/id/creations`

Info: Get author creations

- [x] API
- [ ] Docs

**GetCreatureNames**

Path: `/id/names`

Info: Get author's real names. Inherited from `ICreatureController`

- [x] API
- [ ] Docs

**GetCreatureTags**

Path: `/id/tags`

Info: Get author's tags. Inherited from `ICreatureController`

- [x] API
- [ ] Docs

**GetCreatureRelations**

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

**PostAuthorsCircles**

Path: `/id/circles`

Info: Creates a new link between **existing** `Author` and `Circle` objects by known `id`s in a `authors_circles` table. `Clear`s previous property value

- [x] API
- [ ] Docs

**PostAuthorsCreations**

Path: `/id/creations`

Info: Creates a new link between **existing** `Author` and `Creation` objects by known `id`s in a `authors_creations` table. `Clear`s previous property value

- [x] API
- [ ] Docs

**PostCreaturesNames**

Path: `/id/names`

Info: Creates and pushes new `CreaturesNames` object into corresponding table. `Clear`s previous property value

- [x] API
- [ ] Docs

**PostTags**

Path: `/id/tags`

Info: Creates a new link between **existing** `Author` and `Tag` objects by known `id`s in a `creatures_tags` table. `Clear`s previous property value

- [x] API
- [ ] Docs

**PostCreaturesRelations**

Path: `/id/relations`

Info: Creates a new link between two **existing** `Creature` objects by known `id`s in a `creatures_relations` table. `Clear`s previous property value (from perspective of `origin`)

- [ ] API
- [ ] Docs

#### PUT

**PutAuthorNames**

Path: `/id/author_names`

Info: Creates a new link between **existing** `Author` and `Name` objects by known `id`s in a `authors_names` table. Expands previous value

**Important**: since one name can have only **one** author, it **overrides** previously selected `Author`!

- [x] API
- [ ] Docs

**PutAuthorCircles**

Path: `/id/circles`

Info: Same as corresponding `POST` method, but doesn't override existing value, expanding it instead

- [x] API
- [ ] Docs

**PutAuthorCreations**

Path: `/id/creations`

Info: Same as corresponding `POST` method, but doesn't override existing value, expanding it instead

- [x] API
- [ ] Docs

**PutCreaturesNames**

Path: `/id/names`

Info: Creates a new link between **existing** `Creature` and `Name` objects by known `id`s in a `creatures_names` table. Expands previous value

**Important**: since one name can have only **one** creature, it **overrides** previously selected `Creature`!

- [ ] API
- [ ] Docs

**PutTags**

Path: `/id/tags`

Info: Same as corresponding `POST` method, but doesn't override existing value, expanding it instead

- [ ] API
- [ ] Docs

**PutCreaturesRelations**

Path: `/id/relations`

Info: Same as corresponding `POST` method, but doesn't override existing value, expanding it instead

- [ ] API
- [ ] Docs

#### PATCH

**PatchAuthor**

Path: `/id`

Info: Updates existing `Author` entry with data, specified in `json-patch` format. Theoretically can update **any** property of `Author`, but needs more testing, as `Client` (will merge into tests later) app grows

- [ ] API
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

**DeleteAuthorCircles**

Path: `/id/circles`

Info: Deletes the link between `Author` and `Circle` objects

- [x] API
- [ ] Docs

**DeleteAuthorsCreations**

Path: `/id/creations`

Info: Deletes the link between `Author` and `Creation` objects

- [ ] API
- [ ] Docs

**DeleteCreaturesNames**

Path: `/id/names`

Info: Deletes list of specified `name`s. Since `creatures_names` table is dependent on `creatures`, removing values from `Author.CreaturesNames` collection **will** also **purge** them from `creatures_names` table

- [ ] API
- [ ] Docs

**DeleteTags**

Path: `/id/tags`

Info: Deletes the link between `Author` and `Tag` objects

- [ ] API
- [ ] Docs

**DeleteCreaturesRelations**

Path: `/id/relations`

Info: Deletes the link between two `Creature` objects

- [ ] API
- [ ] Docs

### Circle

#### GET

#### POST

#### PUT

#### PATCH

#### DELETE

### Character

#### GET

#### POST

#### PUT

#### PATCH

#### DELETE

### Manga

#### GET

#### POST

#### PUT

#### PATCH

#### DELETE

### Tag

#### GET

#### POST

#### PUT

#### PATCH

#### DELETE