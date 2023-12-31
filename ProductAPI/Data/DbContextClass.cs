﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ProductAPI.Model;
using System.Reflection;

namespace ProductAPI.Data
{
    public class DbContextClass:DbContext
    {
        private readonly IConfiguration _configuration;
        public DbContextClass(IConfiguration configuration)
        {
                this._configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
            sqlServerOptionsAction:SqlOperation=> 
            {
                SqlOperation.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                //Configuring Connection Resiliency:
                ////پیکربندی انعطاف پذیری اتصال:
                SqlOperation
                .EnableRetryOnFailure(maxRetryCount: 5,
                 maxRetryDelay: TimeSpan.FromSeconds(30),
                 errorNumbersToAdd: null);

                //////////// Changing default behavior when client evaluation occurs to throw.// تغییر رفتار پیش‌فرض زمانی که ارزیابی مشتری رخ می‌دهد. 
                //////////// Default in EFCore would be to log warning when client evaluation is done. // پیش‌فرض در EFCore این است که وقتی ارزیابی مشتری انجام می‌شود، هشدار ثبت شود.
                //////////options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));
            });
            
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
