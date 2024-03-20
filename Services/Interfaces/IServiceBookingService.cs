using BirthdayParty.Models;

namespace BirthdayParty.Services.Interfaces
{
    public interface IServiceBookingService
    {
        List<Service> GetAllServices();
        Service GetServiceById(int id);
        Service UpdateService(Service updatedService);
        Service DeleteService(int id);
        Service CreateService(Service service);
    }
}
