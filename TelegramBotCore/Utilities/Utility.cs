using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using RestSharp;
using TelegramBotCore.Models;
using TelegramBotCore.Repositories;
using User = TelegramBotCore.Models.Context.User;

namespace TelegramBotCore.Utilities
{
    public class Utility
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public Utility(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public string GetUserLink(string userUID)
        {
            return $"https://t.me/harfaynashnasbot?start={userUID}";
        }

        public string GetUid()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 9);
        }

        public bool CheckVerified(User user)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://www.instagram.com/{user.InstaUserName.Substring(1)}/?__a=1", Method.GET);
            var res = client.ExecuteGetAsync<InstaRoot>(request).Result;
            if (res.IsSuccessful)
            {
                if (res.Data.graphql.user.biography != null ? res.Data.graphql.user.biography.Contains(user.UserUid) : false)
                    return true;
                else if (res.Data.graphql.user.external_url != null ? res.Data.graphql.user.external_url.Contains(user.UserUid) : false)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<bool> CheckInstagramVerified(User user)
        {
            if (user.InstaUserName == null)
                throw new Exception("Username is not valid");
            bool flag = false;
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://www.instagram.com/{user.InstaUserName.Substring(1)}/?__a=1", Method.GET);
            var res = await client.ExecuteGetAsync<InstaRoot>(request);
            if (res.IsSuccessful)
            {
                var userId = long.Parse(res.Data.graphql.user.id);
                user.InstaUserId = userId.ToString();
                user.InstaProfileImage = res.Data.graphql.user.profile_pic_url_hd;

                var api = InstaApiBuilder.CreateBuilder()
                    .SetUser(new UserSessionData
                    {
                        UserName = "mr.mohande3",
                        Password = "Amdemon@1377",

                    })
                    .UseLogger(new DebugLogger(LogLevel.Exceptions))
                    .Build();
                string stateI = string.Empty;
                if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "stateInsta.txt")))
                    File.Create(Path.Combine(AppContext.BaseDirectory, "stateInsta.txt"));
                stateI = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "stateInsta.txt"));
                if (!string.IsNullOrEmpty(stateI))
                    api.LoadStateDataFromString(stateI);
                if (!api.IsUserAuthenticated)
                {
                    var respondLogin = api.LoginAsync().Result;
                    if (respondLogin.Succeeded)
                    {
                        var state = api.GetStateDataAsString();
                        File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "stateInsta.txt"),state);
                    }
                }
                var respondCU = await api.UserProcessor.GetFriendshipStatusAsync(userId);
                if (respondCU.Succeeded)
                {
                    return respondCU.Value.FollowedBy;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
            user = _repositoryWrapper.Users.UpdateEntry(user);
        }
    }
}
