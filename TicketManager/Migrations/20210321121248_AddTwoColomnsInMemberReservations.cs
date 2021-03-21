using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketManager.Migrations
{
    public partial class AddTwoColomnsInMemberReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dramas_AspNetUsers_UserId",
                table: "Dramas");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberReservations_Stages_DramaName_StageNum",
                table: "MemberReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_OutsideReservations_Stages_DramaName_StageNum",
                table: "OutsideReservations");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Stages",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuestName",
                table: "OutsideReservations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DramaName",
                table: "OutsideReservations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GuestName",
                table: "MemberReservations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DramaName",
                table: "MemberReservations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "MemberReservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "MemberReservations",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Dramas",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "NotifiedMemberIds",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifiedMemberIds", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Dramas_AspNetUsers_UserId",
                table: "Dramas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberReservations_Stages_DramaName_StageNum",
                table: "MemberReservations",
                columns: new[] { "DramaName", "StageNum" },
                principalTable: "Stages",
                principalColumns: new[] { "DramaName", "Num" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutsideReservations_Stages_DramaName_StageNum",
                table: "OutsideReservations",
                columns: new[] { "DramaName", "StageNum" },
                principalTable: "Stages",
                principalColumns: new[] { "DramaName", "Num" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dramas_AspNetUsers_UserId",
                table: "Dramas");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberReservations_Stages_DramaName_StageNum",
                table: "MemberReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_OutsideReservations_Stages_DramaName_StageNum",
                table: "OutsideReservations");

            migrationBuilder.DropTable(
                name: "NotifiedMemberIds");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "MemberReservations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "MemberReservations");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Stages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "GuestName",
                table: "OutsideReservations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DramaName",
                table: "OutsideReservations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "GuestName",
                table: "MemberReservations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DramaName",
                table: "MemberReservations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Dramas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Dramas_AspNetUsers_UserId",
                table: "Dramas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberReservations_Stages_DramaName_StageNum",
                table: "MemberReservations",
                columns: new[] { "DramaName", "StageNum" },
                principalTable: "Stages",
                principalColumns: new[] { "DramaName", "Num" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OutsideReservations_Stages_DramaName_StageNum",
                table: "OutsideReservations",
                columns: new[] { "DramaName", "StageNum" },
                principalTable: "Stages",
                principalColumns: new[] { "DramaName", "Num" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
