﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GourmetShopWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class mergeupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*  migrationBuilder.AddColumn<int>(
                  name: "CategoryId",
                  table: "Product",
                  type: "int",
                  nullable: true);

              migrationBuilder.CreateTable(
                  name: "Categories",
                  columns: table => new
                  {
                      Id = table.Column<int>(type: "int", nullable: false)
                          .Annotation("SqlServer:Identity", "1, 1"),
                      CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                  },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_Categories", x => x.Id);
                  });

              migrationBuilder.CreateIndex(
                  name: "IX_Product_CategoryId",
                  table: "Product",
                  column: "CategoryId");

              migrationBuilder.AddForeignKey(
                  name: "FK_Product_Categories_CategoryId",
                  table: "Product",
                  column: "CategoryId",
                  principalTable: "Categories",
                  principalColumn: "Id");*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                 name: "FK_Product_Categories_CategoryId",
                 table: "Product");

             migrationBuilder.DropTable(
                 name: "Categories");

             migrationBuilder.DropIndex(
                 name: "IX_Product_CategoryId",
                 table: "Product");

             migrationBuilder.DropColumn(
                 name: "CategoryId",
                 table: "Product");*/
        }
    }
}
