using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace LineCSharpSDK
{
    public class LineOAuthResponse
    {
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("scope")]
        public object Scope { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
    public class LineUser
    {

        public string userId { get; set; }
        public string displayName { get; set; }
        public string pictureUrl { get; set; }

    }

    public class LineNotifyStatus
    {
        public int status { get; set; }
        public string message { get; set; }
    }
    public class LineLoginClient
    {
        private string _client_id { get; set; }
        private string _client_secret { get; set; }
        private string _redirect_uri { get; set; }


        public LineLoginClient(string clientId, string secrect, string redirectUrl)
        {
            this._client_id = clientId;
            _client_secret = secrect;
            _redirect_uri = redirectUrl;
        }

        public string GetOAuthUrl(string state)
        {
            var baseUrl = "";
            string login = "https://access.line.me/dialog/oauth/weblogin?";
            baseUrl = login;
            var url = string.Format("{0}response_type=code&client_id={1}&redirect_uri={2}&state={3}", baseUrl,
                _client_id, _redirect_uri, state);
            return url;
        }

        public string GetLineLoginAccessToken(string code)
        {

            try
            {
                var url = "https://api.line.me/v2/oauth/accessToken";
                HttpClient httpClient = new HttpClient();

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                nvc.Add(new KeyValuePair<string, string>("code", code));
                nvc.Add(new KeyValuePair<string, string>("client_id", _client_id));
                nvc.Add(new KeyValuePair<string, string>("client_secret", _client_secret));
                nvc.Add(new KeyValuePair<string, string>("redirect_uri", _redirect_uri));
                var res = httpClient.PostAsync(url, new FormUrlEncodedContent(nvc)).Result;
                var k = res.Content.ReadAsStringAsync().Result;

                var userdata = JsonConvert.DeserializeObject<LineOAuthResponse>(k);
                if (userdata != null)
                {
                    return userdata.AccessToken;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }


            //https://notify-api.line.me/api/notify





        }

        public LineUser GetLineLoginUserProfile(string accessToken)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers.Set("Authorization", "Bearer " + accessToken);
                var profileUrl = "https://api.line.me/v2/profile";
                var h = client.DownloadString(profileUrl);
                var lineUser = JsonConvert.DeserializeObject<LineUser>(h);
                return lineUser;
            }
            catch (Exception e)
            {
                return null;
            }

        }

    }

    public class LineNotifyClient
    {
        private string _client_id { get; set; }
        private string _client_secret { get; set; }
        private string _redirect_uri { get; set; }


        public LineNotifyClient(string clientId, string secrect, string redirectUrl)
        {
            this._client_id = clientId;
            _client_secret = secrect;
            _redirect_uri = redirectUrl;
        }

        public string GetLineNotifyOAuthUrl(string state)
        {
            var baseUrl = "";
            string notify = "https://notify-bot.line.me/oauth/authorize?";
            string login = "https://access.line.me/dialog/oauth/weblogin?";
            baseUrl = notify + "scope=notify&";
            var url = string.Format("{0}response_type=code&client_id={1}&redirect_uri={2}&state={3}", baseUrl,
                _client_id, _redirect_uri, state);

            return url;
        }

        public string GetLineAccessToken(string code)
        {

            try
            {
                var url = "https://notify-bot.line.me/oauth/token";

                HttpClient httpClient = new HttpClient();

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                nvc.Add(new KeyValuePair<string, string>("code", code));
                nvc.Add(new KeyValuePair<string, string>("client_id", _client_id));
                nvc.Add(new KeyValuePair<string, string>("client_secret", _client_secret));
                nvc.Add(new KeyValuePair<string, string>("redirect_uri", _redirect_uri));
                var res = httpClient.PostAsync(url, new FormUrlEncodedContent(nvc)).Result;
                var k = res.Content.ReadAsStringAsync().Result;

                var userdata = JsonConvert.DeserializeObject<LineOAuthResponse>(k);
                if (userdata != null)
                {
                    return userdata.AccessToken;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public LineNotifyStatus GetLineNotifyStatus(string accessToken)
        {
            try
            {
                var url = "https://notify-api.line.me/api/status";
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var res = httpClient.GetAsync(url).Result;
                var k = res.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<LineNotifyStatus>(k);
                return obj;
            }
            catch (Exception e)
            {
                return null;
            }
          
        }

        public LineNotifyStatus RevokeLineNotifyStatus(string accessToken)
        {
            try
            {
                var url = "https://notify-api.line.me/api/revoke";
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var res = httpClient.GetAsync(url).Result;
                var k = res.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<LineNotifyStatus>(k);
                return obj;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public LineNotifyStatus SendLineNotifiy(string accessToken, string message)
        {

            try
            {
                var url = "https://notify-api.line.me/api/notify";
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("message", message));


                var res = httpClient.PostAsync(url, new FormUrlEncodedContent(nvc)).Result;
                var k = res.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<LineNotifyStatus>(k);
                return obj;
            }
            catch (Exception e)
            {
                return null;
            }

        }



    }
}