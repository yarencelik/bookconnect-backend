using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShelf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("2c47fdea-2638-49e4-9f75-6283fe17fb32"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("607cf571-234e-48ba-b46c-1d62cd804a86"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9df75e45-23c9-42c3-8c13-93bb1e2c2049"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d1580a6e-4e9b-4f98-8d4f-de6e73f84af0"));

            migrationBuilder.CreateTable(
                name: "Shelf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShelfName = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shelf_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookShelf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShelfId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookShelf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookShelf_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookShelf_Shelf_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Genre", "ISBN", "Pages", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("7b864770-92a1-4a75-8f8f-2a8811558f7e"), null, new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(123), "SampleGenre", "9781234567897", 300, "SampleTitle", new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(123) },
                    { new Guid("d22e0bd7-048a-4442-bee4-ebd6a189c6ea"), null, new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(165), "SampleGenre2", "9781234567444", 200, "SampleTitle2", new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(166) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Password", "Role", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("ce8f7fe1-7c79-441f-9a6e-041552fe5117"), new DateTime(2023, 5, 28, 10, 3, 40, 790, DateTimeKind.Utc).AddTicks(9440), "reader@reader.com", null, null, "$argon2id$v=19$m=65536,t=3,p=4$MFRlbno0YURlLUdWV2R2Z1JWZjk$L9ahKZ719vSNb9qGr2vMdi2WrUcBmdmf5YKFRzsn6c8", 2, new DateTime(2023, 5, 28, 10, 3, 40, 790, DateTimeKind.Utc).AddTicks(9443), "reader" },
                    { new Guid("da1cd637-6923-4e22-a597-55b8e811240b"), new DateTime(2023, 5, 28, 10, 3, 40, 396, DateTimeKind.Utc).AddTicks(4162), "admin@admin.com", null, null, "$argon2id$v=19$m=65536,t=3,p=4$MFRlbno0YURlLUdWV2R2Z1JWZjk$hHv/O3Uob/FVXCKwvoXfdD1pRVrlm4Y4gM9HnkRgLm4", 0, new DateTime(2023, 5, 28, 10, 3, 40, 396, DateTimeKind.Utc).AddTicks(4165), "admin" }
                });

            migrationBuilder.InsertData(
                table: "Shelf",
                columns: new[] { "Id", "CreatedAt", "OwnerId", "ShelfName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3f231a68-f48f-4ac1-b339-a95ac0df14d9"), new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(242), new Guid("da1cd637-6923-4e22-a597-55b8e811240b"), "Read", new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(243) },
                    { new Guid("72239fcb-91cd-41ea-bec7-51af80354756"), new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(248), new Guid("ce8f7fe1-7c79-441f-9a6e-041552fe5117"), "Read", new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(248) },
                    { new Guid("7ce327a5-f0c9-4ada-9185-2d95fb78a809"), new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(240), new Guid("da1cd637-6923-4e22-a597-55b8e811240b"), "Currently Reading", new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(240) },
                    { new Guid("82d2f019-d1c3-485b-aed2-dfd48068ca7e"), new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(246), new Guid("ce8f7fe1-7c79-441f-9a6e-041552fe5117"), "Currently Reading", new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(247) },
                    { new Guid("866800b0-4756-4d18-a49b-668b98f6ccb0"), new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(245), new Guid("ce8f7fe1-7c79-441f-9a6e-041552fe5117"), "Favorite", new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(245) },
                    { new Guid("96d2a8b7-5fd4-41ba-8b2e-6b4fb3e27244"), new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(227), new Guid("da1cd637-6923-4e22-a597-55b8e811240b"), "Favorite", new DateTime(2023, 5, 28, 10, 3, 40, 791, DateTimeKind.Utc).AddTicks(228) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookShelf_BookId",
                table: "BookShelf",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookShelf_ShelfId",
                table: "BookShelf",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Shelf_OwnerId",
                table: "Shelf",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookShelf");

            migrationBuilder.DropTable(
                name: "Shelf");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7b864770-92a1-4a75-8f8f-2a8811558f7e"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d22e0bd7-048a-4442-bee4-ebd6a189c6ea"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ce8f7fe1-7c79-441f-9a6e-041552fe5117"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("da1cd637-6923-4e22-a597-55b8e811240b"));

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Genre", "ISBN", "Pages", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2c47fdea-2638-49e4-9f75-6283fe17fb32"), null, new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(1300), "SampleGenre", "9781234567897", 300, "SampleTitle", new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(1301) },
                    { new Guid("607cf571-234e-48ba-b46c-1d62cd804a86"), null, new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(1325), "SampleGenre2", "9781234567444", 200, "SampleTitle2", new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(1326) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Password", "Role", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("9df75e45-23c9-42c3-8c13-93bb1e2c2049"), new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(600), "reader@reader.com", null, null, "$argon2id$v=19$m=65536,t=3,p=4$MFRlbno0YURlLUdWV2R2Z1JWZjk$L9ahKZ719vSNb9qGr2vMdi2WrUcBmdmf5YKFRzsn6c8", 2, new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(603), "reader" },
                    { new Guid("d1580a6e-4e9b-4f98-8d4f-de6e73f84af0"), new DateTime(2023, 5, 28, 1, 5, 33, 63, DateTimeKind.Utc).AddTicks(6318), "admin@admin.com", null, null, "$argon2id$v=19$m=65536,t=3,p=4$MFRlbno0YURlLUdWV2R2Z1JWZjk$hHv/O3Uob/FVXCKwvoXfdD1pRVrlm4Y4gM9HnkRgLm4", 0, new DateTime(2023, 5, 28, 1, 5, 33, 63, DateTimeKind.Utc).AddTicks(6321), "admin" }
                });
        }
    }
}
