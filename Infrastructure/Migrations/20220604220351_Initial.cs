using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    PatientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tickets_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "ID", "IsDelete", "Name", "phoneNumber" },
                values: new object[,]
                {
                    { new Guid("dde4ba50-808e-479f-be8b-72f69913442f"), false, "ahmed1", "01025882940" },
                    { new Guid("dde4ba51-808e-479f-be8b-72f69913442f"), false, "ahmed2", "01025882941" },
                    { new Guid("dde4ba52-808e-479f-be8b-72f69913442f"), false, "ahmed3", "01025882942" },
                    { new Guid("dde4ba53-808e-479f-be8b-72f69913442f"), false, "ahmed4", "01025882943" },
                    { new Guid("dde4ba54-808e-479f-be8b-72f69913442f"), false, "ahmed5", "01025882944" },
                    { new Guid("dde4ba55-808e-479f-be8b-72f69913442f"), false, "ahmed6", "01025882945" },
                    { new Guid("dde4ba56-808e-479f-be8b-72f69913442f"), false, "ahmed7", "01025882946" },
                    { new Guid("dde4ba57-808e-479f-be8b-72f69913442f"), false, "ahmed8", "01025882947" },
                    { new Guid("dde4ba58-808e-479f-be8b-72f69913442f"), false, "ahmed9", "01025882948" },
                    { new Guid("dde4ba59-808e-479f-be8b-72f69913442f"), false, "ahmed10", "01025882949" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "IsActive", "IsDelete", "Password", "UserName" },
                values: new object[] { new Guid("bcc2500a-7632-4d6c-a212-e2b28a18291e"), true, false, "$2b$10$ADfArRlGOuMD/oVYVnoSqujORyXM5rw8oAHPREfegbSRwqmfDIMI2", "admin" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "ID", "IsDelete", "Number", "PatientID", "Time" },
                values: new object[,]
                {
                    { new Guid("1925c89b-e71c-4414-91cd-73870dbb9ef6"), false, "9", new Guid("dde4ba58-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(494) },
                    { new Guid("489692ab-1ca3-44a3-9de9-9f38243cff6a"), false, "4", new Guid("dde4ba53-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(454) },
                    { new Guid("52505b50-5889-475e-8002-9f9c2ce83539"), false, "1", new Guid("dde4ba50-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(429) },
                    { new Guid("76b708dc-a804-430e-94f3-86bde31ace8f"), false, "10", new Guid("dde4ba59-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(498) },
                    { new Guid("86363b2c-9128-4c31-872f-ed0d5c814938"), false, "7", new Guid("dde4ba56-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(486) },
                    { new Guid("86597129-4733-4545-99e4-dbc06c70430a"), false, "5", new Guid("dde4ba54-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(458) },
                    { new Guid("9b97b19e-94c0-4167-b2fa-17c27e5b51d1"), false, "2", new Guid("dde4ba51-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(446) },
                    { new Guid("aeb95a58-b339-4d95-bdb3-745b3e059c09"), false, "3", new Guid("dde4ba52-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(450) },
                    { new Guid("d23945b7-fe8c-4d50-bba0-d55f0433b269"), false, "8", new Guid("dde4ba57-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(490) },
                    { new Guid("f899a6d2-baa4-4463-944c-99047d3f49fa"), false, "6", new Guid("dde4ba55-808e-479f-be8b-72f69913442f"), new DateTime(2022, 6, 5, 0, 3, 50, 928, DateTimeKind.Local).AddTicks(462) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_phoneNumber",
                table: "Patients",
                column: "phoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PatientID",
                table: "Tickets",
                column: "PatientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
