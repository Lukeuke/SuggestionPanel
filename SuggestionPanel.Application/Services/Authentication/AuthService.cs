using SuggestionPanel.Application.Data;

namespace SuggestionPanel.Application.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext _context;

        public AuthService(ApplicationContext context)
        {
            _context = context;
        }

        public bool Login(string number, string password)
        {
            var user = _context.ValueStreamResponsibilities.FirstOrDefault(x => x.Number == number);

            if (user == null)
                return false;

            if (user.PasswordHash != AuthHelper.GenerateHash(password, user.Salt))
                return false;

            return true;
        }
    }
}
