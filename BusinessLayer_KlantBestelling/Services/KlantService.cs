using BusinessLayer_KlantBestelling.Interfaces;
using BusinessLayer_KlantBestelling.ServiceExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer_KlantBestelling.Services {
    public class KlantService {

        private IBestellingRepository _bestellingRepo;
        private IKlantRepository _klantRepo;

        public KlantService(IKlantRepository klantRepo, IBestellingRepository bestellingRepo) {
            this._klantRepo = klantRepo;
            this._bestellingRepo = bestellingRepo;
        }

        public Klant ToonKlant(int klantId) {
            try
            {
                if (!bestaatKlant(klantId))
                {
                    throw new KlantServiceException("Klant bestaat niet");
                }
                return _klantRepo.GeefKlant(klantId);
            }
            catch (Exception ex)
            {
                throw new KlantServiceException("Klant kan niet getoond worden - " + ex.Message);
            }
        }

        public Klant KlantToevoegen(Klant klant) {
            try {
                if (klant == null) {
                    throw new KlantServiceException("Klant is null.");
                }
                if (_klantRepo.BestaatKlant(klant)) {
                    throw new KlantServiceException("Klant bestaat al.");
                }
                return _klantRepo.KlantToevoegen(klant);
            } catch (Exception ex) {
                throw new KlantServiceException("Klant kan niet toegevoegd worden - " + ex.Message);
            }

        }

        public void VerwijderKlant(int klantId) {
            try {
                if (!_klantRepo.BestaatKlant(klantId)) {
                    throw new KlantServiceException("Klant bestaat niet.");
                }
                if (_bestellingRepo.HeeftKlantBestellingen(klantId)) {
                    throw new KlantServiceException("Klant heeft nog bestellingen.");
                }
            } catch (Exception ex) {
                throw new KlantServiceException("Klant kan niet verwijderd worden - " + ex.Message);
            }
            _klantRepo.VerwijderKlant(klantId);
        }

        public bool bestaatKlant(int klantId) {
            try {
                return _klantRepo.BestaatKlant(klantId);
            } catch (Exception ex) {
                throw new KlantServiceException("Klant bestaat niet - "+ ex.Message);
            }
        }

        public Klant UpdateKlant(Klant klant) {
            try {
                if (klant == null) {
                    throw new KlantServiceException("Klant is null.");
                }
                if (!_klantRepo.BestaatKlant(klant.KlantId)) {
                    throw new KlantServiceException("Klant bestaat niet.");
                }
                Klant klantDb = ToonKlant(klant.KlantId);
                if (klantDb == klant) {
                    throw new KlantServiceException("Er zijn geen verschillen met het origineel.");
                }
                _klantRepo.KlantUpdaten(klant);
                return klant;
            } catch (Exception ex) {
                throw new KlantServiceException("Klant kan niet geupdatet worden - " + ex.Message);
            }
        }
    }
}

