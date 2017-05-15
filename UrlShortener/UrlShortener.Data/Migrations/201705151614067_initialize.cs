namespace UrlShortener.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Url",
                c =>
                    new
                    {
                        Id = c.Guid(false),
                        CreationDateTime = c.DateTime(false),
                        InflateUrl = c.String(),
                        ShortenedUrl = c.String()
                    }).PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            this.DropTable("dbo.Url");
        }
    }
}