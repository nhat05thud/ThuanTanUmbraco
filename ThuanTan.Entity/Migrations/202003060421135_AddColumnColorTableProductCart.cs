namespace ThuanTan.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnColorTableProductCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCarts", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCarts", "Color");
        }
    }
}
