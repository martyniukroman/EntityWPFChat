namespace EntityWPFChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pictures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "PictureLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "PictureLink");
        }
    }
}
