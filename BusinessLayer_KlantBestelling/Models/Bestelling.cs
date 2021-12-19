using BusinessLayer_KlantBestelling.ModelExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer_KlantBestelling {
    public class Bestelling {
        public Bestelling(int bestellingId, Klant klant, int aantal, int product) :this(klant,aantal,product) {
            ZetKlant(klant);
            ZetAantal(aantal);
            ZetProduct(product);
            ZetId(bestellingId);
        }

        public Bestelling(Klant klant, int aantal, int product) {
            ZetKlant(klant);
            ZetAantal(aantal);
            ZetProduct(product);
        }

        public int BestellingId { get; private set; }
        public Klant Klant { get; private set; }
        public int Aantal { get; private set; }
        public Product Product { get; private set; }


        public void ZetId(int BestellingId) {
            if (BestellingId <= 0) {
                throw new BestellingException("Het ID is ongeldig. Het moet 1 of meer zijn.");
            }
            this.BestellingId = BestellingId;
        }
        public void ZetProduct(int product) {
            if (!Enum.IsDefined(typeof(Product), (Product)product)) {
                throw new BestellingException("Product bestaat niet.");
            }
            this.Product = (Product)product;
        }
        public void ZetAantal(int aantal) {
            if (aantal <= 1) {
                throw new BestellingException("Het aantal moet steeds groter zijn dan 1.");
            }
            Aantal = aantal;
        }
        public void ZetKlant(Klant klant) {
            if (klant == null) {
                throw new BestellingException("Klant is NULL.");
            }
            Klant = klant;
        }
    }
}
