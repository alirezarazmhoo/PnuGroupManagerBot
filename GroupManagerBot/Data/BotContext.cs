using GroupManagerBot.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManagerBot.Data
{
 public	class BotContext : DbContext
	{
		//localDb
		public BotContext() : base("Data Source=.;Initial Catalog=GroupManagerDb;Integrated Security=true")
		{
			this.Configuration.ProxyCreationEnabled = false;
			this.Configuration.LazyLoadingEnabled = false;
		}
		public DbSet<User>  Users { get; set; }
		public DbSet<UserMessage>  UserMessages { get; set; }


	}
}
