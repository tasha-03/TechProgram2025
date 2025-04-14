using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechProgram2025.Migrations
{
    /// <inheritdoc />
    public partial class Init_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCategories",
                columns: table => new
                {
                    InsuranceCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCategories", x => x.InsuranceCategoryID);
                    table.ForeignKey(
                        name: "FK_InsuranceCategories_InsuranceCategories_ParentCategoryID",
                        column: x => x.ParentCategoryID,
                        principalTable: "InsuranceCategories",
                        principalColumn: "InsuranceCategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceVariants",
                columns: table => new
                {
                    InsuranceVariantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryInsuranceCategoryID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceVariants", x => x.InsuranceVariantID);
                    table.ForeignKey(
                        name: "FK_InsuranceVariants_InsuranceCategories_CategoryInsuranceCategoryID",
                        column: x => x.CategoryInsuranceCategoryID,
                        principalTable: "InsuranceCategories",
                        principalColumn: "InsuranceCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    IsProblematic = table.Column<bool>(type: "bit", nullable: false),
                    AgentUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_Contracts_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_AgentUserID",
                        column: x => x.AgentUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractInsuranceVariant",
                columns: table => new
                {
                    ContractsContractID = table.Column<int>(type: "int", nullable: false),
                    InsuranceVariantsInsuranceVariantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInsuranceVariant", x => new { x.ContractsContractID, x.InsuranceVariantsInsuranceVariantID });
                    table.ForeignKey(
                        name: "FK_ContractInsuranceVariant_Contracts_ContractsContractID",
                        column: x => x.ContractsContractID,
                        principalTable: "Contracts",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractInsuranceVariant_InsuranceVariants_InsuranceVariantsInsuranceVariantID",
                        column: x => x.InsuranceVariantsInsuranceVariantID,
                        principalTable: "InsuranceVariants",
                        principalColumn: "InsuranceVariantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractInsuranceVariant_InsuranceVariantsInsuranceVariantID",
                table: "ContractInsuranceVariant",
                column: "InsuranceVariantsInsuranceVariantID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_AgentUserID",
                table: "Contracts",
                column: "AgentUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientID",
                table: "Contracts",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCategories_ParentCategoryID",
                table: "InsuranceCategories",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceVariants_CategoryInsuranceCategoryID",
                table: "InsuranceVariants",
                column: "CategoryInsuranceCategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractInsuranceVariant");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "InsuranceVariants");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "InsuranceCategories");
        }
    }
}
