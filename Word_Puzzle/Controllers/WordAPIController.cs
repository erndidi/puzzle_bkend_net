using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puzzle_API.Data;
using Puzzle_API.Logging;
using Puzzle_API.Model;
using Puzzle_API.Model.DTO;
using Puzzle_API.Model.Store;

namespace Puzzle_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PuzzleGame")]
    public class WordAPIController : ControllerBase
    {
        private readonly ILogger<WordAPIController> _logger;
        private readonly DataContext _dataContext;
      
        public WordAPIController(ILogger<WordAPIController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

   
            [HttpGet("/getword/{word},{sessionid}")]
        public async Task<ActionResult<WordDTO>> GetWord(string word, string sessionid)
        {
            _logger.LogInformation("Logger is working");
            WordDTO wordDTO = Word_Store.GetWord(_dataContext,word,sessionid); 
            return wordDTO;
        }


    }
}
