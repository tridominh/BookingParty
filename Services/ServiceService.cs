using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayParty.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        public List<Service> GetAllServices()
        {
            return serviceRepository.GetAll().ToList();
        }

        public Service GetServiceById(int id)
        {
            return serviceRepository.Get(id);
        }

        public Service UpdateService(ServiceUpdateDto updatedService)
        {
            var service = new Service
            {
                ServiceId = updatedService.ServiceId,
                PackageId = updatedService.PackageId,
                ServiceName = updatedService.ServiceName,
                ServicePrice = updatedService.ServicePrice
            };
            return serviceRepository.Update(service);
        }

        public Service DeleteService(int id)
        {
            return serviceRepository.Delete(id);
        }

        public Service CreateService(ServiceCreateDto service)
        {
            var serviceObj = new Service
            {
                PackageId = service.PackageId,
                ServiceName = service.ServiceName,
                ServicePrice = service.ServicePrice
            };
            return serviceRepository.Add(serviceObj);
        }
    }
}
