using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Product",
                columns: table => new
                {
                    IdProduct = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    AliasName = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IdUserCreatedBy = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IdUserUpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    IdRefObjectState = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.IdProduct);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Security",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AliasName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IdRefObjectState = table.Column<long>(nullable: false),
                    Password = table.Column<string>(maxLength: 150, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNo = table.Column<string>(maxLength: 50, nullable: true),
                    IdUserCreatedBy = table.Column<Guid>(nullable: true),
                    IdUserUpdatedBy = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_User_User_IdUserCreatedBy",
                        column: x => x.IdUserCreatedBy,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_User_User_IdUserUpdatedBy",
                        column: x => x.IdUserUpdatedBy,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "RefObjectState",
                schema: "dbo",
                columns: table => new
                {
                    IdRefObjectState = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AliasName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsDisplay = table.Column<bool>(nullable: false),
                    IdUserCreatedBy = table.Column<Guid>(nullable: true),
                    IdUserUpdatedBy = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefObjectState", x => x.IdRefObjectState);
                    table.ForeignKey(
                        name: "FK_RefObjectState_User_IdUserCreatedBy",
                        column: x => x.IdUserCreatedBy,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_RefObjectState_User_IdUserUpdatedBy",
                        column: x => x.IdUserUpdatedBy,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "IdUser");
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "RefObjectState",
                columns: new[] { "IdRefObjectState", "AliasName", "CreatedDate", "Description", "IdUserCreatedBy", "IdUserUpdatedBy", "IsDefault", "IsDisplay", "Name", "UpdatedDate" },
                values: new object[] { 1L, "Active", null, "Active", null, null, true, false, "Active", null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "RefObjectState",
                columns: new[] { "IdRefObjectState", "AliasName", "CreatedDate", "Description", "IdUserCreatedBy", "IdUserUpdatedBy", "IsDefault", "IsDisplay", "Name", "UpdatedDate" },
                values: new object[] { 2L, "Terminated", null, "Terminated", null, null, false, false, "Terminated", null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "RefObjectState",
                columns: new[] { "IdRefObjectState", "AliasName", "CreatedDate", "Description", "IdUserCreatedBy", "IdUserUpdatedBy", "IsDefault", "IsDisplay", "Name", "UpdatedDate" },
                values: new object[] { 3L, "Frozen", null, "Frozen", null, null, false, false, "Frozen", null });

            migrationBuilder.InsertData(
                schema: "Product",
                table: "Product",
                columns: new[] { "IdProduct", "AliasName", "CreatedDate", "Description", "IdRefObjectState", "IdUserCreatedBy", "IdUserUpdatedBy", "Name", "UpdatedDate" },
                values: new object[] { new Guid("8beca0a3-dde0-4af4-9197-c669a2ba754f"), "Product 1", null, "Product 1", 1L, null, null, "Product 1", null });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "User",
                columns: new[] { "IdUser", "AliasName", "CreatedDate", "Description", "DisplayName", "Email", "FirstName", "IdRefObjectState", "IdUserCreatedBy", "IdUserUpdatedBy", "LastName", "MiddleName", "Name", "Password", "PhoneNo", "UpdatedDate" },
                values: new object[] { new Guid("48c9419f-6872-4167-9a07-ce63a1644328"), "Admin", new DateTime(2024, 5, 11, 18, 44, 18, 419, DateTimeKind.Local).AddTicks(3208), "Admin user", "Admin", "admin@example.com", "Admin", 1L, null, null, "Admin", "Bin", "Admin", "admin123", "0123456789", new DateTime(2024, 5, 11, 18, 44, 18, 420, DateTimeKind.Local).AddTicks(4393) });

            migrationBuilder.CreateIndex(
                name: "IX_RefObjectState_IdUserCreatedBy",
                schema: "dbo",
                table: "RefObjectState",
                column: "IdUserCreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RefObjectState_IdUserUpdatedBy",
                schema: "dbo",
                table: "RefObjectState",
                column: "IdUserUpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RefObjectState_Name",
                schema: "dbo",
                table: "RefObjectState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdRefObjectState",
                schema: "Product",
                table: "Product",
                column: "IdRefObjectState");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdUserCreatedBy",
                schema: "Product",
                table: "Product",
                column: "IdUserCreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdUserUpdatedBy",
                schema: "Product",
                table: "Product",
                column: "IdUserUpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                schema: "Product",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IdRefObjectState",
                schema: "Security",
                table: "User",
                column: "IdRefObjectState");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdUserCreatedBy",
                schema: "Security",
                table: "User",
                column: "IdUserCreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdUserUpdatedBy",
                schema: "Security",
                table: "User",
                column: "IdUserUpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_Name",
                schema: "Security",
                table: "User",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_IdUserCreatedBy",
                schema: "Product",
                table: "Product",
                column: "IdUserCreatedBy",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_IdUserUpdatedBy",
                schema: "Product",
                table: "Product",
                column: "IdUserUpdatedBy",
                principalSchema: "Security",
                principalTable: "User",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_RefObjectState_IdRefObjectState",
                schema: "Product",
                table: "Product",
                column: "IdRefObjectState",
                principalSchema: "dbo",
                principalTable: "RefObjectState",
                principalColumn: "IdRefObjectState");

            migrationBuilder.AddForeignKey(
                name: "FK_User_RefObjectState_IdRefObjectState",
                schema: "Security",
                table: "User",
                column: "IdRefObjectState",
                principalSchema: "dbo",
                principalTable: "RefObjectState",
                principalColumn: "IdRefObjectState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefObjectState_User_IdUserCreatedBy",
                schema: "dbo",
                table: "RefObjectState");

            migrationBuilder.DropForeignKey(
                name: "FK_RefObjectState_User_IdUserUpdatedBy",
                schema: "dbo",
                table: "RefObjectState");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "RefObjectState",
                schema: "dbo");
        }
    }
}
