using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechProgram2025.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Insurance_Category_Relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceVariants_InsuranceCategories_CategoryInsuranceCategoryID",
                table: "InsuranceVariants");

            migrationBuilder.RenameColumn(
                name: "CategoryInsuranceCategoryID",
                table: "InsuranceVariants",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_InsuranceVariants_CategoryInsuranceCategoryID",
                table: "InsuranceVariants",
                newName: "IX_InsuranceVariants_CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceVariants_InsuranceCategories_CategoryID",
                table: "InsuranceVariants",
                column: "CategoryID",
                principalTable: "InsuranceCategories",
                principalColumn: "InsuranceCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceVariants_InsuranceCategories_CategoryID",
                table: "InsuranceVariants");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "InsuranceVariants",
                newName: "CategoryInsuranceCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_InsuranceVariants_CategoryID",
                table: "InsuranceVariants",
                newName: "IX_InsuranceVariants_CategoryInsuranceCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceVariants_InsuranceCategories_CategoryInsuranceCategoryID",
                table: "InsuranceVariants",
                column: "CategoryInsuranceCategoryID",
                principalTable: "InsuranceCategories",
                principalColumn: "InsuranceCategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
