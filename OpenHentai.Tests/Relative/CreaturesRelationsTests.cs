using OpenHentai.Creatures;
using OpenHentai.Relations;
using OpenHentai.Relative;

namespace OpenHentai.Tests.Relative;

public class CreaturesRelationsTests
{
    [Test]
    public void ConstructorTest()
    {
        var creature1Mock = new Mock<Creature>();
        var creature2Mock = new Mock<Creature>();

        var cr1 = new CreaturesRelations();
        var cr2 = new CreaturesRelations(creature1Mock.Object, creature2Mock.Object, CreatureRelations.Alternative);
    }

    [Test]
    public void PropertiesTest()
    {
        var creature1Mock = new Mock<Creature>();
        var creature2Mock = new Mock<Creature>();

        var cr = new CreaturesRelations
        {
            Origin = creature1Mock.Object,
            Related = creature2Mock.Object,
            Relation = CreatureRelations.Enemy
        };
    }
}