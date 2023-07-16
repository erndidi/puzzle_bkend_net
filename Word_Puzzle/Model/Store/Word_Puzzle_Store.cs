using Microsoft.EntityFrameworkCore.Diagnostics;
using Puzzle_API.Data;
using Puzzle_API.Model.DTO;
using Serilog;
using System.Drawing.Text;

namespace Puzzle_API.Model.Store
{
    public static class Word_Store
    {

       public static WordDTO GetWord(DataContext dataContext,string incomingWord,string sessionid)
        {
            Log.Logger = new LoggerConfiguration()
                          .WriteTo.Console()
                          .CreateLogger();
           Word word = null;
            
            WordDTO wordDTO = new WordDTO();
            try
            {
                if (!incomingWord.Equals("*"))
                {
                    Guid userid = dataContext.UserSessions.Where(us => us.SessionId == sessionid).Select(us => us.UserDetailId).FirstOrDefault();
                    if (!sessionid.Equals("*"))
                    {
                        
                        string usedWord = string.Empty;
                        //the below is a comma delimited string of words used in puzzled that have been solved in the past by the user
                        string wordToParse = dataContext.UserWord.Where(uw => uw.UserDetail.Id == userid).Select(uw => uw.UsedWord).FirstOrDefault();

                        foreach (var wrd in dataContext.Words)
                        {
                            //making sure that a word solved recently doesn't reappear
                            if (!wrd.Text.Contains(wordToParse) && wrd.Text.Contains(incomingWord))
                            {
                                wordDTO.Id= wrd.Id;
                                wordDTO.Text= wrd.Text;
                                wordDTO = GetDefinitions(ref wordDTO, dataContext, wrd);

                            }
                        }
                    }
                    StoreUsedWord(dataContext, userid, incomingWord);
                }
                else
                {
                    Random rand = new Random();
                    // Word word;
                    int count = dataContext.Words.Count();
                    int rec = rand.Next(1, count);
                    word = dataContext.Words.Where(w => w.Id == rec).SingleOrDefault();
                    wordDTO = new WordDTO {  Id = word.Id, Text = word.Text};
                    wordDTO = GetDefinitions(ref wordDTO, dataContext, word);
                }
               
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while executing GetWord method");
            }

            Log.CloseAndFlush();

            return wordDTO;
        }

        private static WordDTO GetDefinitions(ref WordDTO wordDTO, DataContext dataContext, Word word)
        {
            wordDTO.Definitions.Add(GetTrueDefinition(dataContext, word.Id));
            wordDTO.Definitions.AddRange(Word_Store.GetDecoys(dataContext, 5, word.Id));
            return wordDTO;
        }

        private static DefinitionDTO GetTrueDefinition(DataContext datacontext, int wordid)
        {
            Definition def =  datacontext.Definitions.Where(d => d.WordId == wordid).SingleOrDefault();
            DefinitionDTO dto = new DefinitionDTO { Id = def.Id, Text = def.Text, WordId = def.WordId };
            return dto; 
        }

        private static List<DefinitionDTO> GetDecoys(DataContext dataContext, int total, int wordId)
        {

            List<DefinitionDTO> defTOs = new List<DefinitionDTO>();
            try
            {
                List<int> ids = dataContext.Definitions.Select(id => id.WordId).ToList();
                List<int> chosenIds = new List<int>();
                //the below action is used to select definition id's unlike the true definition
                //their maybe a better way to do this with lambdas or a delegate,but in the interest of time,I'm using the below method.
                for (int i = 0; i < total;i++)
                {
                    Random random = new Random();
                    int r = random.Next(0, ids.Count());
                    chosenIds.Add(ids[r]);
                }

                for (int idx = 0; idx < chosenIds.Count; idx++)
                {
                    int workid = chosenIds[idx];
                    DefinitionDTO? dto = (from d in dataContext.Definitions
                                          where d.WordId == workid
                                          select new DefinitionDTO
                                          {
                                              Id = d.Id,
                                              Text = d.Text,
                                              WordId = d.WordId
                                          }).FirstOrDefault();
                    defTOs.Add(dto);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           

           return defTOs;

        }
        private static async void StoreUsedWord(DataContext dc,Guid userid, string incomingWord)
        {
            UserWord alreadyUsedWords = dc.UserWord.Where(uw => uw.UserDetail.Id == userid).FirstOrDefault();
            if(alreadyUsedWords != null)
            {
                alreadyUsedWords.UsedWord = string.Join(",", alreadyUsedWords.UsedWord,incomingWord);
            }
            else
            {
                alreadyUsedWords = new UserWord
                {
                    Id = Guid.NewGuid(),
                    UsedWord = incomingWord,
                    UserDetail = new UserDetail { Id = userid }
                };
            }
            dc.UserWord.Add(alreadyUsedWords);
            await dc.SaveChangesAsync();
        }
        
    }
}
