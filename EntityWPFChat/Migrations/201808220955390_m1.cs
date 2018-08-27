namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MessageContent = c.String(nullable: false),
                        dateTime = c.DateTime(nullable: false),
                        Receiver_ID = c.Int(),
                        Sender_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.Receiver_ID)
                .ForeignKey("dbo.People", t => t.Sender_ID)
                .Index(t => t.Receiver_ID)
                .Index(t => t.Sender_ID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Sender_ID", "dbo.People");
            DropForeignKey("dbo.Messages", "Receiver_ID", "dbo.People");
            DropIndex("dbo.Messages", new[] { "Sender_ID" });
            DropIndex("dbo.Messages", new[] { "Receiver_ID" });
            DropTable("dbo.People");
            DropTable("dbo.Messages");
        }
    }
}
