using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoDotNetAPI
{
	public class DataSetInfo
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public int rows { get; set; }
		public int columns { get; set; }
		public object schema { get; set; }
		public object owner { get; set; }
		public DateTime createdAt { get; set; }
		public DateTime updatedAt { get; set; }
	}
}
