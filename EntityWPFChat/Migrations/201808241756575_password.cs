namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class password : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Password");
        }
    }
}
