using System.Net;
using System.Net.Mail;
using BE_TaskManager.Models;
using BE_TaskManager.Models.Response;
using BE_TaskManager.Services;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Cursor;


namespace BE_TaskManager.Controllers {

    [Route("user")]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly UserServices _userServices;
        public UsersController(UserServices userServices) {
            _userServices = userServices;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response>> Create([FromBody] User request ){
            var user = await _userServices.Create(request);
            return Ok(user);
        }

        [HttpPut("update")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> Update(Guid id, [FromBody] User request){
            var user = await _userServices.Update(id, request);
            if(user.StatusCode == HttpStatusCode.NotFound){
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpDelete("delete")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> Delete(Guid id){
            var user = await _userServices.Delete(id);
            if(user.StatusCode == HttpStatusCode.NotFound){
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpGet("getAll")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetAll(){
            var user = await _userServices.GetAll();
            if(user.StatusCode == HttpStatusCode.NotFound){
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpGet("getById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetById(Guid id){
            var user = await _userServices.GetById(id);
            if(user.StatusCode == HttpStatusCode.NotFound){
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpGet("getByEmail")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response>> GetByEmail(string email){
            var user = await _userServices.GetByEmail(email);
            if(user.StatusCode == HttpStatusCode.NotFound){
                return NotFound(user);
            }
            return Ok(user);
        }

    }
}