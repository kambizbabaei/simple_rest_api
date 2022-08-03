using System.Threading.Tasks;

namespace webapitest.Services
{
    public interface IIdentityService
    {
        Task<string> RegisterAsync(string requestEmail, string requestPassword);
        Task<string> LoginAsync(string requestEmail, string requestPassword);
    }
}