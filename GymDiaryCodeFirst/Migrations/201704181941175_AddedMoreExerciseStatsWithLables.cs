namespace GymDiaryCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMoreExerciseStatsWithLables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "ExerciseStats10_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats11_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats12_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats13_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats14_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats15_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats3_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats4_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats5_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats6_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats7_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats8_ExerciseStatsId", c => c.Int());
            AddColumn("dbo.Workouts", "ExerciseStats9_ExerciseStatsId", c => c.Int());
            CreateIndex("dbo.Workouts", "ExerciseStats10_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats11_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats12_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats13_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats14_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats15_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats3_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats4_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats5_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats6_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats7_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats8_ExerciseStatsId");
            CreateIndex("dbo.Workouts", "ExerciseStats9_ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats10_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats11_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats12_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats13_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats14_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats15_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats3_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats4_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats5_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats6_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats7_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats8_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
            AddForeignKey("dbo.Workouts", "ExerciseStats9_ExerciseStatsId", "dbo.ExerciseStats", "ExerciseStatsId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "ExerciseStats9_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats8_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats7_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats6_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats5_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats4_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats3_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats15_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats14_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats13_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats12_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats11_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats10_ExerciseStatsId", "dbo.ExerciseStats");
            DropIndex("dbo.Workouts", new[] { "ExerciseStats9_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats8_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats7_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats6_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats5_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats4_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats3_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats15_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats14_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats13_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats12_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats11_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats10_ExerciseStatsId" });
            DropColumn("dbo.Workouts", "ExerciseStats9_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats8_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats7_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats6_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats5_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats4_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats3_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats15_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats14_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats13_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats12_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats11_ExerciseStatsId");
            DropColumn("dbo.Workouts", "ExerciseStats10_ExerciseStatsId");
        }
    }
}
