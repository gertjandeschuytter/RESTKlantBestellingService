using BusinessLayer_KlantBestelling.Interfaces;
using BusinessLayer_KlantBestelling.ServiceExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer_KlantBestelling.Services {
    public class BestellingService {

        private readonly IBestellingRepository _repo;
        private readonly IKlantRepository _klantrepo;

        public BestellingService(IBestellingRepository repo, IKlantRepository klantrepo) {
            this._repo = repo;
            this._klantrepo = klantrepo;
        }

        public IEnumerable<Bestelling> GeefBestellingenKlant(int klantId) {
            try {
                return _repo.GeefBestellingenKlant(klantId);
            } catch (Exception ex) {
                throw new BestellingServiceException("GeefBestellingenKlant - error", ex);
            }
        }

        public bool HeeftKlantBestellingen(int klantId) {
            try {
                return _repo.HeeftKlantBestellingen(klantId);
            } catch (Exception ex) {
                throw new BestellingServiceException("HeeftBestellingenKlant - Klant heeft nog bestellingen en kan niet verwijderd worden", ex);
            }
        }

        public Bestelling BestellingToevoegen(Bestelling b) {
            try {
                if (b == null) {
                    throw new BestellingServiceException("Bestelling is null");
                }
                if (!_klantrepo.BestaatKlant(b.Klant.KlantId)) {
                    throw new BestellingServiceException("klant bestaat nog niet dus er kunnen geen bestellingen worden aangemaakt");
                }
                _repo.BestellingToevoegen(b);
                return b;
                } catch (Exception ex) {
                throw new BestellingServiceException("BestellingToevoegen" + ex);
            }
        }

        public Bestelling BestellingTonen(int bestellingId) {
            try {
                //hier moet nog een bestaat bestelling
                return _repo.BestellingTonen(bestellingId);
            } catch (Exception ex) {
                throw new BestellingServiceException("BestellingWeergeven - bestelling bestaat niet", ex);
            }
        }

        public void BestellingVerwijderen(int bestellingId) {
            try {
                if (!_repo.BestaatBestelling(bestellingId)) {
                    throw new BestellingServiceException("Bestelling bestaat niet.");
                }
                _repo.VerwijderBestelling(bestellingId);
            } catch (Exception ex) {
                throw new BestellingServiceException("BestellingVerwijderen - " + ex.Message);
            }
        }

        public bool BestaatBestelling(int klantId) {
            try {
                return _repo.BestaatBestelling(klantId);
            } catch (Exception ex) {
                throw new BestellingServiceException("Bestelling bestaat niet - " + ex.Message);
            }
        }

        public Bestelling UpdateBestelling(Bestelling bestelling) {
            if (bestelling == null) {
                throw new BestellingServiceException("Bestelling is null.");
            }
            if (!_repo.BestaatBestelling(bestelling.BestellingId)) {
                throw new BestellingServiceException("Bestelling bestaat niet.");
            }
            Bestelling bestellingDb = BestellingTonen(bestelling.BestellingId);
            if (bestellingDb == bestelling) {
                throw new BestellingServiceException("Er zijn geen verschillen met het origineel.");
            }
            _repo.UpdateBestelling(bestelling);
            return bestelling;
        }
    }
}
