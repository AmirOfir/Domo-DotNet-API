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


		public Guid Create<T>(string name, string desc = null)
		{
			var request = new RestRequest("datasets", Method.POST);
			request.AddParameter(new Parameter() { Type = ParameterType.HttpHeader, Name = "Accept", Value = "application/json" });

			CreateDataSetDTO schema = new CreateDataSetDTO()
			{
				name = name,
				description = desc,
				schema = new CreateDataSetDTO.Schema() { columns = ConvertToDto(typeof(T)) },
			};

			request.AddHeader("Content-Type", "application/json");
			request.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(schema), ParameterType.RequestBody);

			var info = client.Execute<DataSetInfo>(request);
			return info.Data.id;
		}
		private List<CreateDataSetColumnDTO> ConvertToDto(Type t)
		{
			List<CreateDataSetColumnDTO> r = new List<CreateDataSetColumnDTO>();
			var properties = t.GetProperties();
			foreach (var item in properties)
			{
				CreateDataSetColumnDTO v = new CreateDataSetColumnDTO() { name = item.Name };
				if (!item.PropertyType.IsValueType && item.PropertyType.Name != "String")
					throw new Exception("Datasets only supports primitive types");
				switch (item.PropertyType.Name)
				{
					case "Int32":
						v.type = "INT";
						break;
					case "DateTime":
						v.type = "DATE";
						break;
					case "String":
					default:
						v.type = "STRING";
						break;
				}
				r.Add(v);
				
			}
			return r;
		}
		class CreateDataSetDTO
		{
			public class Schema {
				public List<CreateDataSetColumnDTO> columns { get; set; }
			}

			public string name { get; set; }
			public string description { get; set; }
			public int rows { get; set; } = 0;
			public Schema schema { get; set; } = new Schema();
		}
		class CreateDataSetColumnDTO
		{
			public string type { get; set; }
			public string name { get; set; }
		}


		public void ImportData<T>(Guid id, List<T> data)
		{
			var request = new RestRequest($"datasets/{id}/data", Method.PUT);
			request.AddHeader("Content-Type", "text/csv");
			//request.RequestFormat = DataFormat.
			var csv = ShallowObjectCsvMaker(data);
			request.AddParameter("text/csv",csv.ToString(), ParameterType.RequestBody);

			var result = client.Execute(request);

		}
		
		public string ShallowObjectCsvMaker<T>(List<T> data)
		{
			StringBuilder csv = new StringBuilder();
			var lstProps = typeof(T).GetProperties();

			foreach (var item in data)
			{
				string[] vals = new string[lstProps.Length];
				for (int i = 0; i < lstProps.Length; i++)
				{
					vals[i] = lstProps[i].GetValue(item).ToString();
				}
				csv.AppendLine(string.Join(",", vals));
			}

			return csv.ToString();
		}
	}
}
