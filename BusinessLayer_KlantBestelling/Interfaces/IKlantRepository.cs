using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer_KlantBestelling.Interfaces {
    public interface IKlantRepository {
        Klant GeefKlant(int klantId);
        bool BestaatKlant(int KlantId);
        bool BestaatKlant(Klant klant);
        Klant KlantToevoegen(Klant klant);
        void VerwijderKlant(int klantId);
        void KlantUpdaten(Klant klant);
    }
}
