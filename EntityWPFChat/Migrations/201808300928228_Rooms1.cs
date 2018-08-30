namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rooms1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Receiver_ID", "dbo.People");
            DropIndex("dbo.Messages", new[] { "Receiver_ID" });
            DropColumn("dbo.Messages", "Receiver_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Receiver_ID", c => c.Int());
            CreateIndex("dbo.Messages", "Receiver_ID");
            AddForeignKey("dbo.Messages", "Receiver_ID", "dbo.People", "ID");
        }
    }
}
