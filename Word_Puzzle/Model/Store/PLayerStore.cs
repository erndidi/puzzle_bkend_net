using Puzzle_API.Model.DTO;
using Puzzle_API.Logging;
using Serilog;
using System.Security.Cryptography;
using Puzzle_API.Hub;

namespace Puzzle_API.Model.Store
{
    public static class PlayerStore
    {



        public static bool IsEmailNotAvailable(DataContext dataContext, string email)
        {
            return dataContext.UserDetail.Any(ud => ud.Email == email);
        }

        public static bool IsUserNameNotAvailable(DataContext dataContext, string email)
        {
            return dataContext.UserDetail.Any(ud => ud.UserName == email);
        }
        public static void GetPlayer(DataContext dataContext, ref PlayerDTO user)
        {
            string testEmail = user.Email;
            if(dataContext.UserDetail.Count((p)=>p.Email==testEmail)>0)
            {
                user.SessionId = GetSessionId();
                user.UserFound= true;
            }
          
          
        }

        public async static Task<PlayerDTO> AddPlayerAsync(DataContext dataContext, PlayerDTO user)
        {
            Log.Logger = new LoggerConfiguration()
                       .WriteTo.Console()
                       .CreateLogger();
            Guid uid = Guid.Empty;
            PlayerDTO player = new PlayerDTO();
            try
            {

                if (!string.IsNullOrEmpty(user.Email))
                {
                    UserDetail ud = new UserDetail
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        Lastname = user.Lastname,
                        FacebookId = user.FacebookId,
                        GoogleId = user.GoogleId,
                        Score = 0,
                        UserName = user.UserName,
                        CreateDateTime = DateTime.Now,
                        UpdateDateTime = DateTime.Now,
                        Password = user.Password

                    };

                    if (!string.IsNullOrEmpty(user.UsedWordId))
                    {
                        AddUserWord(dataContext, user);
                    }
                    dataContext.UserDetail.Add(ud);

                    uid = ud.Id;

                    await dataContext.SaveChangesAsync();

                    player.SessionId = GetSessionId();
                   


                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while executing AddUser method");
            }
            return player;
        }

        private static void AddUserWord(DataContext dataContext, PlayerDTO user)
        {
            try
            {
                if (user.Email != null)
                {
                    string? wordIds = (from u in dataContext.UserDetail
                                       select u.UserWords).FirstOrDefault();

                    if (!string.IsNullOrEmpty(wordIds))
                    {
                        if (wordIds.Contains(","))
                        {
                            List<string> currentWords = wordIds.Split(',').ToList();

                            if (currentWords.Count > 50)
                            {
                                currentWords.RemoveAt(0);
                                currentWords.Add(string.Concat(user.UsedWordId, ","));
                            }
                            else
                            {
                                currentWords.Add(string.Concat(user.UsedWordId, ","));
                            }
                        }

                    }
                    else
                    {
                        wordIds = string.Concat(user.UsedWordId, ",");
                    }

                }
                dataContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while executing GetWord method");
            }
        }

        public static void UpdateUser(DataContext dataContext, PlayerDTO user)
        {
            try
            {
                if (IsSessionValid(dataContext, user))
                {
                    UserDetail userFromDB = dataContext.UserDetail.Where(ud => ud.Id == user.Id).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while executing UpdateUser method");
            }
        }

        public static async Task<string> UpdateUserScore(PlayerDTO udto, DataContext dc)
        {
            UserDetail userDetail = dc.UserDetail.Where(ud => ud.Id == udto.Id).FirstOrDefault();
            string message = string.Empty;

            try
            {

                if ((userDetail.SessionId != null) && (userDetail.SessionId == udto.SessionId))
                {
                    userDetail.Score = udto.Score;

                    if (userDetail.UpdateDateTime.Minute > Constants.Timeout)
                    {
                        userDetail.SessionId = GetSessionId();
                    }

                }
                //update session id if needed.
                await dc.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Format("An error occurred while executing UpdateUserScoreWord method for user{0}", udto.Id.ToString()));
                message = ex.Message;
            }
            if (string.IsNullOrEmpty(message))
            {
                message = "Update succeeded.";
            }

            return message;
        }
        public static List<PlayerDTO> GetTopPlayerScores(DataContext dc)
        {
            List<PlayerDTO> topPlayers= (from player in dc.UserDetail
                                      orderby player.Score descending
                                      select new PlayerDTO
                                      {
                                          UserName= player.UserName,
                                          Score= player.Score
                                      }).Take(10).ToList();
            return topPlayers;
        }


        private static string GetSessionId()
        {
            byte[] sessionIdBytes = new byte[16]; // Generate 16 bytes for session ID

            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(sessionIdBytes); // Fill the byte array with random values
            }

            string sessionId = BitConverter.ToString(sessionIdBytes).Replace("-", "").ToLower(); // Convert the byte array to a hexadecimal string

            return sessionId;
        }

     
        private static bool IsSessionValid(DataContext dc, PlayerDTO udto)
        {
            UserDetail ud = dc.UserDetail.Where(u => u.SessionId == udto.SessionId).FirstOrDefault();
            DateTime rightNow = DateTime.Now;
            DateTime userTimeInSession = ud.UpdateDateTime;

            TimeSpan timeElapsed = rightNow - userTimeInSession;
            double minutesElapsed = timeElapsed.TotalMinutes;
            return minutesElapsed < Constants.Timeout ? true : false;
        }

        private static string GetNewUserWordString(string currentWord, string newWord)
        {
            string[] workArray = currentWord.Split(',');
            workArray.Append(string.Format("{0}, {1}", ",", currentWord));
            return workArray.ToString();
        }
    }
}
