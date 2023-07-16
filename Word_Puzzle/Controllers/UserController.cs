using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puzzle_API.Model;
using Puzzle_API.Model.DTO;
using Puzzle_API.Model.Store;
using Puzzle_API.Data;
using Puzzle_API.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Puzzle_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly DataContext _dataContext;

        public UserController(ILogger<UserController> logger, Puzzle_API.Data.DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

      
        [HttpPost("/add/{user}")]
        public async Task<ActionResult<UserDTO>> AddUser(UserDTO user)
        {
            try
            {
               if(UserStore.IsEmailNotAvailable(_dataContext, user.Email))
                {
                    return Conflict("User email already in use.");
                }

                UserStore.AddUser(_dataContext, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500,"There was an error setting up the user.");
            }


            return Ok();
        }

        [HttpPost("/update/{user}")]
        public async Task<ActionResult<WordDTO>> UpdateUser(UserDTO user)
        {
            try
            {
                await UserStore.UpdateUserScore(user, _dataContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("There was an error setting up the user.");
            }

            return Ok();
        }

        [HttpPost("/updatescore/{user}")]
        public async Task<ActionResult<UserDTO>> Updatescore(UserDTO user)
        {
            try
            {
                await UserStore.UpdateUserScore(user, _dataContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("There was an error setting up the user.");
            }

            if (string.IsNullOrEmpty(user.SessionId))
            {
                return BadRequest("Invalid session ID");
            }

            return user;
        }

        [HttpPost("/updatesessionid")]
        public async Task<ActionResult<WordDTO>> UpdateSessionId(UserDTO user)
        {
            try
            {
                UserStore.UpdateUser(_dataContext, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("There was an error setting up the user.");
            }

            return Ok();
        }


    }
}
