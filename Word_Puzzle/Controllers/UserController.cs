using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puzzle_API.Model;
using Puzzle_API.Model.DTO;
using Puzzle_API.Model.Store;
using Puzzle_API.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Puzzle_API.Hub;

namespace Puzzle_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly DataContext _dataContext;

        public UserController(ILogger<UserController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet("/gettopScores")]
        public List<PlayerDTO> GetAllScores(PlayerDTO user)
        {
            
            return PlayerStore.GetTopPlayerScores(_dataContext);
            ;
        }

        [HttpPost("/add")]
      
        public async Task<ActionResult<PlayerDTO>> Add([FromBody] PlayerDTO user)
        {
            PlayerDTO player = new PlayerDTO();
            string sessionid = string.Empty;

            try
            {

                if (PlayerStore.IsEmailNotAvailable(_dataContext, user.Email))
                {
                    // return Conflict("User email already in use.");
                }

                if (PlayerStore.IsUserNameNotAvailable(_dataContext, user.UserName))
                {
                    // return Conflict("User email already in use.");

                }
                player = await PlayerStore.AddPlayerAsync(_dataContext, user);
            }



            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                 return StatusCode(500, "There was an error setting up the user.");
            }


            return player;
        }
        [HttpPost("/login")]
        public async Task<ActionResult<PlayerDTO>> Login([FromBody] PlayerDTO player)
        {
            player.UserFound = false; 
            try
            {
                PlayerStore.GetPlayer(_dataContext, ref player);         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);                
            }
            return player;
        }

    }
}
