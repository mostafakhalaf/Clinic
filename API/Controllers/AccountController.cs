using API.Helper;
using Application.Enum;
using Core.Entities;
using Core.Repositories.UnitOfwork;
using Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> DoLogin(UserLogin model)
        {
            if (model == null)
                return Ok(new APIReturnObj<object>()
                {
                    ReturnValue = null,
                    Status = APIReturnStatus.BadRequest
                }); ;

            var user = await _unitOfWork.Users.FindAsync(c => (c.UserName == model.Username));
            if (user == null)
            {
                return Ok(new APIReturnObj<object>()
                {
                    ReturnValue = null,
                    Status = APIReturnStatus.WrongUser
                });
            }
            if (user.IsDelete)
            {
                return Ok(new APIReturnObj<object>()
                {
                    ReturnValue = null,
                    Status = APIReturnStatus.NotFound
                });
            }
            if (!user.IsActive)
            {
                return Ok(new APIReturnObj<object>()
                {
                    ReturnValue = null,
                    Status = APIReturnStatus.NotActive
                });
            }
            //cheack  password of user  

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))

            {
                return Ok(new APIReturnObj<object>()
                {
                    ReturnValue = null,
                    Status = APIReturnStatus.WrongPassword
                });
            }

            LoginResponse userIdentity = new LoginResponse();
            userIdentity.username = user.UserName;
            userIdentity.Token = TokenHandler.CreateToken(user);
            return Ok(new APIReturnObj<LoginResponse>()
            {
                ReturnValue = userIdentity,
                Status = APIReturnStatus.Success
            });

        }
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> DoCreate(APISendObj<User> entity)
        {
            if (entity == null)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.BadRequest });
            // Do Custom Data Modification or setting
            /* To Do Code Generation Functionality */
            //cheack to make sure no UserName exist 
            var mainUser = await _unitOfWork.Users.FindAsync(u => u.UserName == entity.SendValue.UserName);
            if (mainUser != null)
            {
                return Ok(new APIReturnObj<object>()
                {
                    ReturnValue = null,
                    Status = APIReturnStatus.UserNameIsExist
                });
            }

            entity.SendValue.Password = BCrypt.Net.BCrypt.HashPassword(entity.SendValue.Password);
            bool success = await _unitOfWork.Users.AddAsync(entity.SendValue);
            await _unitOfWork.Complete();
            // make VerificationCode to can login

            /*send message to phone*/
            /*send message to email */
            return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = success ? APIReturnStatus.Success : APIReturnStatus.Failure });
        }
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> DoUpdate(APISendObj<User> entity)
        {
            if (entity == null)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.BadRequest });

            // Check if Entity Still Exist
            var oldEntity = await _unitOfWork.Users.FindAsync(e => e.ID == entity.SendValue.ID && !e.IsDelete);
            if (oldEntity == null)
                return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = APIReturnStatus.NotFound });

            //Check User Not Null

            if (oldEntity == null)
                return Ok(new APIReturnObj<object>()
                {
                    ReturnValue = null,
                    Status = APIReturnStatus.BadRequest
                });
            //Check Of User Name If Exist Before By Another User
            if (oldEntity.UserName != entity.SendValue.UserName)
            {
                if (await _unitOfWork.Users.FindAsync(u => u.UserName == entity.SendValue.UserName) != null)

                    return Ok(new APIReturnObj<object>()
                    {
                        ReturnValue = null,
                        Status = APIReturnStatus.UserNameIsExist
                    });
            }

            bool success = _unitOfWork.Users.Update(oldEntity, entity.SendValue);
            await _unitOfWork.Complete();

            return Ok(new APIReturnObj<object>() { ReturnValue = null, Status = success ? APIReturnStatus.Success : APIReturnStatus.Failure });
        }


    }
}
