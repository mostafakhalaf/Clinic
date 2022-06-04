using Application.Enum;
using Application.Static;
using Core.Entities;
using Core.Repositories.UnitOfwork;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("List")]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(new APIReturnObj<List<Ticket>>()
            {
                ReturnValue = await _unitOfWork.Tickets.GetAllAsync(p => p.IsDelete == false,null,p => p.Number,orderByType: OrderBy.Descending),
                Status = APIReturnStatus.Success
            });
        }
        [Route("Details")]
        [HttpPost]
        public async Task<IActionResult> GetDetails(APISendObj<string> entityInfo)
        {
            if (string.IsNullOrEmpty(entityInfo.SendValue))
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.BadRequest });
            // Check if Entity Still Exist
            var Patient = await _unitOfWork.Patients.FindAsync(e => e.phoneNumber == entityInfo.SendValue && !e.IsDelete,null);
            if (Patient == null)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.NotFound });
            var Ticket = await _unitOfWork.Tickets.FindAsync(e => e.PatientID == Patient.ID && !e.IsDelete, new[] { "Patient" });
            return Ok(new APIReturnObj<Ticket>() { ReturnValue = Ticket, Status = APIReturnStatus.Success });
        }
    }
}
