using BusinessLayer_KlantBestelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_KlantBestelling.Model.Output {
    public class KlantRESTOutputDTO {

        public KlantRESTOutputDTO(string klantId, string name, string adres, List<string> bestellingen) {
            Name = name;
            Adres = adres;
            KlantId = klantId;
            this.bestellingen = bestellingen;
        }

        public string KlantId { get; private set; }
        public string Name { get; private set; }
        public string Adres { get; private set; }
        public List<string> bestellingen { get; private set; }
    }
}
