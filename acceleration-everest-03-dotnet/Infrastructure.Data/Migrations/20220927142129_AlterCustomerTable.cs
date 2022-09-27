using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class AlterCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customers");

            migrationBuilder.RenameColumn(
                name: "whatsapp",
                table: "Customers",
                newName: "Whatsapp");

            migrationBuilder.RenameColumn(
                name: "number",
                table: "Customers",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Customers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "Customers",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Customers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Customers",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "cellphone",
                table: "Customers",
                newName: "Cellphone");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Customers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "postal_code",
                table: "Customers",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "Customers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "email_sms",
                table: "Customers",
                newName: "EmailSms");

            migrationBuilder.RenameColumn(
                name: "date_of_birth",
                table: "Customers",
                newName: "DateOfBirth");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Customers",
                type: "VARCHAR(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers");

            migrationBuilder.RenameColumn(
                name: "Whatsapp",
                table: "customers",
                newName: "whatsapp");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "customers",
                newName: "number");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "customers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "customers",
                newName: "cpf");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "customers",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "customers",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Cellphone",
                table: "customers",
                newName: "cellphone");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "customers",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "customers",
                newName: "postal_code");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "customers",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "EmailSms",
                table: "customers",
                newName: "email_sms");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "customers",
                newName: "date_of_birth");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "customers",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "Id");
        }
    }
}
