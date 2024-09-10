using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IMedicalhistoryService _medicalhistoryService;


        public HistoryController(IMedicalhistoryService medicalhistoryService)
        {
            _medicalhistoryService = medicalhistoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalHistory([FromBody] MedicalHistory medicalHistory)
        {
            var MedicalHistoryAdded = await _medicalhistoryService.AddMedicalHistory(medicalHistory);
            return Ok(MedicalHistoryAdded);
        }
        [HttpGet]
        public async Task<IActionResult> GetMedicalHistoryById(int historyid)
        {
            var history=await _medicalhistoryService.GetMedicalHistory(historyid);
            return Ok(history);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMedicalHistory(int historyid, [FromBody] MedicalHistory medicalHistory)
        {
            if (historyid != medicalHistory.HistoryId)
            {
                return BadRequest("History id mismatch");
            }
            var updatedhistory=await _medicalhistoryService.UpdateMedicalHistory(historyid, medicalHistory);
            if(updatedhistory == null)
            {
                return NotFound();
            }
            return Ok(updatedhistory);
        }
    }
}
