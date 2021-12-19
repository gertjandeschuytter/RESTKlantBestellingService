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
        private IKlantRepository _repo;

        public KlantService(IKlantRepository repo, IBestellingRepository bestellingRepo) {
            this._repo = repo;
            this._bestellingRepo = bestellingRepo;
        }

        public Klant ToonKlant(int klantId) {
            return _repo.GeefKlant(klantId);
        }

        public Klant KlantToevoegen(Klant klant) {
            try {
                if (klant == null) {
                    throw new KlantServiceException("Klant is null.");
                }
                if (_repo.BestaatKlant(klant)) {
                    throw new KlantServiceException("Klant bestaat al.");
                }
                return _repo.KlantToevoegen(klant);
            } catch (Exception ex) {
                throw new KlantServiceException("KlantToevoegen - " + ex.Message);
            }

        }

        public void VerwijderKlant(int klantId) {
            try {
                if (!_repo.BestaatKlant(klantId)) {
                    throw new KlantServiceException("Klant bestaat niet.");
                }
                if (_bestellingRepo.HeeftKlantBestellingen(klantId)) {
                    throw new KlantServiceException("Klant heeft nog bestellingen.");
                }
            } catch (Exception ex) {
                throw new KlantServiceException("Verwijder klant - " + ex.Message);
            }
            _repo.VerwijderKlant(klantId);
        }

        public bool bestaatKlant(int klantId) {
            try {
                return _repo.BestaatKlant(klantId);
            } catch (Exception ex) {
                throw new KlantServiceException("Klant bestaat niet", ex);
            }
        }

        public Klant UpdateKlant(Klant klant) {
            try {
                if (klant == null) {
                    throw new KlantServiceException("Klant is null.");
                }
                if (!_repo.BestaatKlant(klant.KlantId)) {
                    throw new KlantServiceException("Klant bestaat niet.");
                }
                Klant klantDb = ToonKlant(klant.KlantId);
                if (klantDb == klant) {
                    throw new KlantServiceException("Er zijn geen verschillen met het origineel.");
                }
                _repo.KlantUpdaten(klant);
                return klant;
            } catch (Exception ex) {
                throw new KlantServiceException("UpdateKlant - " + ex.Message);
            }
        }
    }
}

