using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Resource;
using HMSBusinessLogic.Services.MedicalRecord;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Appointment;
using HMSDataAccess.Repo.MedicalRecord;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;


namespace HMSBusinessLogic.Manager.MedicalRecord
{
    public interface IMedicalRecordsManager
    {
        Task CreateMedicalRecord(MedicalRecordModel model);
        Task DeleteMedicalRecord(int id);
        Task UpdateMedicalRecord(int Id, MedicalRecordModel model);
        Task<MedicalRecordResource> GetMedicalRecordById(int id);
        Task<List<MedicalRecordResource>> GetAllMedicalRecords();
    }
    public class MedicalRecordManager : IMedicalRecordsManager
    {
        private readonly IMedicalRecordREpo _medicalRecordRepo;
        private readonly IValidator<MedicalRecordModel> _validator;
        private readonly IMedicalRecordService _medicalRecordUpdateService;
        private readonly IAppointmentRepo _appointmentRepo;
        public MedicalRecordManager(IMedicalRecordREpo medicalRecordRepo,
               IAppointmentRepo appointmentRepo,
            IValidator<MedicalRecordModel> validators,
            IMedicalRecordService medicalRecordUpdateService
            )
        {
            _medicalRecordRepo = medicalRecordRepo;
            _appointmentRepo = appointmentRepo;
            _validator = validators;
            _medicalRecordUpdateService = medicalRecordUpdateService;
        }


        public async Task CreateMedicalRecord(MedicalRecordModel model)
        {
            await _validator.ValidateAndThrowAsync(model);

            var medicalRecord = model.ToEntity();

            var appointment = await _appointmentRepo.GetAppointmentById(medicalRecord.AppointmentId) ??
                 throw new NotFoundException(appointmentDoesnotExist);

            medicalRecord.DoctorId = appointment.DoctorId;
            medicalRecord.PatientId = appointment.PatientId;

            await _medicalRecordRepo.CreateMedicalRecord(medicalRecord);
        }

        public async Task DeleteMedicalRecord(int id)
        {
            var result = await _medicalRecordRepo.GetMedicalRecordByIdNoTracking(id) ??
                   throw new NotFoundException(MedicalRecordDoesnotExist);

            await _medicalRecordRepo.DeleteMedicalRecord(result);
        }

        public async Task UpdateMedicalRecord(int id, MedicalRecordModel model)
        {
            if (id != model.Id)
                throw new ConflictException(NotTheSameId);

            await _validator.ValidateAndThrowAsync(model);

            var medicalRecordEntity = await _medicalRecordRepo.GetMedicalRecordById(id) ??
                   throw new NotFoundException(MedicalRecordDoesnotExist);

            _medicalRecordUpdateService.SetValues(medicalRecordEntity, model);

            await _medicalRecordRepo.SaveChanges();
        }

        public async Task<MedicalRecordResource> GetMedicalRecordById(int id)
        {
            var medicalRecord = await _medicalRecordRepo.GetMedicalRecordByIdNoTracking(id) ??
                throw new NotFoundException(MedicalRecordDoesnotExist);

            return medicalRecord.ToResource();
        }

        public async Task<List<MedicalRecordResource>> GetAllMedicalRecords() =>
            (await _medicalRecordRepo.GetAllMedicalRecords()).Select(a => a.ToResource()).ToList();

    }

}
