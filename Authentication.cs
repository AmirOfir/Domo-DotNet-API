﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace DomoDotNetAPI
{
    public static class Authentication
    {
		class OAuthRes
		{
			public string access_token { get; set; }
		}
		/// <summary>
		/// Creates an access token 
		/// </summary>
		/// <param name="clientId"></param>
		/// <param name="clientSecret"></param>
		/// <param name="scopes"></param>
		/// <returns></returns>
		public static string CreateAccessToken(string clientId, string clientSecret, string[] scopes)
		{
			var base64 = Base64Encode($"{clientId}:{clientSecret}");
			var client = new RestClient($"https://api.domo.com/");
			var request = new RestRequest($"/oauth/token?grant_type=client_credentials&scope={string.Join(" ",scopes)}", Method.GET);
			request.AddHeader("Authorization", "Basic " + base64);
			var response = client.Execute<OAuthRes>(request);

			Trace.WriteLine(response.ResponseStatus == ResponseStatus.Completed ? response.Content : response.ErrorMessage);
			if (response.Data != null)
				return response.Data.access_token;
			return null;
		}


		private static string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
	}
}
