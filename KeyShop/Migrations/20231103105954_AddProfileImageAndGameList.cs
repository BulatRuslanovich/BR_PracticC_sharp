using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyShop.Migrations {
    /// <inheritdoc />
    public partial class AddProfileImageAndGameList : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<List<string>>(name: "GameIdList", table: "AspNetUsers", type: "text[]",
                                                     nullable: true);

            migrationBuilder.AddColumn<string>(name: "ProfileImageUrl", table: "AspNetUsers", type: "text",
                                               nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(name: "GameIdList", table: "AspNetUsers");

            migrationBuilder.DropColumn(name: "ProfileImageUrl", table: "AspNetUsers");
        }
    }
}
