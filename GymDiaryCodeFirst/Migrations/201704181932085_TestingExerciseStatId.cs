namespace GymDiaryCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestingExerciseStatId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Workouts", name: "ExerciseStats1Id", newName: "ExerciseStats1_ExerciseStatsId");
            RenameIndex(table: "dbo.Workouts", name: "IX_ExerciseStats1Id", newName: "IX_ExerciseStats1_ExerciseStatsId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Workouts", name: "IX_ExerciseStats1_ExerciseStatsId", newName: "IX_ExerciseStats1Id");
            RenameColumn(table: "dbo.Workouts", name: "ExerciseStats1_ExerciseStatsId", newName: "ExerciseStats1Id");
        }
    }
}
