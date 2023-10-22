# OpenHentai

A work in progress on API for all upcoming **OpenHentai** projects (*if I'll write at least one until it's working lol*)

These incliudes:

- [DoujinDownloader](https://github.com/Gigas002/DoujinDownloader) -- will be renamed to something like `DoujinRepoOrganizer`, using the same API and stanards; download functions will be removed, but a downloaders-compatible `.txt`/`.json` output will still be available
- [WaisetsuToshokan](https://github.com/Gigas002/WaisetsuToshokan) -- an analogue of famous [MyAnimeList](https://myanimelist.net), but for doujins and NSFW stuff. Read more about the plans/resoning inside the corresponding repo

## Status

[![build-test-deploy](https://github.com/Gigas002/OpenHentai/actions/workflows/build-test-deploy.yml/badge.svg)](https://github.com/Gigas002/OpenHentai/actions/workflows/build-test-deploy.yml)

![Dependabot Status](https://flat.badgen.net/github/dependabot/Gigas002/OpenHentai)

[![codecov](https://codecov.io/github/Gigas002/OpenHentai/branch/master/graph/badge.svg)](https://codecov.io/github/Gigas002/OpenHentai)

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/0830b8500252474481805631e4984392)](https://app.codacy.com/gh/Gigas002/OpenHentai/dashboard)

## Releases

**Downloads count**

On Github: [![GitHub all releases](https://img.shields.io/github/downloads/Gigas002/OpenHentai/total)](https://github.com/Gigas002/OpenHentai/releases)

OpenHentai: [![Nuget](https://img.shields.io/nuget/dt/OpenHentai)](https://www.nuget.org/packages/OpenHentai/)

**Grab binaries**

GitHub Releases: [![Release](https://img.shields.io/github/release/Gigas002/OpenHentai.svg)](https://github.com/Gigas002/OpenHentai/releases/latest)

NuGet: [![NuGet](https://img.shields.io/nuget/v/OpenHentai.svg)](https://www.nuget.org/packages/OpenHentai/)

## Vulnerabilities

Build.props are [not yet supported](https://docs.snyk.io/guides/snyk-for-.net-developers#not-supported-in-.net) by snyk

## Contributing

Feel free to contribute. Right now the main tasks are following:

- Complete tagging enum: see `OpenHentai.Tags.TagCategory` and `OpenHentai.Tags.Tag` classes, descriptions
- Test and polish database-related stuff (`OpenHentai` library)
- Test and polish API (`OpenHentai.WebAPI` project)
- Improve tests coverage

After these are complete, new tasks will appear (e.g. searching through db, improving ci/cd and repo stuff, etc). After above tasks are done, we can start working on client app (`WaisetsuToshokan`) little by little and the first `preview` version of libraries can be pushed to nuget
