using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Licenta.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    idCustomer = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    firstName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    lastName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    phone = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    username = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.idCustomer);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    idFacilities = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    facilitiesName = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.idFacilities);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    idHotel = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descriptionTable = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    HotelName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Stars = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.idHotel);
                });

            migrationBuilder.CreateTable(
                name: "FacilitiesHotel",
                columns: table => new
                {
                    idHotel = table.Column<int>(nullable: false),
                    idFacilities = table.Column<int>(nullable: false),
                    quantiy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilitiesHotel", x => new { x.idHotel, x.idFacilities });
                    table.ForeignKey(
                        name: "FK__Facilitie__idFac__628FA481",
                        column: x => x.idFacilities,
                        principalTable: "Facilities",
                        principalColumn: "idFacilities",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Facilitie__idHot__619B8048",
                        column: x => x.idHotel,
                        principalTable: "Hotels",
                        principalColumn: "idHotel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    idLocation = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idHotel = table.Column<int>(nullable: true),
                    nrStreat = table.Column<int>(nullable: true),
                    regionName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    streatName = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.idLocation);
                    table.ForeignKey(
                        name: "FK__Location__idHote__5165187F",
                        column: x => x.idHotel,
                        principalTable: "Hotels",
                        principalColumn: "idHotel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    idRoom = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bath = table.Column<bool>(nullable: true),
                    Beds = table.Column<int>(nullable: true),
                    idHotel = table.Column<int>(nullable: true),
                    reserved = table.Column<bool>(nullable: true),
                    roomNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.idRoom);
                    table.ForeignKey(
                        name: "FK__Rooms__idHotel__4E88ABD4",
                        column: x => x.idHotel,
                        principalTable: "Hotels",
                        principalColumn: "idHotel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    idReservations = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    check_in = table.Column<DateTime>(type: "date", nullable: true),
                    check_out = table.Column<DateTime>(type: "date", nullable: true),
                    idCustomer = table.Column<int>(nullable: true),
                    idRoom = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.idReservations);
                    table.ForeignKey(
                        name: "FK__Reservati__idCus__5AEE82B9",
                        column: x => x.idCustomer,
                        principalTable: "Customers",
                        principalColumn: "idCustomer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Reservati__idRoo__5441852A",
                        column: x => x.idRoom,
                        principalTable: "Rooms",
                        principalColumn: "idRoom",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacilitiesHotel_idFacilities",
                table: "FacilitiesHotel",
                column: "idFacilities");

            migrationBuilder.CreateIndex(
                name: "IX_Location_idHotel",
                table: "Location",
                column: "idHotel");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_idCustomer",
                table: "Reservations",
                column: "idCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_idRoom",
                table: "Reservations",
                column: "idRoom");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_idHotel",
                table: "Rooms",
                column: "idHotel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilitiesHotel");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
