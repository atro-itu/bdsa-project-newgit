using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NEWgIT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIDFromCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Commits",
                table: "Commits");

            migrationBuilder.DropIndex(
                name: "IX_Commits_Id",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Commits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commits",
                table: "Commits",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_Commits_Hash",
                table: "Commits",
                column: "Hash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Commits",
                table: "Commits");

            migrationBuilder.DropIndex(
                name: "IX_Commits_Hash",
                table: "Commits");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Commits",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commits",
                table: "Commits",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Commits_Id",
                table: "Commits",
                column: "Id",
                unique: true);
        }
    }
}
