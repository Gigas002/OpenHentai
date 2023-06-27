# OpenHentai

A work in progress on API for all upcoming **OpenHentai** projects (*if I'll write at least one until it's working lol*)

These incliudes:

- [DoujinDownloader](https://github.com/Gigas002/DoujinDownloader) -- will be renamed to something like `DoujinRepoOrganizer`, using the same API and stanards; download functions will be removed, but a downloaders-compatible `.txt`/`.json` output will still be available
- [WaisetsuToshokan](https://github.com/Gigas002/WaisetsuToshokan) -- an analogue of famous [MyAnimeList](https://myanimelist.net), but for doujins and NSFW stuff. Read more about the plans/resoning inside the corresponding repo

## Contributing

Feel free to contribute. Right now the main tasks are following:

- Complete tagging enum: see `OpenHentai.Tags.TagCategory` and `OpenHentai.Tags.Tag` classes, descriptions
- Test and polish database-related stuff (`OpenHentai` library)
- Test and polish API (`OpenHentai.WebAPI` project)
- Improve tests coverage

After these are complete, new tasks will appear (e.g. searching through db, improving ci/cd and repo stuff, etc). After above tasks are done, we can start working on client app (`WaisetsuToshokan`) little by little and the first `preview` version of libraries can be pushed to nuget
