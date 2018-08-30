namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomsfixes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Rooms_ID", "dbo.Rooms");
            DropIndex("dbo.People", new[] { "Rooms_ID" });
            AddColumn("dbo.Messages", "Room_ID", c => c.Int());
            CreateIndex("dbo.Messages", "Room_ID");
            AddForeignKey("dbo.Messages", "Room_ID", "dbo.Rooms", "ID");
            DropColumn("dbo.People", "Rooms_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Rooms_ID", c => c.Int());
            DropForeignKey("dbo.Messages", "Room_ID", "dbo.Rooms");
            DropIndex("dbo.Messages", new[] { "Room_ID" });
            DropColumn("dbo.Messages", "Room_ID");
            CreateIndex("dbo.People", "Rooms_ID");
            AddForeignKey("dbo.People", "Rooms_ID", "dbo.Rooms", "ID");
        }
    }
}
