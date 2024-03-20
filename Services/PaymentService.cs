using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirthdayParty.Services
{
    public class PaymentServices
    {
        private readonly IGenericRepository<Payment> _paymentRepository;

        public PaymentServices(IGenericRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public Payment CreatePayment(Payment payment)
        {
            return _paymentRepository.Add(payment);
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _paymentRepository.GetAll();
        }

        public Payment GetPaymentById(int id)
        {
            return _paymentRepository.Get(id);
        }

        public Payment UpdatePayment(Payment payment)
        {
            return _paymentRepository.Update(payment);
        }

        public Payment DeletePayment(int id)
        {
            return _paymentRepository.Delete(id);
        }
    }
}
