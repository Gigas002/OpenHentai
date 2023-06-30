using Moq;
using OpenHentai.Circles;
using OpenHentai.Creations;

namespace OpenHentai.Tests.Integration;

public class CreationsCirclesTests : DatabaseTestsBase
{
    [Test]
    public void PushCreationsCirclesTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ymM1 = new Mock<Manga>("default::Monokemono Shoya");
        var ymM2 = new Mock<Manga>("default::Monokemono");
        var aM1 = new Mock<Manga>("default::VictimGirls 24");
        var aM2 = new Mock<Manga>("default::VictimGirls 25");

        var nnnt = new Mock<Circle>("default::noraneko-no-tama");
        var fatalpulse = new Mock<Circle>("default::Fatalpulse");

        ymM1.Object.Circles.Add(nnnt.Object);
        ymM2.Object.Circles.Add(nnnt.Object);
        fatalpulse.Object.Creations.Add(aM1.Object);
        fatalpulse.Object.Creations.Add(aM2.Object);

        db.SaveChanges();
    }
}
