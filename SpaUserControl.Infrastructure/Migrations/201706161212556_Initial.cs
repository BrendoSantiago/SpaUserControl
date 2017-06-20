namespace SpaUserControl.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        //PARA PREEENCHER ESTA CLASSE, ASSSISTIR O VÍDEO 2

        public override void Up()
        {
        }

        public override void Down()
        {
            //DropIndex("dbo.User", "IX_EMAIL");
            //DropTable("dbo.User");
        }
    }
}