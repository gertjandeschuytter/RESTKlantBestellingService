using API_KlantBestelling.Mappers;
using API_KlantBestelling.Model.Input;
using API_KlantBestelling.Model.Output;
using BusinessLayer_KlantBestelling;
using BusinessLayer_KlantBestelling.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_KlantBestelling.Controllers {
    [Route("api/klant")]
    [ApiController]
    public class KlantBestellingController : ControllerBase {
        private string url = "http://localhost:44328";
        private BestellingService _bestellingService;
        private KlantService _klantService;

        public KlantBestellingController(BestellingService bestellingService, KlantService klantService) {
            this._bestellingService = bestellingService;
            this._klantService = klantService;
        }
        #region Klant GET POST DELETE PUT
        // GET: Klant
        [HttpGet("{KlantId}")]
        public ActionResult<KlantRESTOutputDTO> GetKlant(int KlantId) {
            try {
                Klant klant = _klantService.ToonKlant(KlantId);
                return Ok(MapFromDomain.MapFromKlantDomain(url, klant, _bestellingService));
            } catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }
        //POST Klant
        [HttpPost]
        public ActionResult<KlantRESTOutputDTO> PostKlant([FromBody] KlantRESTInputDTOPOST restdto) {
            try {
                Klant klant = _klantService.KlantToevoegen(MapToDomain.MapToKlantDomainPOST(restdto));
                return CreatedAtAction(nameof(GetKlant), new { KlantId = klant.KlantId }, MapFromDomain.MapFromKlantDomain(url, klant, _bestellingService));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        //DELETE Klant
        [HttpDelete("{KlantId}")]
        public ActionResult<KlantRESTOutputDTO> DeleteKlant(int KlantId) {
            try {
                _klantService.VerwijderKlant(KlantId);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        //PUT Klant
        [HttpPut("{KlantId}")]
        public ActionResult<KlantRESTOutputDTO> PutKlant(int KlantId, [FromBody] KlantRESTInputDTOPUT klantRESTInputDTO) {
            try {
                if (KlantId != klantRESTInputDTO.KlantId) {
                    return BadRequest();
                }
                if (_klantService.bestaatKlant(KlantId)) {
                    Klant klant = _klantService.UpdateKlant(MapToDomain.MapToKlantDomainPUT(klantRESTInputDTO));
                    return CreatedAtAction(nameof(GetKlant), new { KlantId = klantRESTInputDTO.KlantId }, MapFromDomain.MapFromKlantDomain(url, klant, _bestellingService));
                } else {
                    return BadRequest("Klant bestaat niet");
                }
            } catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }
        #endregion

        #region Bestelling GET POST DELETE
        //GET Bestellingen
        [HttpGet]
        [Route("{KlantId}/Bestelling/{bestellingId}")]
        public ActionResult<BestellingRESTOutputDTO> GetBestelling(int KlantId, int bestellingId) {
            try {
                Bestelling bestelling = _bestellingService.BestellingTonen(bestellingId);
                if (bestelling.Klant.KlantId != KlantId) {
                    return BadRequest("KlantId komt niet overeen met de opgegeven KlantId");
                }
                return Ok(MapFromDomain.MapFromBestellingDomain(url, bestelling));
            } catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }

        //POST Bestelling
        [HttpPost]
        [Route("{KlantId}/Bestelling/")]
        public ActionResult<KlantRESTOutputDTO> PostBestelling(int KlantId, [FromBody] BestellingRESTInputDTOPOST restdto) {
            try {
                Klant klant = _klantService.ToonKlant(restdto.KlantId);
                if (KlantId != restdto.KlantId) {
                    return BadRequest("KlantId komt niet overeen de opgegeven Id");
                }
                if (!Enum.IsDefined(typeof(Product), restdto.Product))
                    return BadRequest("VoegBestellingToe - product is ongeldig");
                else {
                    Bestelling bestelling = _bestellingService.BestellingToevoegen(MapToDomain.MapToBestellingDomainPOST(restdto, klant));
                    return CreatedAtAction(nameof(GetBestelling), new { KlantId = bestelling.Klant.KlantId, bestellingId = bestelling.BestellingId }, MapFromDomain.MapFromBestellingDomain(url, bestelling));

                }
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        //DELETE Bestelling
        [HttpDelete]
        [Route("{KlantId}/Bestelling/{bestellingId}")]
        public ActionResult<BestellingRESTOutputDTO> DeleteBestelling(int KlantId, int bestellingId) {
            try {
                if (!_klantService.bestaatKlant(KlantId)) {
                    return BadRequest("Klant bestaat niet.");
                }
                _bestellingService.BestellingVerwijderen(bestellingId);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        //PUT Bestelling
        [HttpPut]
        [Route("{KlantId}/Bestelling/{bestellingId}")]
        public ActionResult<BestellingRESTOutputDTO> PutBestelling(int KlantId, int bestellingId, [FromBody] BestellingRESTInputDTOPUT bestellingRESTInputDTOPUT) {
            try {
                if (!_bestellingService.BestaatBestelling(KlantId) || bestellingRESTInputDTOPUT == null) {
                    return BadRequest();
                }
                Klant klant = _klantService.ToonKlant(KlantId);
                if (klant.KlantId != bestellingRESTInputDTOPUT.KlantId) {
                    return BadRequest("KlantId komt niet overeen met KlantId.");
                }
                Bestelling bestelling = MapToDomain.MapToBestellingDomainPUT(bestellingRESTInputDTOPUT, klant);
                bestelling.ZetId(bestellingId);
                Bestelling bestellingDb = _bestellingService.UpdateBestelling(bestelling);
                return CreatedAtAction(nameof(GetBestelling), new { KlantId = bestellingDb.Klant.KlantId, bestellingId = bestellingDb.BestellingId }, MapFromDomain.MapFromBestellingDomain(url, bestellingDb));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
