namespace CinemaSharpAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            //Seeding users
            //admin@cinemasharp.com Pass123!
            //guest@cinemasharp.com Pass123!
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ceecd867-719d-4d95-9f6d-8168c26f1a93', N'admin@cinemasharp.com', 0, N'AAY56sJRxKSEFdLVe2k2x77Ibyx6LBYpN9gfB45kXGaH7YOxW++wjWXBLgtLasiwpw==', N'259b06f1-9e2c-4fb0-a55e-2eb90cabdd71', NULL, 0, 0, NULL, 1, 0, N'admin@cinemasharp.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ffdb7e07-a356-4332-9a86-1483ac11aceb', N'guest@cinemasharp.com', 0, N'AJbZs3dV1HuZzUluQSyOqmUYo6eZ/qgiZW2JCno65510LKYg3UszIRhoE0zmyzw3DQ==', N'47df65e9-a82b-49a0-b4d5-1ffc3e839361', NULL, 0, 0, NULL, 1, 0, N'guest@cinemasharp.com')  
            ");

            //Seeding roles
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a6d0866b-3b1f-432d-a1f1-4c8f202ffd7b', N'CanManageMovies')");

            //Adding role to user admin
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ceecd867-719d-4d95-9f6d-8168c26f1a93', N'a6d0866b-3b1f-432d-a1f1-4c8f202ffd7b')");
        }
        
        public override void Down()
        {
        }
    }
}
