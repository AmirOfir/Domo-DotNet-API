using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomoDotNetAPI;

namespace Test
{
	class Program
	{
		class ITME_DETAILS
		{
			public int H { get; set; }
			public string V { get; set; }
		}
		class ITME
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public List<ITME_DETAILS> Details { get; set; }
		}

		class Primitives
		{
			public string Id { get; set; }
			public string Name { get; set; }
			public string prim { get; set; }
		}

		static void Main(string[] args)
		{
			var at = Authentication.CreateAccessToken("b3f476ac-6b5f-4a6e-82f3-84e15e5eee21", "b81d6646c4f9397a6e3e83b68d16f60bc9097aa14ef8f94fa1c40dd796b77b50", new string[] { "data", "user" });
			DataSets d = new DataSets(at);
			//d.Create<Primitives>();


			var lstPrimitives = new List<Primitives>
			{
				new Primitives() { Id="a1", Name="N1", prim="dg"},
				new Primitives() { Id="a2", Name="N2", prim="dg2"},
				new Primitives() { Id="a3", Name="N3", prim="dg3"},
				new Primitives() { Id="a4", Name="N4", prim="dg4"},
				new Primitives() { Id="a5", Name="N5", prim="dg5"},
				new Primitives() { Id="a6", Name="N6", prim="dg6"},
			};

			Console.Write(d.ShallowObjectCsvMaker(lstPrimitives));

			d.ImportData(Guid.Parse("07f0f343-a004-42f7-aab3-ac5397b79bf7"), lstPrimitives);
		}
	}
}
