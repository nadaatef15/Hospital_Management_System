using CloudinaryDotNet;
using FluentValidation;
using HMSBusinessLogic.Manager.AccountManager;
using HMSBusinessLogic.Manager.Appointment;
using HMSBusinessLogic.Manager.Doctor;
using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Manager.IdentityManager;
using HMSBusinessLogic.Manager.LabTechnician;
using HMSBusinessLogic.Manager.MedicalRecord;
using HMSBusinessLogic.Manager.Patient;
using HMSBusinessLogic.Manager.PermissionManager;
using HMSBusinessLogic.Manager.Pharmacist;
using HMSBusinessLogic.Manager.Receptionist;
using HMSBusinessLogic.Seeds;
using HMSBusinessLogic.Services.Appointment;
using HMSBusinessLogic.Services.Doctor;
using HMSBusinessLogic.Services.GeneralServices;
using HMSBusinessLogic.Services.MedicalRecord;
using HMSBusinessLogic.Services.PatientService;
using HMSBusinessLogic.Services.user;
using HMSBusinessLogic.Validators;
using HMSContracts;
using HMSContracts.Infrastructure.Exceptions;
using HMSContracts.Model.Appointment;
using HMSContracts.Model.DoctorSchadule;
using HMSContracts.Model.Identity;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.DBContext;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Appointment;
using HMSDataAccess.Repo.Doctor;
using HMSDataAccess.Repo.LabTech;
using HMSDataAccess.Repo.MedicalRecord;
using HMSDataAccess.Repo.Patient;
using HMSDataAccess.Repo.Pharmacist;
using HMSDataAccess.Repo.Receptionist;
using Hospital_Management_System.Refliction;
using Microsoft.AspNetCore.Identity;
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


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IRoleManager, RoleManager>();
            builder.Services.AddScoped<IAccountManager, AccountManager>();
            builder.Services.AddScoped<IPermission, PermissionManager>();
            builder.Services.AddScoped<IReceptionistManager, ReceptionistManager>();
            builder.Services.AddScoped<IReceptionistRepo, ReceptionistRepo>();
            builder.Services.AddScoped<IDoctorManager, DoctorManager>();
            builder.Services.AddScoped<IDoctorRepo , DoctorRepo>();
            builder.Services.AddScoped<IPatientsManager, PatientsManager>();
            builder.Services.AddScoped<IPatientRepo ,  PatientRepo>();
            builder.Services.AddScoped<ILabTechnicianManager, LabTechnicianManager>();
            builder.Services.AddScoped<ILabTechRepo, LabTechRepo>();
            builder.Services.AddScoped<IMedicalRecordREpo, MedicalRecordRepo>();
            builder.Services.AddScoped<IMedicalRecordManager, MedicalRecordManager>();
            builder.Services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            builder.Services.AddScoped<IAppointmentManager, AppointmentManager>();
            builder.Services.AddScoped<IPharmacistRepo, PharmacistRepo>();
            builder.Services.AddScoped<IPharmacistManager, PharmacistManager>();
            builder.Services.AddScoped<IDoctorScheduleManager, DoctorScheduleManager>();
            builder.Services.AddScoped<IDoctorScheduleRepo, DoctorScheduleRepo>();

            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPatientService , PatientService>();
            builder.Services.AddScoped<IDoctorScheduleService , DoctorScheduleService>();

            builder.Services.AddScoped<IValidator<UserModel>, UserValidator>();
            builder.Services.AddScoped<IValidator<MedicalRecordModel>, MedicalRecordValidator>();
            builder.Services.AddScoped<IValidator<AppointmentModel>, AppointmentValidator>();
            builder.Services.AddScoped<IValidator<DoctorScheduleModel>, DoctorScheduleValidation>();




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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }
    }
}
