using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateCoverTypesTableInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoverTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoverTypes_Name",
                table: "CoverTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoverTypes");
        }
    }
}
