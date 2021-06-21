using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManagerBot.Model
{
	public class User
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string UserName { get; set; }
		public bool IsSpecialUser { get; set; }

	}
}
