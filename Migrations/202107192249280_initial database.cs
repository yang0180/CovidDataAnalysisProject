namespace Assignment2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CovidCases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pruid = c.String(),
                        Prname = c.String(),
                        PrnameFR = c.String(),
                        Date = c.String(),
                        Numconf = c.String(),
                        Numprob = c.String(),
                        Numdeath = c.String(),
                        Numtotal = c.String(),
                        Numtoday = c.String(),
                        Ratetotal = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CovidCases");
        }
    }
}
