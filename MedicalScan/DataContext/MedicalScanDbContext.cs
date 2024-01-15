using MedicalScan.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalScan.DataContext;

public class MedicalScanDbContext: DbContext
{
    public MedicalScanDbContext(DbContextOptions<MedicalScanDbContext> options) : base(options)
    {
    }

    public DbSet<SpecialityModel> Specialities { get; set; }
    public DbSet<DoctorsModel> Doctors { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DoctorsModel>()
            .HasMany(d => d.Specialties)
            .WithMany(s => s.Doctors)
            .UsingEntity<Dictionary<string, object>>("DoctorSpecialties",
                j => j.HasOne<SpecialityModel>().WithMany().HasForeignKey("DoctorId"),
                j => j.HasOne<DoctorsModel>().WithMany().HasForeignKey("SpecialtyId"));
    }
}
