namespace API_KlantBestelling.Model.Output {
    public class BestellingRESTOutputDTO
    {
        #region Properties
        public string BestellingId { get; set; }
        public string KlantId { get; set; }
        public string Product { get; set; }
        public int Aantal { get; set; }
        #endregion

        #region Constructors
        public BestellingRESTOutputDTO(string bestellingId, string klantId, string product, int aantal)
        {
            BestellingId = bestellingId;
            KlantId = klantId;
            Product = product;
            Aantal = aantal;
        }
        #endregion
    }
}
