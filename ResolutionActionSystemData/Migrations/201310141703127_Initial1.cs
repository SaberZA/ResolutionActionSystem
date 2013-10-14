namespace ResolutionActionSystemContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meeting", "PreviousMeeting_MeetingId", c => c.Int());
            AddForeignKey("dbo.Meeting", "PreviousMeeting_MeetingId", "dbo.Meeting", "MeetingId");
            CreateIndex("dbo.Meeting", "PreviousMeeting_MeetingId");
            DropColumn("dbo.Meeting", "PreviousMeetingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meeting", "PreviousMeetingId", c => c.Int(nullable: false));
            DropIndex("dbo.Meeting", new[] { "PreviousMeeting_MeetingId" });
            DropForeignKey("dbo.Meeting", "PreviousMeeting_MeetingId", "dbo.Meeting");
            DropColumn("dbo.Meeting", "PreviousMeeting_MeetingId");
        }
    }
}
