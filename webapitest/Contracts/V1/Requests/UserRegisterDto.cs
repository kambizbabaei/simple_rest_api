using System.ComponentModel.DataAnnotations;

namespace webapitest.Contracts.V1.Requests
{
    public class UserRegisterDto
    {
        [EmailAddress]
        public string Email { get; set; }
        
        [MaxLength(16)]
        public string Password { get; set; }
    }
}