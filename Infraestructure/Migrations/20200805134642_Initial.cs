namespace Infraestructure.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Initial : Migration
    {
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.CreateTable
            (
                "TaskItems",
                table => new
                {
                    Id            = table.Column< int >( nullable: false ).Annotation( "SqlServer:Identity", "1, 1" ),
                    Details       = table.Column< string >( nullable: true ),
                    CurrentStatus = table.Column< int >( nullable: false ),
                    Created       = table.Column< DateTime >( nullable: false ),
                    LastUpdated   = table.Column< DateTime >( nullable: false )
                }, constraints: table => { table.PrimaryKey( "PK_TaskItems", x => x.Id ); }
            );
        }

        protected override void Down( MigrationBuilder migrationBuilder ) { migrationBuilder.DropTable( "TaskItems" ); }
    }
}