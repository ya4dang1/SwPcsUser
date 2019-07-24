using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class SetOptionalFieldForUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_FileLibraries_IDFileId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "IDIssuanceDate",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<Guid>(
                name: "IDFileId",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<DateTime>(
                name: "IDExpiryDate",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "UserCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_FileLibraries_IDFileId",
                table: "UserProfiles",
                column: "IDFileId",
                principalTable: "FileLibraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_FileLibraries_IDFileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "UserCards");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "UserProfiles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IDIssuanceDate",
                table: "UserProfiles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IDFileId",
                table: "UserProfiles",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IDExpiryDate",
                table: "UserProfiles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "UserProfiles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_FileLibraries_IDFileId",
                table: "UserProfiles",
                column: "IDFileId",
                principalTable: "FileLibraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
