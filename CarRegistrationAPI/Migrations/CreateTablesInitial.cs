using FluentMigrator;
using FluentMigrator.Postgres;
using System.Runtime.ConstrainedExecution;

namespace CarRegistrationAPI.Migrations
{
    [Migration(1)]
    public class CreateTablesInitial : Migration
    {
        public override void Up()
        {
            //Create.Table("Employee")
            //    .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
            //    .WithColumn("FirstName").AsString().NotNullable()
            //    .WithColumn("LastName").AsString().NotNullable()
            //    .WithColumn("Age").AsInt32().Nullable();

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "text", nullable: false),
            //        Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
            //        NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //    });

            Create.Table("AspNetRoles")
                .WithColumn("Id").AsString().NotNullable().PrimaryKey("PK_AspNetRoles")
                .WithColumn("Name").AsString(256).Nullable()
                .WithColumn("NormalizedName").AsString(256).Nullable()
                .WithColumn("ConcurrencyStamp").AsString().Nullable();
        }

        public override void Down()
        {
            //Delete.Table("Employee");
            Delete.Table("AspNetRoles");
        }
    }
}
