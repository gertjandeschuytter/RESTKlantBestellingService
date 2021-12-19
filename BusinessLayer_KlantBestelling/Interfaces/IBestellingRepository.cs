using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer_KlantBestelling.Interfaces {
    public interface IBestellingRepository {
        IEnumerable<Bestelling> GeefBestellingenKlant(int klantId);
        bool HeeftKlantBestellingen(int klantId);
        bool BestaatBestelling(int BestellingId);
        Bestelling BestellingToevoegen(Bestelling b);
        Bestelling BestellingTonen(int bestellingId);
        void VerwijderBestelling(int bestellingId);
        void UpdateBestelling(Bestelling bestelling);
    }
}
