using BusinessLayer_KlantBestelling.ModelExceptions;
using System;

namespace BusinessLayer_KlantBestelling {
    public class Klant {
        public string Name { get; private set; }
        public string Adres { get; private set; }
        public int KlantId { get; private set; }

        public Klant(int klantId, string name, string adres) : this(name, adres) {
            ZetKlantId(klantId);
        }
        public Klant(string name, string adres) {
            ZetNaam(name);
            ZetAdres(adres);
        }
        public void ZetKlantId(int klantId) {
            if (klantId <= 0) {
                throw new KlantException("Het ID is ongeldig. Het moet 1 of meer zijn.");
            }
            KlantId = klantId;
        }
        public void ZetAdres(string adres) {
            if (string.IsNullOrWhiteSpace(adres)) {
                throw new KlantException("Het adres mag niet leeg zijn.");
            }
            if (adres.Length <= 10) {
                throw new KlantException("Het andres moet minstens uit 10 karakters bestaan.");
            }
            Adres = adres;
        }
        public void ZetNaam(string naam) {
            if (string.IsNullOrWhiteSpace(naam)) {
                throw new KlantException("De naam mag niet leeg zijn.");
            }
            Name = naam;
        }
    }
}
