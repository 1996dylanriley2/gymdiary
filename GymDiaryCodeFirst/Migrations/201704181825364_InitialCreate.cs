namespace GymDiaryCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PrimaryMuscleId = c.Int(nullable: false),
                        SecondaryMuscleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Muscles", t => t.PrimaryMuscleId, cascadeDelete: true)
                .ForeignKey("dbo.Muscles", t => t.SecondaryMuscleId)
                .Index(t => t.PrimaryMuscleId)
                .Index(t => t.SecondaryMuscleId);
            
            CreateTable(
                "dbo.Muscles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExerciseStats",
                c => new
                    {
                        ExerciseStatsId = c.Int(nullable: false, identity: true),
                        ExerciseId = c.Int(nullable: false),
                        WorkoutId = c.Int(nullable: false),
                        WeightInKg = c.Single(),
                        Reps = c.String(),
                        Sets = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseStatsId)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .ForeignKey("dbo.Workouts", t => t.WorkoutId, cascadeDelete: true)
                .Index(t => t.ExerciseId)
                .Index(t => t.WorkoutId);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        WorkoutId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Date = c.DateTime(),
                        ExerciseStats1Id = c.Int(),
                        ExerciseStats2_ExerciseStatsId = c.Int(),
                    })
                .PrimaryKey(t => t.WorkoutId)
                .ForeignKey("dbo.ExerciseStats", t => t.ExerciseStats1Id)
                .ForeignKey("dbo.ExerciseStats", t => t.ExerciseStats2_ExerciseStatsId)
                .Index(t => t.ExerciseStats1Id)
                .Index(t => t.ExerciseStats2_ExerciseStatsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseStats", "WorkoutId", "dbo.Workouts");
            DropForeignKey("dbo.Workouts", "ExerciseStats2_ExerciseStatsId", "dbo.ExerciseStats");
            DropForeignKey("dbo.Workouts", "ExerciseStats1Id", "dbo.ExerciseStats");
            DropForeignKey("dbo.ExerciseStats", "ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.Exercises", "SecondaryMuscleId", "dbo.Muscles");
            DropForeignKey("dbo.Exercises", "PrimaryMuscleId", "dbo.Muscles");
            DropIndex("dbo.Workouts", new[] { "ExerciseStats2_ExerciseStatsId" });
            DropIndex("dbo.Workouts", new[] { "ExerciseStats1Id" });
            DropIndex("dbo.ExerciseStats", new[] { "WorkoutId" });
            DropIndex("dbo.ExerciseStats", new[] { "ExerciseId" });
            DropIndex("dbo.Exercises", new[] { "SecondaryMuscleId" });
            DropIndex("dbo.Exercises", new[] { "PrimaryMuscleId" });
            DropTable("dbo.Workouts");
            DropTable("dbo.ExerciseStats");
            DropTable("dbo.Muscles");
            DropTable("dbo.Exercises");
        }
    }
}
