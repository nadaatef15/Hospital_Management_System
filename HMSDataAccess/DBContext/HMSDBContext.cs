using HMSDataAccess.Entity;
using HMSDataAccess.Interfaces;
using HMSDataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HMSDataAccess.DBContext
{
    public class HMSDBContext : IdentityDbContext<UserEntity>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HMSDBContext()
        {

        }

        public HMSDBContext(DbContextOptions options , IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor= httpContextAccessor;
        }

        public HMSDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<ReceptionistEntity> Receptionists { get; set; }
        public DbSet<PharmacistEntity> Pharmacists { get; set; }
        public DbSet<LabTechnicianEntity> LabTechnicians { get; set; }
        public DbSet<MedicineEntity> Medicine { get; set; }
        public DbSet<TestEntity> Tests { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<InvoiceItemEntity> InvoiceItems { get; set; }
        public DbSet<InvoiceEntity> Invoice { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<PrescriptionEntity> Prescriptions { get; set; }
        public DbSet<DiagnosesEntity> Diagnoses { get; set; }
        public DbSet<MedicalRecordDiagnoses> MedicalRecordDiagnoses { get; set; }
        public DbSet<MedicalRecordTests> MedicalRecordTests { get; set; }
        public DbSet<MedicalRecordEntity> MedicalRecord { get; set; }
        public DbSet<DoctorSpecialties> DoctorSpecialties { get; set; }
        public DbSet<DoctorScheduleEntity> DoctorSchedule { get; set; }
        public DbSet<SpecialtyEntity> Specialties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>()
                .HasQueryFilter(x => !x.IsDeleted);

            builder.Entity<DoctorEntity>()
                .ToTable("Doctors");

            builder.Entity<PatientEntity>()
                .ToTable("Patients");

            builder.Entity<ReceptionistEntity>()
                .ToTable("Receptionists");

            builder.Entity<PharmacistEntity>()
               .ToTable("Pharmacists");

            builder.Entity<LabTechnicianEntity>()
               .ToTable("LabTechnicians");


            builder.Model
                .GetEntityTypes()
                .Where(entityType => typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType) && entityType.BaseType == null)
                .ToList()
                .ForEach(entityType =>
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "entity");

                    var filter = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, nameof(ISoftDelete.IsDeleted)),
                            Expression.Constant(false)
                        ),
                        parameter
                    );

                    builder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
                );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete softEntity)
                {
                    entry.State = EntityState.Modified;
                    softEntity.IsDeleted = true;
                    softEntity.DeletedOn = DateTime.Now;
                    softEntity.DeletedBy = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                }

                else if (entry.State == EntityState.Added && entry.Entity is ITrackable trackableEntity)
                {
                        trackableEntity.CreatedOn = DateTime.Now;
                        trackableEntity.CreatedBy = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "System";
                    
                }

                else if (entry.State == EntityState.Modified && entry.Entity is ITrackable trackable)
                {
                    trackable.UpdatedOn = DateTime.Now;
                    trackable.UpdatedBy = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "System";
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
