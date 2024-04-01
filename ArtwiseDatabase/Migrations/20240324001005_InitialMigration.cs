using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtwiseDatabase.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "addition_requests",
            columns: table => new
            {
                id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                author = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                image_url = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                source_url = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                author_is_ai = table.Column<bool>(type: "INTEGER", nullable: false),
                tags = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                date_added = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_addition_requests", x => x.id);
            },
            comment: "Represents a user art addition request.");

        migrationBuilder.CreateTable(
            name: "arts",
            columns: table => new
            {
                id = table.Column<Guid>(type: "TEXT", nullable: false),
                author = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                image_url = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                source_url = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                author_is_ai = table.Column<bool>(type: "INTEGER", nullable: false),
                is_accessible = table.Column<bool>(type: "INTEGER", nullable: false),
                date_added = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_arts", x => x.id);
            },
            comment: "Represents an art.");

        migrationBuilder.CreateTable(
            name: "users",
            columns: table => new
            {
                id = table.Column<Guid>(type: "TEXT", nullable: false),
                email = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                password_hash = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                type = table.Column<int>(type: "INTEGER", nullable: false),
                date_added = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_users", x => x.id);
            },
            comment: "Represents a user.");

        migrationBuilder.CreateTable(
            name: "removal_requests",
            columns: table => new
            {
                art_id = table.Column<Guid>(type: "TEXT", nullable: false),
                email = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                description = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                date_added = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_removal_requests", x => new { x.art_id, x.email });
                table.ForeignKey(
                    name: "fk_removal_requests_arts_art_id",
                    column: x => x.art_id,
                    principalTable: "arts",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Represents a user art removal request.");

        migrationBuilder.CreateTable(
            name: "tags",
            columns: table => new
            {
                art_id = table.Column<Guid>(type: "TEXT", nullable: false),
                tag = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                date_added = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tags", x => new { x.art_id, x.tag });
                table.ForeignKey(
                    name: "fk_tags_arts_art_id",
                    column: x => x.art_id,
                    principalTable: "arts",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Represents an art tag.");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "addition_requests");

        migrationBuilder.DropTable(
            name: "removal_requests");

        migrationBuilder.DropTable(
            name: "tags");

        migrationBuilder.DropTable(
            name: "users");

        migrationBuilder.DropTable(
            name: "arts");
    }
}