using BirthdayParty.Models.LocalImages;

namespace BirthdayParty.Services.Interfaces
{
    public interface IServiceImageLocalService
    {
        List<ServiceImageLocal> GetAllServiceImages();

        ServiceImageLocal GetServiceImage(int id);

        ServiceImageLocal CreateServiceImage(ServiceImageLocal serviceImage);

        ServiceImageLocal UpdateServiceImage(ServiceImageLocal serviceImage);

        ServiceImageLocal DeleteServiceImage(int id);
    }
}
