namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "Room");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Room", c => c.Int(nullable: false));
        }
    }
}
