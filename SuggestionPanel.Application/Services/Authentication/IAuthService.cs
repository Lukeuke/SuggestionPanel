using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuggestionPanel.Application.Services.Authentication
{
    public interface IAuthService
    {
        bool Login(string number, string password);
    }
}
