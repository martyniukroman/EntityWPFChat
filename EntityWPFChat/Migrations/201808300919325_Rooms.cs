namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rooms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Room", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Room");
        }
    }
}
