using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManagerBot.Model
{
	public class UserMessage
	{
		public int Id { get; set; }
		public int Code { get; set;  }
		public string PollId { get; set; }
		public int UserId { get; set; }
		public User User { get; set;  }

	}
}
