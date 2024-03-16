using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Models.ModelScaffold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayParty.Services.Interfaces
{
    public interface IPackageService
    {
        List<Package> GetAllPackages();

        List<Service> GetAllServicesByPackageId(int packageId);

        Package CreatePackage(PackageCreateDto package);

        Package UpdatePackage(PackageUpdateDto package);

        Package DeletePackage(int id);
    }
}
