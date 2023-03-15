using CarRegistrationAPI.Entities;
using FluentMigrator;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CarRegistrationAPI.Migrations
{
    [Migration(20230314001)]
    public class InitialIdentityTables_20230314001 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Create.Table("identity_roles")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey("identity_roles_pkey")
                .WithColumn("name").AsString(256).Nullable()
                .WithColumn("normalized_name").AsString(256).Nullable()
                .WithColumn("concurrency_stamp").AsString().Nullable();

            Create.Table("identity_users")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey("identity_users_pkey")
                .WithColumn("username").AsString(256).Nullable()
                .WithColumn("normalized_username").AsString(256).Nullable()
                .WithColumn("email").AsString(256).Nullable()
                .WithColumn("normalized_email").AsString(256).Nullable()
                .WithColumn("email_confirmed").AsBoolean().NotNullable()
                .WithColumn("password_hash").AsString().Nullable()
                .WithColumn("security_stamp").AsString().Nullable()
                .WithColumn("concurrency_stamp").AsString().Nullable()
                .WithColumn("phone_number").AsString().Nullable()
                .WithColumn("phone_number_confirmed").AsBoolean().NotNullable()
                .WithColumn("two_factor_enabled").AsBoolean().NotNullable()
                .WithColumn("lockout_end").AsDateTime().Nullable()
                .WithColumn("lockout_enabled").AsBoolean().NotNullable()
                .WithColumn("access_failed_count").AsInt32().NotNullable();

            Create.Table("identity_role_claims")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey("identity_role_claims_pkey")
                .WithColumn("role_id").AsGuid().NotNullable()
                .WithColumn("claim_type").AsString().Nullable()
                .WithColumn("claim_value").AsString().Nullable();

            Create.ForeignKey().FromTable("identity_role_claims").ForeignColumn("role_id").ToTable("identity_roles").PrimaryColumn("id");

            Create.Table("identity_user_claims")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey("identity_user_claims_pkey")
                .WithColumn("user_id").AsGuid().NotNullable()
                .WithColumn("claim_type").AsString().Nullable()
                .WithColumn("claim_value").AsString().Nullable();

            Create.ForeignKey().FromTable("identity_user_claims").ForeignColumn("user_id").ToTable("identity_users").PrimaryColumn("id");

            Create.Table("identity_user_logins")
                .WithColumn("login_provider").AsString(128).NotNullable().PrimaryKey()
                .WithColumn("provider_key").AsString(128).NotNullable().PrimaryKey()
                .WithColumn("provider_display_name").AsString().Nullable()
                .WithColumn("user_id").AsGuid().NotNullable();

            Create.ForeignKey().FromTable("identity_user_logins").ForeignColumn("user_id").ToTable("identity_users").PrimaryColumn("id");


            Create.Table("identity_user_roles")
                .WithColumn("user_id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("role_id").AsGuid().NotNullable().PrimaryKey();

            Create.ForeignKey().FromTable("identity_user_roles").ForeignColumn("role_id").ToTable("identity_roles").PrimaryColumn("id");
            Create.ForeignKey().FromTable("identity_user_roles").ForeignColumn("user_id").ToTable("identity_users").PrimaryColumn("id");

            Create.Table("identity_user_tokens")
            .WithColumn("user_id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("login_provider").AsString(128).NotNullable().PrimaryKey()
            .WithColumn("name").AsString(128).NotNullable().PrimaryKey()
            .WithColumn("value").AsString().Nullable();

            Create.ForeignKey().FromTable("identity_user_tokens").ForeignColumn("user_id").ToTable("identity_users").PrimaryColumn("id");

            Create.Index("ix_identity_role_claims_role_id").OnTable("identity_role_claims").OnColumn("role_id");
            Create.Index("ix_identity_user_claims_user_id").OnTable("identity_user_claims").OnColumn("user_id");
            Create.Index("ix_identity_user_logins_user_id").OnTable("identity_user_logins").OnColumn("user_id");
            Create.Index("ix_identity_user_roles_role_id").OnTable("identity_user_roles").OnColumn("role_id");
            Create.Index("email_index").OnTable("identity_users").OnColumn("normalized_email");


            Create.Table("Owners")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("Firstname").AsString(128).NotNullable()
            .WithColumn("Lastname").AsString(128).NotNullable()
            .WithColumn("Middlename").AsString(128).NotNullable()
            .WithColumn("Birthdate").AsDateTime().NotNullable()
            .WithColumn("Address").AsString(256).NotNullable();

            Create.Index("lastname_index").OnTable("Owners").OnColumn("Lastname");


            Create.Table("Certificates")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Ownerid").AsInt32().NotNullable()
                .WithColumn("Registrationplate").AsString(128).NotNullable()
                .WithColumn("Model").AsString(128).NotNullable()
                .WithColumn("Year").AsInt32().NotNullable()
                .WithColumn("Color").AsString().NotNullable()
                .WithColumn("Vin").AsString(128).NotNullable()
                .WithColumn("Type").AsString(128).NotNullable()
                .WithColumn("Maxmass").AsInt32().NotNullable()
                .WithColumn("Issuedate").AsDateTime().NotNullable()
                .WithColumn("Issuer").AsString(256).NotNullable();

            Create.ForeignKey().FromTable("Certificates").ForeignColumn("Ownerid").ToTable("Owners").PrimaryColumn("Id");

            Insert.IntoTable("identity_roles")
                .Row(new UserRole
                {
                    id = new Guid("0e543f8c-0093-4aa1-ad0b-18368c9b099d"),
                    concurrency_stamp = "f0ecda90-6a4b-41df-b634-1f74220f556e",
                    name = "User",
                    normalized_name = "USER"
                });

            Insert.IntoTable("identity_roles")
                .Row(new UserRole
                {
                    id = new Guid("95c93ace-7651-44c4-8737-52851d614f32"),
                    concurrency_stamp = "f843d78e9-a36e-4c55-84dc-2009287721da",
                    name = "Administrator",
                    normalized_name = "ADMINISTRATOR"
                });

            Insert.IntoTable("identity_users")
                .Row(new
                {
                    id = "610268a8-2b23-494e-856c-6bba84e7ebcc",
                    access_failed_count = 0,
                    concurrency_stamp = "481c89e2-9aa6-4248-a5de-371f8935c08a",
                    email = "admin@example.com",
                    email_confirmed = false,
                    lockout_enabled = false,
                    normalized_email = "ADMIN@EXAMPLE.COM",
                    normalized_username = "ADMIN@EXAMPLE.COM",
                    password_hash = "AQAAAAIAAYagAAAAEMR3xdNTSINaziTGSTSxUse8I6sfmFr3r2Bpku6E5zkZ+2LxGctd975YQ9w+DrSkrg==",
                    phone_number_confirmed = false,
                    security_stamp = "13d23d08-4360-46c0-b892-0fc5459b60a7",
                    two_factor_enabled = false,
                    username = "admin@example.com"
                });

            Insert.IntoTable("identity_users")
                .Row(new
                {
                    id = "cf833103-d733-4402-b00c-1263ca230e72",
                    access_failed_count = 0,
                    concurrency_stamp = "77f00288-f544-4ec0-a347-605c63de3e57",
                    email = "user@example.com",
                    email_confirmed = false,
                    lockout_enabled = false,
                    normalized_email = "USER@EXAMPLE.COM",
                    normalized_username = "USER@EXAMPLE.COM",
                    password_hash = "AQAAAAIAAYagAAAAEMR3xdNTSINaziTGSTSxUse8I6sfmFr3r2Bpku6E5zkZ+2LxGctd975YQ9w+DrSkrg==",
                    phone_number_confirmed = false,
                    security_stamp = "245d97d6-82b3-41a4-ab41-04135021a49f",
                    two_factor_enabled = false,
                    username = "user@example.com"
                });

            Insert.IntoTable("identity_user_roles")
                .Row(new
                {
                    user_id = "610268a8-2b23-494e-856c-6bba84e7ebcc",
                    role_id = "95c93ace-7651-44c4-8737-52851d614f32"
                });

            Insert.IntoTable("identity_user_roles")
                .Row(new
                {
                    user_id = "cf833103-d733-4402-b00c-1263ca230e72",
                    role_id = "0e543f8c-0093-4aa1-ad0b-18368c9b099d"
                });

            Insert.IntoTable("Owners")
                .Row(new Owner
                {
                    Id = 1,
                    Firstname = "L",
                    Lastname = "K",
                    Middlename = "N",
                    Birthdate = new DateTime(1969, 12, 6),
                    Address = "RB, Minsk, Narnia str 13 -13"
                });

            Insert.IntoTable("Certificates")
                .Row(new Certificate
                {
                    Id= 1,
                    Ownerid= 1,
                    Color = "Brown",
                    Issuedate = new DateTime(2014, 11, 25),
                    Issuer = "GAI",
                    Maxmass= 1950,
                    Model="NISSAN QUASHKAI",
                    Registrationplate = "0001 SK-7",
                    Type="SUV Universal",
                    Vin="SJ231456723178634001",
                    Year=2014
                });
        }
    }
}
