using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IDPDemoApp.HttpClient.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestClientController : ControllerBase
    {

        [HttpGet]
        public async Task<dynamic> Get()
        {
            var httpClient = new System.Net.Http.HttpClient();

            // discover endpoints from metadata
            var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (discoveryDocument.IsError)
            {
                Console.WriteLine(discoveryDocument.Error);
                return BadRequest(discoveryDocument.Error);
            }

            //Make Token Request
            var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "m2mclient",
                ClientSecret = "secret",
                GrantType = "client_credentials",//GrantTypes.ResourceOwnerPassword,
                Scope = "api1"
            };

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return BadRequest(tokenResponse.Error);
            }

            // call resource api
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync("https://localhost:44357/api/values");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return BadRequest(response.StatusCode.ToString());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            return Ok(jsonString);
            
        }


        [HttpGet("getClaims")]
        public async Task<dynamic> GetClaims()
        {
            
            var httpClient = new System.Net.Http.HttpClient();

            // discover endpoints from metadata
            var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (discoveryDocument.IsError)
            {
                Console.WriteLine(discoveryDocument.Error);
                return BadRequest(discoveryDocument.Error);
            }

            //Make Token Request
            var passwordTokenRequest = new PasswordTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "ropacc",
                ClientSecret = "secret",
                GrantType = "password",//GrantTypes.ResourceOwnerPassword,
                Scope = "api1",
                UserName = "Jon",
                Password = "jon123"
            };

           var tokenResponse =  await httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

           if (tokenResponse.IsError)
           {
               Console.WriteLine(tokenResponse.Error);
               return BadRequest(tokenResponse.Error);
           }

            // call resource api
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync("https://localhost:44357/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return BadRequest(response.StatusCode.ToString());
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            return Ok(jsonString);
        }


        [HttpGet("getUserInfo")]
        public async Task<dynamic> GetUserInfo()
        {

            var httpClient = new System.Net.Http.HttpClient();

            // discover endpoints from metadata
            var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (discoveryDocument.IsError)
            {
                Console.WriteLine(discoveryDocument.Error);
                return BadRequest(discoveryDocument.Error);
            }

            //Make Token Request
            var passwordTokenRequest = new PasswordTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "ropacc",
                ClientSecret = "secret",
                GrantType = "password",
                Scope = "openid profile api1 role email", //notice scopes requested
                UserName = "Jon",
                Password = "jon123"
            };

            var tokenResponse = await httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return BadRequest(tokenResponse.Error);
            }

            //call userinfo-endpoint
            var userInfoClient = new System.Net.Http.HttpClient();

            //Make UserInfoRequest
            var userInfoRequest = new UserInfoRequest
            {
                Address = discoveryDocument.UserInfoEndpoint,
                Token = tokenResponse.AccessToken
            };
            var userInfoResponse = await userInfoClient.GetUserInfoAsync(userInfoRequest);
            var jsonString = userInfoResponse.Claims.Select(claim => new { claim.Type, claim.Value });


            return Ok(jsonString);
        }


        // To create a token you can use one of the following methods, which totally depends upon which grant type you are using for token generation.

        //Task<TokenResponse> RequestAuthorizationCodeTokenAsync(AuthorizationCodeTokenRequest)
        //Task<TokenResponse> RequestClientCredentialsTokenAsync(ClientCredentialsTokenRequest)
        //Task<TokenResponse> RequestDeviceTokenAsync(DeviceTokenRequest)
        //Task<TokenResponse> RequestPasswordTokenAsync(PasswordTokenRequest)
        //Task<TokenResponse> RequestRefreshTokenAsync(RefreshTokenRequest)
        //Task<TokenResponse> RequestTokenAsync(TokenRequest)
    }

    
}
