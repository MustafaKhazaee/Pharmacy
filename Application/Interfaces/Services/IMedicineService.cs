
using Application.Common;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Services {
    public interface IMedicineService : IGenericService<Medicine> {
        public Task<Medicine> SaveMedicine(MedicineModel medicineModel);
        public Task<Medicine> UpdateMedicine(MedicineModel medicineModel);
        public Task<DataTableResult<Medicine>> GetMedicinePage(DataTableParams param);
    }
}
