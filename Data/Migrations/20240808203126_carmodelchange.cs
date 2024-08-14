using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autokereskedes_BS.Migrations
{
    /// <inheritdoc />
    public partial class carmodelchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_CarModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_CarBrandId",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Models",
                table: "Models");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "CarModels");

            migrationBuilder.RenameIndex(
                name: "IX_Models_CarBrandId",
                table: "CarModels",
                newName: "IX_CarModels_CarBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModels",
                table: "CarModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_Brands_CarBrandId",
                table: "CarModels",
                column: "CarBrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars",
                column: "CarModelId",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_Brands_CarBrandId",
                table: "CarModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModels",
                table: "CarModels");

            migrationBuilder.RenameTable(
                name: "CarModels",
                newName: "Models");

            migrationBuilder.RenameIndex(
                name: "IX_CarModels_CarBrandId",
                table: "Models",
                newName: "IX_Models_CarBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Models",
                table: "Models",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_CarModelId",
                table: "Cars",
                column: "CarModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_CarBrandId",
                table: "Models",
                column: "CarBrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
