using OpenHentai.Creations;
using OpenHentai.Relations;
using OpenHentai.Relative;

namespace OpenHentai.Tests.Relative;

public class CreationsRelationsTests
{
    [Test]
    public void ConstructorTest()
    {
        var creation1Mock = new Mock<Creation>();
        var creation2Mock = new Mock<Creation>();

        var cr1 = new CreationsRelations();
        var cr2 = new CreationsRelations(creation1Mock.Object, creation2Mock.Object, CreationRelations.Alternative);
    }

    [Test]
    public void PropertiesTest()
    {
        var creation1Mock = new Mock<Creation>();
        var creation2Mock = new Mock<Creation>();

        var cr = new CreationsRelations
        {
            Origin = creation1Mock.Object,
            Related = creation2Mock.Object,
            Relation = CreationRelations.Slave
        };
    }
}