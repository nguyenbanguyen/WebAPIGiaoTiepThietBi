using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetCoreMVC.Models;

namespace NetCoreMVC.Migrations
{
    [DbContext(typeof(NetCoreMVCContext))]
    partial class NetCoreMVCContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<decimal>("Balance");

                    b.Property<string>("CreatedDate");

                    b.Property<bool>("IsHuman");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<bool>("Rememberme");

                    b.HasKey("AccountID");

                    b.ToTable("Account");
                });
        }
    }
}
