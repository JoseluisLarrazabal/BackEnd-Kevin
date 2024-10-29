using CB_Backend_FAB.Models;

namespace CB_Backend_FAB.Services
{
    public interface IRecordTreatmentsService
    {
        Task CreateAsync(RecordTreatments recordTreatments);
        Task<IEnumerable<RecordTreatments>> GetAllAsync();
        Task<RecordTreatments> GetByIdAsync(int id);
    }
}
