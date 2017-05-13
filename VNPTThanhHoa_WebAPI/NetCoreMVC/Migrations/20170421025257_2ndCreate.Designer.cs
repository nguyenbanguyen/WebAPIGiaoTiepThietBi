using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetCoreMVC.Models;

namespace NetCoreMVC.Migrations
{
    [DbContext(typeof(NetCoreMVCContext))]
    [Migration("20170421025257_2ndCreate")]
    partial class _2ndCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetCoreMVC.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<bool>("Rememberme");

                    b.HasKey("AccountID");

                    b.ToTable("Account");
                });
        }
    }
}
