using EduMentor.Application.Common;
using EduMentor.Domain.Enum;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMentor.Persistence.Migrations;

/// <inheritdoc />
public partial class Seed_Users : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@$"
            INSERT INTO [Roles] VALUES 
            ('{PopulateDb.populateDto.AdminId}','{RoleEnum.Admin}',0), 
            ('{PopulateDb.populateDto.StudentId}','{RoleEnum.Student}',0), 
            ('{PopulateDb.populateDto.ParentId}','{RoleEnum.Parent}',0),
            ('{PopulateDb.populateDto.TeacherId}','{RoleEnum.Teacher}',0), 
            ('{PopulateDb.populateDto.DirectorId}','{RoleEnum.Director}',0);

            {PopulateDb.Populate()}
        ");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
