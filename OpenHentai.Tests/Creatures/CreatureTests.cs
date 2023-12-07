using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Tags;

namespace OpenHentai.Tests.Creatures;

public class CreatureTests
{
    [Test]
    public void ConstructorTest()
    {
        var creature1 = new Creature();
        var creature2 = new Creature(2);

        var namedCreature1 = new Creature("default::name 1");

        var nameMock = new Mock<LanguageSpecificTextInfo>("default::name 2");
        var namedCreature2 = new Creature(nameMock.Object);
    }

    [Test]
    public void PropertiesTest()
    {
        var creature = new Creature
        {
            Id = 1,
            Birthday = DateTime.Now,
            Age = 500,
            Gender = Gender.Female
        };

        var namesMock = new Mock<CreaturesNames>();
        var descMock = new Mock<LanguageSpecificTextInfo>();
        var mediaMock = new Mock<MediaInfo>();
        var tagMock = new Mock<Tag>();
        var relationMock = new Mock<CreaturesRelations>();

        creature.Names.Add(namesMock.Object);
        creature.Description.Add(descMock.Object);
        creature.Media.Add(mediaMock.Object);
        creature.Tags.Add(tagMock.Object);
        creature.Relations.Add(relationMock.Object);
    }

    [Test]
    public void GetNamesTest()
    {
        var nameMock = new Mock<LanguageSpecificTextInfo>("default::name");
        var creature = new Creature(nameMock.Object);

        var names = creature.GetNames();

        var name = names.FirstOrDefault(t => t.Language == nameMock.Object.Language
                                            && t.Text == nameMock.Object.Text);

        if (name is null)
            Assert.Fail();
    }

    [Test]
    public void AddNamesTest()
    {
        var creature = new Creature();

        var nameMock = new Mock<LanguageSpecificTextInfo>("default::title");

        creature.AddNames(new List<LanguageSpecificTextInfo> { nameMock.Object });

        var title = creature.GetNames().FirstOrDefault(t => t.Language == nameMock.Object.Language
                                            && t.Text == nameMock.Object.Text);

        if (title is null)
            Assert.Fail();
    }

    [Test]
    public void AddNameTest()
    {
        var creature = new Creature();

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title1");

        creature.AddName(titleMock.Object);
        creature.AddName("default::title2");

        var names = creature.GetNames();

        if (names is null || !names.Any())
            Assert.Fail();
    }

    [Test]
    public void GetRelationsTest()
    {
        var creatureMock = new Mock<Creature>();
        var creature = new Creature();
        var creatureRelationsMock = new Mock<CreaturesRelations>(creatureMock.Object, creature, CreatureRelations.Friend);
        creature.Relations.Add(creatureRelationsMock.Object);

        var relations = creature.GetRelations();

        if (relations is null || relations.Count == 0)
            Assert.Fail();
    }

    [Test]
    public void AddRelationsTest()
    {
        var creatureMock = new Mock<Creature>();
        var relationsMock = new Mock<Dictionary<Creature, CreatureRelations>>();
        relationsMock.Object.Add(creatureMock.Object, CreatureRelations.Relative);

        var creature = new Creature();

        creature.AddRelations(relationsMock.Object);

        var relations = creature.GetRelations();

        if (relations is null || relations.Count == 0)
            Assert.Fail();
    }

    [Test]
    public void AddRelationTest()
    {
        var creature1 = new Creature(1);
        var creature2 = new Creature(2);
        var creatureRelation = new KeyValuePair<Creature, CreatureRelations>(creature1, CreatureRelations.Alternative);

        var creature = new Creature();

        creature.AddRelation(creatureRelation);
        creature.AddRelation(creature2, CreatureRelations.Enemy);

        var relations = creature.GetRelations();

        if (relations is null || relations.Count == 0)
            Assert.Fail();
    }
}
