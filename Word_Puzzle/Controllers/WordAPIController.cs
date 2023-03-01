using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Word_Puzzle.Data;
using Word_Puzzle.Logging;
using Word_Puzzle.Model;
using Word_Puzzle.Model.DTO;
using Word_Puzzle.Model.Store;

namespace Word_Puzzle.Controllers
{
    [Route("api/WordAPI")]
    [ApiController]
    public class WordAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly DataContext _dataContext;

        public WordAPIController(ILogging logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [EnableCors("MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<WordDTO> GetWord()
        {
            WordDTO wordDTO = Word_Puzzle_Store.GetWord(_dataContext);


            return wordDTO;

        }



    }
}
