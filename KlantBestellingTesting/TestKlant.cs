using BusinessLayer_KlantBestelling;
using BusinessLayer_KlantBestelling.ModelExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Testing {
    public class TestKlant {
        [Fact]
        public void Test_Naam_Valid() {
            Klant kl = new Klant("Gertjan", "Vlaanderenstraat 152b");
            kl.ZetNaam("Gertjan");
            Assert.Equal("Gertjan", kl.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Test_Naam_Invalid(string naam) {
            Assert.Throws<KlantException>(() => new Klant(naam, "Teststraatlaan 290"));
        }

        [Fact]
        public void Test_Adres_Valid() {
            Klant kl = new Klant("Sander", "Kerkstraat 5");
            kl.ZetAdres("Kerkstraat 5");
            Assert.Equal("Kerkstraat 5", kl.Adres);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Mopstraat")]
        public void Test_Adres_Invalid(string adres) {
            Assert.Throws<KlantException>(() => new Klant("Jonas", adres));
        }
    }
}
