using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace libraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_Authors_Authors_authorsAuthorID",
                table: "books_Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_books_Authors_Books_booksBookID",
                table: "books_Authors");

            migrationBuilder.DropIndex(
                name: "IX_books_Authors_authorsAuthorID",
                table: "books_Authors");

            migrationBuilder.DropIndex(
                name: "IX_books_Authors_booksBookID",
                table: "books_Authors");

            migrationBuilder.DropColumn(
                name: "authorsAuthorID",
                table: "books_Authors");

            migrationBuilder.DropColumn(
                name: "booksBookID",
                table: "books_Authors");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRead",
                table: "Books",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorID", "Fullname" },
                values: new object[,]
                {
                    { 1, "Nguyễn Văn A" },
                    { 2, "Trần Thị B" },
                    { 3, "Phạm Văn C" }
                });

            migrationBuilder.InsertData(
                table: "publishers",
                columns: new[] { "publishersId", "publishersName" },
                values: new object[,]
                {
                    { 1, "Nhà xuất bản A" },
                    { 2, "Nhà xuất bản B" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "CoverUrl", "DateAdded", "DateRead", "Genre", "Isread", "Rate", "description", "publishersId", "title" },
                values: new object[,]
                {
                    { 1, "url_sach_1.jpg", new DateTime(2024, 5, 7, 8, 30, 22, 630, DateTimeKind.Local).AddTicks(568), new DateTime(2024, 5, 7, 8, 30, 22, 630, DateTimeKind.Local).AddTicks(551), "Thể loại 1", true, 5, "Mô tả sách 1", 1, "Sách 1" },
                    { 2, "url_sach_2.jpg", new DateTime(2024, 5, 7, 8, 30, 22, 630, DateTimeKind.Local).AddTicks(571), null, "Thể loại 2", false, 4, "Mô tả sách 2", 2, "Sách 2" }
                });

            migrationBuilder.InsertData(
                table: "books_Authors",
                columns: new[] { "Books_AuthorsID", "AuthorID", "BookID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_Authors_AuthorID",
                table: "books_Authors",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_books_Authors_BookID",
                table: "books_Authors",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_books_Authors_Authors_AuthorID",
                table: "books_Authors",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_Authors_Books_BookID",
                table: "books_Authors",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_Authors_Authors_AuthorID",
                table: "books_Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_books_Authors_Books_BookID",
                table: "books_Authors");

            migrationBuilder.DropIndex(
                name: "IX_books_Authors_AuthorID",
                table: "books_Authors");

            migrationBuilder.DropIndex(
                name: "IX_books_Authors_BookID",
                table: "books_Authors");

            migrationBuilder.DeleteData(
                table: "books_Authors",
                keyColumn: "Books_AuthorsID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "books_Authors",
                keyColumn: "Books_AuthorsID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "books_Authors",
                keyColumn: "Books_AuthorsID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "publishers",
                keyColumn: "publishersId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "publishers",
                keyColumn: "publishersId",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "authorsAuthorID",
                table: "books_Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "booksBookID",
                table: "books_Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRead",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_books_Authors_authorsAuthorID",
                table: "books_Authors",
                column: "authorsAuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_books_Authors_booksBookID",
                table: "books_Authors",
                column: "booksBookID");

            migrationBuilder.AddForeignKey(
                name: "FK_books_Authors_Authors_authorsAuthorID",
                table: "books_Authors",
                column: "authorsAuthorID",
                principalTable: "Authors",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_Authors_Books_booksBookID",
                table: "books_Authors",
                column: "booksBookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
