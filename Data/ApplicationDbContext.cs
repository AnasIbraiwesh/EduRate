using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eduRateSystem.Models;

namespace eduRateSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<University> Universities { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UniversityReview> UniversityReviews { get;set; }
        public DbSet<ProfessorReview> ProfessorReviews { get; set; }
        public DbSet<ProofSubmission> ProofSubmissions { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Professor>()
                .HasOne(p => p.University)
                .WithMany(u => u.Professors)
                .HasForeignKey(p => p.UniversityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.University)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.UniversityId)
                .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(p => p.Courses)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UniversityReview>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UniversityReviews)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UniversityReview>()
                .HasOne(ur => ur.University)
                .WithMany(u => u.UniversityReviews)
                .HasForeignKey(ur => ur.UniversityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfessorReview>()
                .HasOne(pr => pr.User)
                .WithMany(p => p.ProfessorReviews)
                .HasForeignKey(pr =>  pr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfessorReview>()
                .HasOne(pr => pr.Professor)
                .WithMany(p => p.ProfessorReviews)
                .HasForeignKey(pr => pr.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProofSubmission>()
                .HasOne(ps => ps.User)
                .WithMany(u => u.ProofSubmissions)
                .HasForeignKey(ps => ps.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProofSubmission>()
                .HasOne(ps => ps.ReviewedByAdmin)
                .WithMany()
                .HasForeignKey(ps => ps.ReviewedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.ReportedByUser)
                .WithMany(u => u.ReportsSubmitted)
                .HasForeignKey(r => r.ReportedByUserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Report>()
                .HasOne(r => r.ReviewedByAdmin)
                .WithMany()
                .HasForeignKey(r => r.ReviewedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UniversityReview>()
                .HasIndex(ur => new { ur.UserId, ur.UniversityId })
                .IsUnique();

            modelBuilder.Entity<ProfessorReview>()
                .HasIndex(pr => new { pr.UserId, pr.ProfessorId })
                .IsUnique();

        }

    }
}
