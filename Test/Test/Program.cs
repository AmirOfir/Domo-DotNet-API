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

		static void Main(string[] args)
		{
			var at = Authentication.CreateAccessToken("b3f476ac-6b5f-4a6e-82f3-84e15e5eee21", "b81d6646c4f9397a6e3e83b68d16f60bc9097aa14ef8f94fa1c40dd796b77b50", new string[] { "data", "user" });
			DataSets d = new DataSets(at);
			d.Create<ITME>();
		}
	}
}
