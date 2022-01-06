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
        public static Klant MapToKlantDomain(KlantRESTInputDTO dto)
        {
            try
            {
                if (dto.KlantId > 0) return new Klant(dto.KlantId, dto.Name, dto.Adres);
                else return new Klant(dto.Name, dto.Adres);
            }
            catch (Exception ex)
            {
                throw new MapException("Klant - "+ ex.Message);
            }
        }
        public static Bestelling MapToBestellingDomain(BestellingRESTInputDTO dto, Klant klant)
        {
            try
            {
                Product product = (Product)Enum.Parse(typeof(Product), dto.Product);
                if (dto.BestellingId > 0) return new Bestelling(dto.BestellingId, klant, dto.Aantal, (int)product);
                else return new Bestelling(klant, dto.Aantal, (int)product);
            }
            catch (Exception ex)
            {
                throw new MapException("Bestelling - "+ ex.Message);
            }
        }
    }
}

