using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMentor.Persistence.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Roles",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Roles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Roles",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PasswordResets",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PasswordResets", x => x.Id);
                table.ForeignKey(
                    name: "FK_PasswordResets_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_PasswordResets_UserId",
            table: "PasswordResets",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_RoleId",
            table: "Users",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_Username",
            table: "Users",
            column: "Username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PasswordResets");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "Roles");
    }
}
