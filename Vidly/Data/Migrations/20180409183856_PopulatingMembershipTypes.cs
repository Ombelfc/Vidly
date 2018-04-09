using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vidly.Data.Migrations
{
    public partial class PopulatingMembershipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MembershipType (Id, SignUpFee, DurationInMonths, Discout) VALUES (1, 0, 0, 0)");
            migrationBuilder.Sql("INSERT INTO MembershipType (Id, SignUpFee, DurationInMonths, Discout) VALUES (2, 30, 1, 10)");
            migrationBuilder.Sql("INSERT INTO MembershipType (Id, SignUpFee, DurationInMonths, Discout) VALUES (3, 90, 3, 15)");
            migrationBuilder.Sql("INSERT INTO MembershipType (Id, SignUpFee, DurationInMonths, Discout) VALUES (4, 300, 12, 20)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
