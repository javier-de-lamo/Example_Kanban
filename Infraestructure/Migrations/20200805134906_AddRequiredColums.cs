namespace Infraestructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddRequiredColums : Migration
    {
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AlterColumn< string >
            (
                "Details", "TaskItems", nullable: false, oldClrType: typeof( string ), oldType: "nvarchar(max)",
                oldNullable: true
            );
        }

        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AlterColumn< string >
                ( "Details", "TaskItems", "nvarchar(max)", nullable: true, oldClrType: typeof( string ) );
        }
    }
}