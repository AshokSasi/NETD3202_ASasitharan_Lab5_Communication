using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NETD3202_ASasitharan_Lab5_Comm2.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    doctorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorfname = table.Column<string>(nullable: true),
                    doctorlname = table.Column<string>(nullable: true),
                    doctorphone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.doctorId);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    patientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientfname = table.Column<string>(nullable: true),
                    patientlname = table.Column<string>(nullable: true),
                    patientphone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.patientId);
                });

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    appointmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appointmentType = table.Column<string>(nullable: true),
                    appointmentDate = table.Column<DateTime>(nullable: false),
                    doctorfname = table.Column<string>(nullable: true),
                    doctorlname = table.Column<string>(nullable: true),
                    doctorId = table.Column<int>(nullable: false),
                    patientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.appointmentId);
                    table.ForeignKey(
                        name: "FK_appointment_doctors_doctorId",
                        column: x => x.doctorId,
                        principalTable: "doctors",
                        principalColumn: "doctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_patients_patientId",
                        column: x => x.patientId,
                        principalTable: "patients",
                        principalColumn: "patientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointment_doctorId",
                table: "appointment",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_patientId",
                table: "appointment",
                column: "patientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
