using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zoomnews.Database.Migrations
{
    public partial class InitZoomnewsDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityId = table.Column<Guid>(nullable: false),
                    EntityName = table.Column<string>(maxLength: 200, nullable: false),
                    Changes = table.Column<string>(nullable: false),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    ChangedByUserId = table.Column<Guid>(nullable: false),
                    ChangedByUserName = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false),
                    ParentName = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Type = table.Column<string>(maxLength: 20, nullable: false),
                    Language = table.Column<string>(maxLength: 80, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    Image = table.Column<string>(maxLength: 256, nullable: true),
                    Url = table.Column<string>(maxLength: 256, nullable: true),
                    HasChildren = table.Column<bool>(nullable: false),
                    HasUrl = table.Column<bool>(nullable: false),
                    Index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    CreatedByName = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedById = table.Column<Guid>(nullable: true),
                    UpdatedByName = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedById = table.Column<Guid>(nullable: true),
                    DeletedByName = table.Column<string>(maxLength: 256, nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(maxLength: 4000, nullable: false),
                    Ext = table.Column<string>(maxLength: 4000, nullable: true),
                    ReferenceId = table.Column<Guid>(nullable: false),
                    ReferenceType = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    SEOName = table.Column<string>(maxLength: 4000, nullable: true),
                    SEOTitle = table.Column<string>(maxLength: 4000, nullable: true),
                    SEODescription = table.Column<string>(maxLength: 4000, nullable: true),
                    SEOKeywords = table.Column<string>(maxLength: 4000, nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    CreatedByName = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedById = table.Column<Guid>(nullable: true),
                    UpdatedByName = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedById = table.Column<Guid>(nullable: true),
                    DeletedByName = table.Column<string>(maxLength: 256, nullable: true),
                    CategoryArticleId = table.Column<Guid>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    Image = table.Column<string>(maxLength: 256, nullable: true),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    BriefContent = table.Column<string>(maxLength: 4000, nullable: true),
                    FullContent = table.Column<string>(nullable: true),
                    Source = table.Column<string>(maxLength: 256, nullable: true),
                    Index = table.Column<int>(nullable: true),
                    IsVisible = table.Column<bool>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    Position = table.Column<int>(nullable: true),
                    Ext = table.Column<string>(nullable: true),
                    Ext1 = table.Column<string>(nullable: true),
                    Ext2 = table.Column<string>(nullable: true),
                    Ext3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Category_CategoryArticleId",
                        column: x => x.CategoryArticleId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    SEOName = table.Column<string>(maxLength: 4000, nullable: true),
                    SEOTitle = table.Column<string>(maxLength: 4000, nullable: true),
                    SEODescription = table.Column<string>(maxLength: 4000, nullable: true),
                    SEOKeywords = table.Column<string>(maxLength: 4000, nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    CreatedByName = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedById = table.Column<Guid>(nullable: true),
                    UpdatedByName = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedById = table.Column<Guid>(nullable: true),
                    DeletedByName = table.Column<string>(maxLength: 256, nullable: true),
                    CategoryMediaId = table.Column<Guid>(nullable: false),
                    CategoryMediaName = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Source = table.Column<string>(maxLength: 256, nullable: true),
                    Type = table.Column<string>(maxLength: 20, nullable: true),
                    Ext = table.Column<string>(maxLength: 256, nullable: true),
                    ReferenceId = table.Column<Guid>(nullable: false),
                    ReferenceType = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Link = table.Column<string>(maxLength: 256, nullable: true),
                    ReferenceSource = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_Category_CategoryMediaId",
                        column: x => x.CategoryMediaId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryArticleId",
                table: "Article",
                column: "CategoryArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Title_SEOName",
                table: "Article",
                columns: new[] { "Title", "SEOName" },
                unique: true,
                filter: "[SEOName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Content",
                table: "Comment",
                column: "Content",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentId",
                table: "Comment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_CategoryMediaId",
                table: "Media",
                column: "CategoryMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Name_Id",
                table: "Media",
                columns: new[] { "Name", "Id" },
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Audit");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
