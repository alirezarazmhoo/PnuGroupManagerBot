namespace GroupManagerBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserMessages", "PollId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserMessages", "PollId", c => c.Int(nullable: false));
        }
    }
}
