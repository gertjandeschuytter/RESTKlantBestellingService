using BusinessLayer_KlantBestelling;

namespace API_KlantBestelling.Model.Input {
    public class BestellingRESTInputDTOPOST
    {
        public int KlantId { get; set; }
        public string Product { get; set; }
        public int Aantal { get; set; }
    }
}
