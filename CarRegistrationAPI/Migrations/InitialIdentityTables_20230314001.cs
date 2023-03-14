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


            Insert.IntoTable("identity_roles")
                .Row(new UserRole
                {
                    id = new Guid("0e543f8c-0093-4aa1-ad0b-18368c9b099d"),
                    concurrency_stamp = "f0ecda90-6a4b-41df-b634-1f74220f556e",
                    name = "user",
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
        }
    }
}
