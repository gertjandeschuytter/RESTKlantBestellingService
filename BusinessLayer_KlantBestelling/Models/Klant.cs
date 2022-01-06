using BusinessLayer_KlantBestelling.ModelExceptions;
using System;
using System.Collections.Generic;

namespace BusinessLayer_KlantBestelling {
    public class Klant {
        public string Name { get; private set; }
        public string Adres { get; private set; }
        public int KlantId { get; private set; }
        public List<Bestelling> _bestellingen { get; set; } = new List<Bestelling>();

        public Klant(int id, string naam, string adres, List<Bestelling> b)
        {
            this._bestellingen = b;
            ZetKlantId(id);
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public Klant(string naam, string adres, List<Bestelling> b)
        {
            this._bestellingen = b;
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public Klant(int id, string naam, string adres)
        {
            ZetKlantId(id);
            ZetNaam(naam);
            ZetAdres(adres);
        }
        public Klant(string naam, string adres)
        {
            ZetNaam(naam);
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

        public void VerwijderBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("Klant : VerwijderBestelling - bestelling is null");
            if (!_bestellingen.Contains(bestelling))
            {
                throw new KlantException("Klant : RemoveBestelling - bestelling does not exists");
            }
            else
            {
                if (bestelling.Klant == this)
                    bestelling.VerwijderKlant();
            }
        }

        public void VoegToeBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("Klant : VerwijderBestelling - bestelling is null");
            if (_bestellingen.Contains(bestelling))
            {
                throw new KlantException("Klant : AddBestelling - bestelling already exists");
            }
            else
            {
                _bestellingen.Add(bestelling);
                if (bestelling.Klant != this)
                    bestelling.ZetKlant(this);
            }
        }
        public bool HeeftBestelling(Bestelling bestelling)
        {
            if (_bestellingen.Contains(bestelling)) return true;
            else return false;
        }
    }
}
