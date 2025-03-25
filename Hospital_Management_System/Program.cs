using CloudinaryDotNet;
using FluentValidation;
using HMSBusinessLogic.Manager.AccountManager;
using HMSBusinessLogic.Manager.Appointment;
using HMSBusinessLogic.Manager.Diagnose;
using HMSBusinessLogic.Manager.Doctor;
using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Manager.IdentityManager;
using HMSBusinessLogic.Manager.LabTechnician;
using HMSBusinessLogic.Manager.MedicalRecord;
using HMSBusinessLogic.Manager.Medicine;
using HMSBusinessLogic.Manager.Patient;
using HMSBusinessLogic.Manager.PermissionManager;
using HMSBusinessLogic.Manager.Pharmacist;
using HMSBusinessLogic.Manager.Prescription;
using HMSBusinessLogic.Manager.Receptionist;
using HMSBusinessLogic.Manager.Specialty;
using HMSBusinessLogic.Manager.Test;
using HMSBusinessLogic.Seeds;
using HMSBusinessLogic.Services.Appointment;
using HMSBusinessLogic.Services.GeneralServices;
using HMSBusinessLogic.Services.MedicalRecord;
using HMSBusinessLogic.Services.Medicine;
using HMSBusinessLogic.Services.PatientService;
using HMSBusinessLogic.Services.Prescription;
using HMSBusinessLogic.Services.user;
using HMSBusinessLogic.Validators;
using HMSContracts;
using HMSContracts.Model.Appointment;
using HMSContracts.Model.Diagnose;
using HMSContracts.Model.DoctorSchadule;
using HMSContracts.Model.Identity;
using HMSContracts.Model.MedicalRecord;
using HMSContracts.Model.Medicine;
using HMSContracts.Model.Prescription;
using HMSContracts.Model.Specialty;
using HMSContracts.Model.Test;
using HMSContracts.Model.Users;
using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Appointment;
using HMSDataAccess.Repo.Diagnoses;
using HMSDataAccess.Repo.Doctor;
using HMSDataAccess.Repo.LabTech;
using HMSDataAccess.Repo.MedicalRecord;
using HMSDataAccess.Repo.Medicine;
using HMSDataAccess.Repo.Patient;
using HMSDataAccess.Repo.Pharmacist;
using HMSDataAccess.Repo.Prescription;
using HMSDataAccess.Repo.Receptionist;
using HMSDataAccess.Repo.Specialty;
using HMSDataAccess.Repo.Test;
using Hospital_Management_System.Refliction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Globalization;

namespace Hospital_Management_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Cloudinary
            var cloudinaryConfig = new CloudinaryDotNet.Account(
            builder.Configuration["Cloudinary:CloudName"],
            builder.Configuration["Cloudinary:ApiKey"],
            builder.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(cloudinaryConfig);
            builder.Services.AddSingleton(cloudinary);

            //Localization
            var localizationOptions = new RequestLocalizationOptions();
            var supportCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ar")
            };

            localizationOptions.SupportedCultures = supportCultures;
            localizationOptions.SupportedUICultures = supportCultures;
            localizationOptions.SetDefaultCulture("en");
            localizationOptions.ApplyCurrentCultureToResponseHeaders = true;


            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });
            builder.Services.DBContextService(builder.Configuration);
            builder.Services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<HMSDBContext>();
            builder.Services.AuthenticationService(builder.Configuration);
            builder.Services.SwaggerConfiguration();
            builder.Services.AddLocalization();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("myPolicy", policy =>
                {
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            RegisterServices(builder.Services);

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
               
                // Seed Admin
                await SeedRoleAdmin.SeedAdminRole(roleManager);
                await SeedUserAdmin.SeedAdmin(userManager, roleManager); 
            

            // Configure the HTTP request pipeline.
           
            app.UseRequestLocalization(localizationOptions);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //  app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseCors("myPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IPermission, PermissionManager>();
            services.AddScoped<IReceptionistManager, ReceptionistManager>();
            services.AddScoped<IReceptionistRepo, ReceptionistRepo>();
            services.AddScoped<IDoctorManager, DoctorManager>();
            services.AddScoped<IDoctorRepo, DoctorRepo>();
            services.AddScoped<IPatientsManager, PatientsManager>();
            services.AddScoped<IPatientRepo, PatientRepo>();
            services.AddScoped<ILabTechniciansManager, LabTechnicianManager>();
            services.AddScoped<ILabTechRepo, LabTechRepo>();
            services.AddScoped<IMedicalRecordREpo, MedicalRecordRepo>();
            services.AddScoped<IMedicalRecordsManager, MedicalRecordManager>();
            services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            services.AddScoped<IAppointmentManager, AppointmentManager>();
            services.AddScoped<IPharmacistRepo, PharmacistRepo>();
            services.AddScoped<IPharmacistManager, PharmacistManager>();
            services.AddScoped<IDoctorScheduleManager, DoctorScheduleManager>();
            services.AddScoped<IDoctorScheduleRepo, DoctorScheduleRepo>();
            services.AddScoped<ISpecialtiesManager, SpecialtiesManager>();
            services.AddScoped<ISpecialtyRepo, SpecialtyRepo>();
            services.AddScoped<IDoctorSpecialtiesRepo, DoctorSpecialtiesRepo>();
            services.AddScoped<IDoctorSpecialtiesManager, DoctorSpecialtiesManager>();
            services.AddScoped<IDiagnosesRepo, DiagnosesRepo>();
            services.AddScoped<IDiagnoseManager, DiagnoseManager>();
            services.AddScoped<IMedicineRepo, MedicineRepo>();
            services.AddScoped<IMedicineManager, MedicineManager>();
            services.AddScoped<ITestRepo, TestRepo>();
            services.AddScoped<ITestManager, TestManager>();
            services.AddScoped<IMedicalRecordTestsRepo, MedicalRecordTestsRepo>();
            services.AddScoped<IMedicalRecordTestsManager, MedicalRecordTestsManager>();
            services.AddScoped<IPrescriptionManager, PrescriptionManager>();
            services.AddScoped<IPrescriptionRepo, PrescriptionRepo>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IValidator<UserModel>, UserValidator>();
            services.AddScoped<IValidator<MedicalRecordModel>, MedicalRecordValidator>();
            services.AddScoped<IValidator<AppointmentModel>, AppointmentValidator>();
            services.AddScoped<IValidator<SpecialtyModel>, SpecialtyValidator>();
            services.AddScoped<IValidator<DoctorSpecialtyModel>, DoctorSpecialtyValidator>();
            services.AddScoped<IValidator<DoctorModel>, DoctorValidation>();
            services.AddScoped<IValidator<DiagnoseModel>, Diagnosevalidator>();
            services.AddScoped<IValidator<TestModel>, TestValidator>();
            services.AddScoped<IValidator<MedicalRecordTestsModel>, MedicalRecordTestsValidators>();
            services.AddScoped<IValidator<PrescriptionModel>, PrescriptionValidator>();
            services.AddScoped<IValidator<DoctorScheduleModel>, DoctorScheduleValidator>();
            services.AddScoped<IValidator<MedicineModel>, MedicineValidator>();
            services.AddScoped<IValidator<PrescriptionDispensedModel>, PrescriptionDispensedValidator>();

        }
    }

}
