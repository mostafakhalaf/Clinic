using Application.Enum;
using Application.Static;
using Core.Entities;
using Core.Repositories.UnitOfwork;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("List")]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(new APIReturnObj<List<Patient>>()
            {
                ReturnValue = await _unitOfWork.Patients.GetAllAsync(p=>p.IsDelete==false,null),
                Status = APIReturnStatus.Success
            });
        }
        [Route("Details")]
        [HttpPost]
        public async Task<IActionResult> GetDetails(APISendObj<Guid> entityInfo)
        {
            if (Guid.Empty == entityInfo.SendValue)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.BadRequest });
            // Check if Entity Still Exist
            var entity = await _unitOfWork.Patients.FindAsync(e => e.ID == entityInfo.SendValue && !e.IsDelete);
            if (entity == null)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.NotFound });

            //success found of company 
            return Ok(new APIReturnObj<Patient>() { ReturnValue = entity, Status = APIReturnStatus.Success });

        }
       
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> DoCreate(APISendObj<Patient> entity)
        {
            if (entity == null)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.BadRequest });
            entity.SendValue.ID = Guid.NewGuid();
            await _unitOfWork.Patients.AddAsync(entity.SendValue);
            await _unitOfWork.Complete();
            Ticket ticket = new Ticket();
            ticket.ID = Guid.NewGuid();
            ticket.PatientID = entity.SendValue.ID;
            ticket.IsDelete = false;
            ticket.Number = (await _unitOfWork.Patients.CountAsync()).ToString();
            string basePath = Path.Combine(AppContext.BaseDirectory, "Tickets");
            string DirectoryPath = Path.Combine(basePath,entity.SendValue.phoneNumber+ ".Jpeg");


            CheckerFolder.CheckFolderExist(basePath);
            var t = string.Concat("  د: محمد خميس", Environment.NewLine, "عياده الاسكندريه", Environment.NewLine, "رقم ميعادك هو ",
                Environment.NewLine,ticket.Number, Environment.NewLine,"ميعاد حجزك التقريبى هو", Environment.NewLine,DateTime.Now.Hour,"-",DateTime.Now.Hour);
            TicketsImageSaver.DrawText(Color.Black, Color.White, "Arial", 30, t, 300, 300, DirectoryPath);

            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.Complete();
            return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.Success });

        }
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> DoUpdate(APISendObj<Patient> entity)
        {
            if (entity == null)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.BadRequest });
            _unitOfWork.Patients.Update(entity.SendValue);
            await _unitOfWork.Complete();
            return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.Success });

        }
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DoDelete(APISendObj<Guid> entityID)
        {
            if (entityID.SendValue == Guid.Empty)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.BadRequest });
            var patient = await _unitOfWork.Patients.FindAsync(p => p.ID == entityID.SendValue);
            if (patient == null)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.NotFound });

            patient.IsDelete = true;
            _unitOfWork.Patients.Update(patient);
            await _unitOfWork.Complete();
            var Ticket = await _unitOfWork.Tickets.FindAsync(t=>t.PatientID==patient.ID);
            Ticket.IsDelete=true;
            _unitOfWork.Tickets.Update(Ticket);
            await _unitOfWork.Complete();
            return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.Success });


        }
    }
}
