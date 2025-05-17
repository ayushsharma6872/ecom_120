using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecom_120.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initLoadStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_CreateCoverType 
                                   @name varchar(50)
                                   AS 
                                   insert CoverTypes values(@name)");
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_UpdateCoverType
                                   @id int,
                                   @name varchar(50)
                                   AS 
                                   update CoverType set name=@name where id=@id");
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_DeleteCoverType 
                                   @id int
                                   
                                   AS 
                                   delete from CoverTypes where id=@id");
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetCoverTypes
                                   
                                   AS 
                                   select * from CoverTypes ");
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetCoverType 
                                   @id int
                                   AS 
                                   select * from CoverTypes where id=@id");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Covertypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Covertypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
