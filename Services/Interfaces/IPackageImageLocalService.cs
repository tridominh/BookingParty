using BirthdayParty.Models;
using BirthdayParty.Models.LocalImages;

namespace BirthdayParty.Services.Interfaces
{
    public interface IPackageImageLocalService
    {
        List<PackageImageLocal> GetAllPackageImages();

        PackageImageLocal GetPackageImage(int id);

        PackageImageLocal CreatePackageImage(PackageImageLocal packageImage);

        PackageImageLocal UpdatePackageImage(PackageImageLocal packageImage);

        PackageImageLocal DeletePackageImage(int id);
    }
}
