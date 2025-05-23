﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace TodoApi;

public partial class ToDoDbContext : DbContext
{
    public ToDoDbContext()
    {
    }

    public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
        : base(options)
    {
    }


    public DbSet<Item> Items { get; set; }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
    // .UseMySql(builder.Configuration.GetConnectionString("ToDoDB"),
    //  Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("Server=bzihwsot5wnlw05shmep-mysql.services.clever-cloud.com;" +
                                     "Database=bzihwsot5wnlw05shmep;" +
                                     "User=uc1h6zquvck9wpdk;" +
                                     "Password=W9LIbD5yyy1T36bm39BU;",
                                     Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Item>().ToTable("items1")
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
