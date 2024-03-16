using BirthdayParty.Models.DTOs;
using BirthdayParty.Models.ModelScaffold;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayParty.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository packageRepository;
        private readonly IServiceRepository serviceRepository;

        public PackageService(IPackageRepository packageRepository, IServiceRepository serviceRepository)
        {
            this.packageRepository = packageRepository;
            this.serviceRepository = serviceRepository;
        }

        public List<Package> GetAllPackages()
        {
            return packageRepository.GetAll(p => p.Include(p => p.Services)).ToList();
        }

        public Package UpdatePackage(int id, Package package)
        {
            Package existingPackage = packageRepository.Get(id);

            existingPackage.PackageName = package.PackageName;
            existingPackage.PackageType = package.PackageType;

            packageRepository.Update(existingPackage);

            return existingPackage;
        }

        public Package DeletePackage(int id)
        {
            Package package = packageRepository.Delete(id);

            return package;
        }

        public List<Service> GetAllServicesByPackageId(int packageId)
        {
            List<Service> services = serviceRepository.GetAll()
                .Where(s => s.PackageId == packageId).ToList();

            return services;
        }

        public Package CreatePackage(PackageCreateDto packageCreateDto)
        {
            Package package = new Package
            {
                PackageName = packageCreateDto.PackageName,
                PackageType = packageCreateDto.PackageType
            };

            packageRepository.Add(package);

            return package;
        }

        public Package UpdatePackage(PackageUpdateDto packageUpdateDto)
        {
            Package existingPackage = packageRepository.Get(packageUpdateDto.PackageId);

            existingPackage.PackageName = packageUpdateDto.PackageName;
            existingPackage.PackageType= packageUpdateDto.PackageType;

            packageRepository.Update(existingPackage);

            return existingPackage;
        }
    }
}
