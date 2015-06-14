namespace DAL.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCertificate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Certificate = c.String(),
                        Thumbprint = c.String(),
                        Password = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        IsInstalled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserCertificate");
        }
    }
}
