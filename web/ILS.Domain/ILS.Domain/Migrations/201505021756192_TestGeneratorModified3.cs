namespace ILS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestGeneratorModified3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TGTaskTemplates", "Name", c => c.String());
            AddColumn("dbo.TGTaskTemplates", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TGTaskTemplates", "Description");
            DropColumn("dbo.TGTaskTemplates", "Name");
        }
    }
}
