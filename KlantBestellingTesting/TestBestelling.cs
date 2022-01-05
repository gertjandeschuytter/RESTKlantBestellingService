using BusinessLayer_KlantBestelling;
using BusinessLayer_KlantBestelling.ModelExceptions;
using System;
using Xunit;

namespace Testing {
    public class TestBestelling {

        [Fact]
        public void Test_Id_Valid() {
            Bestelling bestelling = new Bestelling(5, new Klant(1, "Gertjan", "Coudeveldt 2, 8490 Varsenare"), 1, 2);
            bestelling.ZetId(1);
            Assert.Equal(1, bestelling.BestellingId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_Id_Invalid(int id) {
            Assert.Throws<BestellingException>(() => new Bestelling(id, new Klant(2, "Ludovic", "Zandstraat 489b"), 1, 2));
        }

        [Fact]
        public void Test_Aantal_Valid() {
            Bestelling bestelling = new Bestelling(1, new Klant(3, "Jean-Pierre", "Lantaarnstraat"), 5, 2);
            bestelling.ZetAantal(5);
            Assert.Equal(5, bestelling.Aantal);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Test_Aantal_Invalid(int aantal) {
            Assert.Throws<BestellingException>(() => new Bestelling(1, new Klant(1, "Sabine", "Krantenstraat 5"), aantal, 1));
        }
    }
}
