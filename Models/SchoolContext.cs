using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{

    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDBTEST;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentAddress>(entity =>
            {
                entity.HasKey(v => v.StudentID);
                // Otras configuraciones de la entidad, si las hay

                // Agregar otra condición
                entity.Property(v => v.Address1).IsRequired();
            });

            builder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);
                entity.Property(c => c.CourseName).IsRequired();

                entity.HasOne(c => c.Student)
                     .WithMany(s => s.Courses)
                     .HasForeignKey(c => c.StudentId)
                     .IsRequired();
            });
        }



    }

}