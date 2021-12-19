using API_KlantBestelling.Mappers.MapperExceptions;
using API_KlantBestelling.Model.Input;
using API_KlantBestelling.Model.Output;
using BusinessLayer_KlantBestelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_KlantBestelling.Mappers {
    public class MapToDomain {
        public static Klant MapToKlantDomainPUT(KlantRESTInputDTOPUT dto) {
            try {
                if (dto.KlantId > 0) return new Klant(dto.KlantId, dto.Name, dto.Adres);
                else return new Klant(dto.Name, dto.Adres);
            } catch (Exception ex) {
                throw new MapException("MapToDomain: MapToKlantDomain", ex);
            }
        }
        public static Klant MapToKlantDomainPOST(KlantRESTInputDTOPOST dto) {
            try {
                return new Klant(dto.Name, dto.Adres);
            } catch (Exception ex) {
                throw new MapException("MapToDomain: MapToKlantDomain", ex);
            }
        }
        public static Bestelling MapToBestellingDomainPUT(BestellingRESTInputDTOPUT dto, Klant klant) {
            try {
                Product product = (Product)Enum.Parse(typeof(Product), dto.Product);
                if (dto.BestellingId > 0) return new Bestelling(klant, dto.Aantal, (int)product);
                else return new Bestelling(dto.BestellingId, klant, dto.Aantal, (int)product);
            } catch (Exception ex) {
                throw new MapException("MapToDomain: MapToBestellingDomain", ex);
            }
        }
        public static Bestelling MapToBestellingDomainPOST(BestellingRESTInputDTOPOST dto, Klant klant) {
            try {
                Product product = (Product)Enum.Parse(typeof(Product), dto.Product);
                return new Bestelling(klant, dto.Aantal, (int)product);
            } catch (Exception ex) {
                throw new MapException("MapToDomain: MapToBestellingDomain", ex);
            }
        }
        //static bool CheckIfProductExists(string product) {
        //    try {
        //        if (Enum.IsDefined(typeof(Product), product)) {
        //            return true;
        //        } else {
        //            return false;
        //        }
        //    } catch (Exception) {
        //        throw new MapException("Product is niet geldig");
        //    }
    }
}

