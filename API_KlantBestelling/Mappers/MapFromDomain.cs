using API_KlantBestelling.Mappers.MapperExceptions;
using API_KlantBestelling.Model.Output;
using API_KlantBestelling.ModelExceptions;
using BusinessLayer_KlantBestelling;
using BusinessLayer_KlantBestelling.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_KlantBestelling.Mappers {
    public class MapFromDomain {
        public static KlantRESTOutputDTO MapFromKlantDomain(string url, Klant klant, BestellingService bestellingService) {
            try {
                string klantURL = $"{url}/klant/{klant.KlantId}";
                List<string> bestellingen = 
                    bestellingService.GeefBestellingenKlant(klant.KlantId)
                    .Select(x => $"{klantURL}/Bestelling/{x.BestellingId}")
                    .ToList();
                KlantRESTOutputDTO k = new KlantRESTOutputDTO(klantURL, klant.Name, klant.Adres, bestellingen);
                return k;
            } catch (Exception ex) {
                throw new MapException("MapFromKlantDomain" + ex.Message);
            }
        }

        public static BestellingRESTOutputDTO MapFromBestellingDomain(string url, Bestelling bestelling) {
            try {
                string klantURL = $"{url}/Klant/{bestelling.Klant.KlantId}";
                string bestellingURL = klantURL + $"/Bestelling/{bestelling.BestellingId}";
                BestellingRESTOutputDTO bestellingRESTOutputDTO = new(bestellingURL, klantURL, bestelling.Product.ToString(), bestelling.Aantal);
                return bestellingRESTOutputDTO;
            } catch (Exception ex) {
                throw new MapException("MapFromBestellingDomain" + ex.Message);
            }
        }
    }
}
