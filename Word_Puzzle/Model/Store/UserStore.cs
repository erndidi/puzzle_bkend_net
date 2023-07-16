using Puzzle_API.Model.DTO;
using Puzzle_API.Data;
using Puzzle_API.Logging;
using Serilog;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Puzzle_API.Model.Store
{
    public static class UserStore
    {



        public static bool IsEmailNotAvailable(DataContext dataContext, string email)
        {
            return dataContext.UserDetail.Any(ud => ud.Email == email);
        }

        public static Guid AddUser(DataContext dataContext, UserDTO user)
        {
            Log.Logger = new LoggerConfiguration()
                       .WriteTo.Console()
                       .CreateLogger();
            Guid uid = Guid.Empty;
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
                        UserWords = user.UsedWord,
                        UserName = user.UserName

                    };
                    dataContext.UserDetail.Add(ud);

                    uid = ud.Id;
                    string sessionId = GetSessionId();
                    UserSession us = new UserSession
                    {
                        UserDetailId = uid,
                        SessionId = sessionId,
                        DateTimeEntered = DateTime.Now
                    };

                    dataContext.UserSessions.Add(us);
                    dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while executing AddUser method");
            }
            return uid;
        }

        public static void UpdateUser(DataContext dataContext, UserDTO user)
        {
            Log.Logger = new LoggerConfiguration()
                      .WriteTo.Console()
                      .CreateLogger();
            Guid uid = Guid.Empty;
            try
            {

                DateTime rightNow = DateTime.Now;
                DateTime userDateTimeEntered = dataContext.UserSessions.Where(us => us.UserDetailId == user.Id).Select(x => x.DateTimeEntered).FirstOrDefault();

                if (IsSessionValid(userDateTimeEntered))
                {
                    UserDetail userFromDB = dataContext.UserDetail.Where(ud => ud.Id == user.Id).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while executing UpdateUser method");
            }
        }

        public static async Task<UserDTO> UpdateUserScore(UserDTO user, DataContext dc)
        {
            UserDetail userDetail = dc.UserDetail.Where(ud => ud.Id == user.Id).FirstOrDefault();
            UserWord userWord = dc.UserWord.Where(uw => uw.Id == user.Id).FirstOrDefault();
            UserSession userSession = dc.UserSessions.Where(us => us.Id == user.Id).FirstOrDefault();
            try
            {

                if ((userSession != null) && (userSession.SessionId == user.SessionId))
                {
                    userDetail.Score = user.Score;
                    userWord.UsedWord = GetNewUserWordString(userWord.UsedWord, user.UsedWord);
                    if (userSession.DateTimeEntered.Minute > Constants.Timeout)
                    {
                        userSession.SessionId= GetSessionId();
                    }

                }
                //update session id if needed.
                await dc.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Format("An error occurred while executing UpdateUserScoreWord method for user{0}",user.Id.ToString()));
            }

            finally
            {
                await dc.SaveChangesAsync();   
                if(userSession!=null)
                {
                  user.SessionId = userSession.SessionId;
                }
                else
                {
                    user.SessionId = GetSessionId();
                    
                }

            }
            return user;
        }



        private static void AddUserWord(DataContext dataContext, UserDTO user)
        {
            try
            {
                if (user.Email != null)
                {
                    string currentWord = (from u in dataContext.UserDetail
                                          join uw in dataContext.UserWord
                                          on u.Id equals uw.UserDetail.Id
                                          select uw.UsedWord).FirstOrDefault();

                    if (!string.IsNullOrEmpty(currentWord))
                    {
                        currentWord = string.Join(",", currentWord, user.UsedWord);
                    }
                    else
                    {
                        currentWord = user.UsedWord;
                    }

                }
                dataContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while executing GetWord method");
            }
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

        //private static UserDTO GetUserDTO(UserDetail user, UserSession session)
        //{
        //    return new UserDTO {  }
        //}

        private static bool IsSessionValid(DateTime userEnteredDateTime)
        {
            DateTime rightNow = DateTime.Now;
            DateTime userTimeInSession = userEnteredDateTime;

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
