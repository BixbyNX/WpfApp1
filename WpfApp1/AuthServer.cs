using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IdentityModel.Tokens.Jwt;
using static WpfApp1.AuthServer;

namespace WpfApp1
{
    public class AuthServer
    {
        private readonly HttpClient client;
        JWTHelper jwtHelper = new JWTHelper();

        public AuthServer()
        {
            client = new HttpClient();
        }
        public async Task<string> Register(string username, string email, string branchService, string personnelType, string contactNo, string serialNumber, string password)
        {
            try
            {
                string apiURL = "http://localhost:3000/registration";

                var data = new
                {
                    username,
                    email,
                    branchService,
                    personnelType,
                    contactNo,
                    serialNumber,
                    password,
                    
                };

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiURL, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
        }
            public async Task<LoginResponse> Login(string serialNumber, string password)
            {
                string apiURL = "http://localhost:3000/login";

            try
            {
                var data = new
                {
                    serialNumber,
                    password
                };
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiURL, content);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    JObject responseData = JObject.Parse(responseContent);


                    string jwtToken = (string)responseData["accessToken"];
                    string currentUserID = jwtHelper.ExtractUserIDFromToken(jwtToken);

                    Properties.Settings.Default.JWTToken = jwtToken;
                    Properties.Settings.Default.Save();



                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {

                    Console.WriteLine("User not found");
                    // You can throw an exception or return a specific response indicating failure
                    return new LoginResponse { Response = "User not found", StatusCode = response.StatusCode };

                }
                return new LoginResponse { Response = "HTTP request failed", StatusCode = response.StatusCode };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
            }
            public class JWTHelper
            {
                public string ExtractUserIDFromToken(string jwtToken)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;
                    string userID = jsonToken?.Subject;

                    Console.WriteLine("The current user id is " + userID);
                    return userID;

                }
            }

            public class LoginResponse
            {
                public string Response { get; set; }
                public HttpStatusCode StatusCode { get; set; }
            }
        }
    }
