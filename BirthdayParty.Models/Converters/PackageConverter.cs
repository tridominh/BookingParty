using BirthdayParty.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayParty.Models.Converters
{
    public class PackageConverter
    {
        public PackageConverter() { }

        public static Package toEntity(PackageCreateDto packageCreateDto)
        {
            if (packageCreateDto == null) return null;

            return new Package
            {
                PackageName = packageCreateDto.PackageName,
                PackageType = packageCreateDto.PackageType
            };
        }

        public static Package toEntity(PackageUpdateDto packageUpdateDto)
        {
            if (packageUpdateDto == null) return null;

            return new Package
            {
                PackageName = packageUpdateDto.PackageName,
                PackageType = packageUpdateDto.PackageType
            };
        }
    }
}
