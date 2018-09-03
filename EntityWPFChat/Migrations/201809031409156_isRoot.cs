namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isRoot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "isRoot", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "isRoot");
        }
    }
}
