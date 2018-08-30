namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomsdbset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PeopleCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.People", "Rooms_ID", c => c.Int());
            CreateIndex("dbo.People", "Rooms_ID");
            AddForeignKey("dbo.People", "Rooms_ID", "dbo.Rooms", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "Rooms_ID", "dbo.Rooms");
            DropIndex("dbo.People", new[] { "Rooms_ID" });
            DropColumn("dbo.People", "Rooms_ID");
            DropTable("dbo.Rooms");
        }
    }
}
