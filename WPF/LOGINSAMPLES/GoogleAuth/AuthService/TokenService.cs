using Haley.Utils;
using Haley.Rest;
using Haley.Models;
using Haley.Abstractions;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AuthService {
    public static class TokenService {

        static IClient _gClient = new FluentClient();

        static string _clientID = @"261544832001-o5mmdem14ule7rnj0bg2c7itdipaem3d.apps.googleusercontent.com";
        static string _clientSecret = @"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";
        static string _authURL = @"https://accounts.google.com/o/oauth2/v2/auth";
        static  string _tokenURL = @"https://oauth2.googleapis.com/token";

        public static string GetRequestURL() {
            List<QueryParam> paramlist = new List<QueryParam>();
            paramlist.Add(new QueryParam("client_id", _clientID));
            paramlist.Add(new QueryParam("redirect_uri", @"http://localhost:8500/api/auth/google"));
            paramlist.Add(new QueryParam("response_type", "code"));
            paramlist.Add(new QueryParam("scope", "email profile"));

            return _authURL + "?" + new QueryParamList(paramlist).GetConcatenatedString();
        }

        public static async Task<string> GetAccessToken(string code) {
            List<QueryParam> paramlist = new List<QueryParam>();
            paramlist.Add(new QueryParam("client_id", _clientID));
            paramlist.Add(new QueryParam("client_secret", _clientSecret));
            paramlist.Add(new QueryParam("redirect_uri", @"http://localhost:8500/api/auth/google"));
            paramlist.Add(new QueryParam("grant_type", "authorization_code"));
            paramlist.Add(new QueryParam("code", code));

            var raw_response = await _gClient
                .WithEndPoint(_tokenURL)
                .WithParameter(new FormEncodedRequest(paramlist))
                .PostAsync();
            var response = await raw_response.AsStringResponseAsync();
            if (!response.IsSuccessStatusCode) return null;

            var jObj = JsonObject.Parse(response.Content);
            if (jObj == null) return null;
            return jObj["access_token"]?.ToString();
        }
    }
}