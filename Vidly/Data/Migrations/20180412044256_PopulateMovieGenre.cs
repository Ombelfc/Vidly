using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vidly.Data.Migrations
{
    public partial class PopulateMovieGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genre (Id, Type) VALUES (1, 'Action')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Type) VALUES (2, 'Thriller')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Type) VALUES (3, 'Family')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Type) VALUES (4, 'Romance')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Type) VALUES (5, 'Comedy')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
