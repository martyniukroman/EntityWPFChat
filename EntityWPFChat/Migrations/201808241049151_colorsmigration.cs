namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colorsmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Color");
        }
    }
}
