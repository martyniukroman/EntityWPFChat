namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class badgetsneeds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "MessageCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "MessageCount");
        }
    }
}
