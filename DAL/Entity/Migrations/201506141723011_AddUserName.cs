namespace DAL.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCertificate", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserCertificate", "UserName");
        }
    }
}
