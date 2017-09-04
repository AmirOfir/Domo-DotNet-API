using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using RestSharp;

namespace DomoDotNetAPI
{
	public class DataSets
	{
		RestClient client;

		public DataSets(string authToken)
		{
			client = new RestClient("https://api.domo.com/v1/");
			client.DefaultParameters.Add(new Parameter () { Type = ParameterType.HttpHeader, Name = "Authorization", Value = $"Bearer {authToken}" });
		}


		public Guid Create<T>()
		{
			var request = new RestRequest("datasets", Method.POST);
			request.AddParameter(new Parameter() { Type = ParameterType.HttpHeader, Name = "Accept", Value= "application/json"});

			JSchemaGenerator generator = new JSchemaGenerator();
			JSchema schema = generator.Generate(typeof(T));
			request.AddParameter("application/json", schema.ToString(), ParameterType.RequestBody);

			var info = client.Execute<DataSetInfo>(request);
			return info.Data.id;
		}


	}
}
