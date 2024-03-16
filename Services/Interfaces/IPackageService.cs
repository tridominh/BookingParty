using BirthdayParty.Models;
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

        void CreatePackage(Package package);

        Package UpdatePackage(int id, Package package);

        Package DeletePackage(int id);
    }
}
