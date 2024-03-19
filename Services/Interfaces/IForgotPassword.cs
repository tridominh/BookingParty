using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayParty.Services.Interfaces
{
    public class IForgotPassword
    {
        public interface IForgotPasswordService
        {
            Task<bool> ForgotPassword(string email);
        }

    }
}
