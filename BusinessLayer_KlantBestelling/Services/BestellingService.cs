using BusinessLayer_KlantBestelling.Interfaces;
using BusinessLayer_KlantBestelling.ServiceExceptions;
using System;
using System.Collections.Generic;

namespace BusinessLayer_KlantBestelling.Services {
    public class BestellingService {

        private readonly IBestellingRepository _bestellingrepo;
        private readonly IKlantRepository _klantrepo;

        public BestellingService(IBestellingRepository bestellingrepo, IKlantRepository klantrepo)
        {
            this._bestellingrepo = bestellingrepo;
            this._klantrepo = klantrepo;
        }

        public IEnumerable<Bestelling> GeefBestellingenKlant(int klantId)
        {
            try
            {
                return _bestellingrepo.GeefBestellingenKlant(klantId);
            }
            catch (Exception ex)
            {
                throw new BestellingServiceException("Bestellingen van klant kunnen niet getoond worden - " + ex.Message);
            }
        }

        public bool HeeftKlantBestellingen(int klantId)
        {
            try
            {
                return _bestellingrepo.HeeftKlantBestellingen(klantId);
            }
            catch (Exception ex)
            {
                throw new BestellingServiceException("Heeft klant bestellingen - er is iets foutgelopen" + ex.Message);
            }
        }

        public Bestelling BestellingToevoegen(Bestelling bestelling)
        {
            try
            {
                if (bestelling == null)
                {
                    throw new BestellingServiceException("Bestelling is null");
                }
                if (!_klantrepo.BestaatKlant(bestelling.Klant.KlantId))
                {
                    throw new BestellingServiceException("klant bestaat nog niet dus er kunnen geen bestellingen worden aangemaakt");
                }
                Bestelling b = _bestellingrepo.BestellingToevoegen(bestelling);
                return b;
            }
            catch (Exception ex)
            {
                throw new BestellingServiceException("Bestelling kan niet toegevoegd worden - " + ex.Message);
            }
        }

        public Bestelling BestellingTonen(int bestellingId)
        {
            try
            {
                if (!BestaatBestelling(bestellingId))
                {
                    throw new BestellingServiceException("Bestelling bestaat niet.");
                }
                return _bestellingrepo.BestellingTonen(bestellingId);
            }
            catch (Exception ex)
            {
                throw new BestellingServiceException("Bestelling kan niet getoond worden - " + ex.Message);
            }
        }

        public void BestellingVerwijderen(int bestellingId)
        {
            try
            {
                if (!_bestellingrepo.BestaatBestelling(bestellingId))
                {
                    throw new BestellingServiceException("Bestelling bestaat niet.");
                }
                _bestellingrepo.VerwijderBestelling(bestellingId);
            }
            catch (Exception ex)
            {
                throw new BestellingServiceException("Bestelling kan niet verwijderd worden - " + ex.Message);
            }
        }

        public bool BestaatBestelling(int bestellingId)
        {
            try
            {
                return _bestellingrepo.BestaatBestelling(bestellingId);
            }
            catch (Exception ex)
            {
                throw new BestellingServiceException("Bestelling bestaat niet - " + ex.Message);
            }
        }

        public Bestelling UpdateBestelling(Bestelling bestelling)
        {
            try
            {
                if (bestelling == null)
                {
                    throw new BestellingServiceException("Bestelling is null.");
                }
                if (!_bestellingrepo.BestaatBestelling(bestelling.BestellingId))
                {
                    throw new BestellingServiceException("Bestelling bestaat niet.");
                }
                Bestelling bestellingDb = BestellingTonen(bestelling.BestellingId);
                if (bestellingDb == bestelling)
                {
                    throw new BestellingServiceException("Er zijn geen verschillen met het origineel.");
                }
                _bestellingrepo.UpdateBestelling(bestelling);
                return bestelling;
            }
            catch (Exception ex)
            {
                throw new BestellingServiceException("Bestelling kan niet upgedatet worden - " + ex.Message);
            }
        }
    }
}
