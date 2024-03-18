using BirthdayParty.Models.LocalImages;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;

namespace BirthdayParty.Services.LocalImages
{
    public class PackageImageLocalService : IPackageImageLocalService
    {
        private readonly IGenericRepository<PackageImageLocal> _packageImageRepository;

        public PackageImageLocalService(IGenericRepository<PackageImageLocal> packageImageRepository)
        {
            _packageImageRepository = packageImageRepository;
        }

        public List<PackageImageLocal> GetAllPackageImages()
        {
            return _packageImageRepository.GetAll().ToList();
        }

        public PackageImageLocal GetPackageImage(int id)
        {
            return _packageImageRepository.Get(id);
        }

        public PackageImageLocal UpdatePackageImage(PackageImageLocal updatedImage)
        {
            return _packageImageRepository.Update(updatedImage);
        }

        public PackageImageLocal DeletePackageImage(int id)
        {
            return _packageImageRepository.Delete(id);
        }

        public PackageImageLocal CreatePackageImage(PackageImageLocal image)
        {
            return _packageImageRepository.Add(image);
        }

    }
}
