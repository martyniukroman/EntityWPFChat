namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Name", c => c.Int(nullable: false));
        }
    }
}
