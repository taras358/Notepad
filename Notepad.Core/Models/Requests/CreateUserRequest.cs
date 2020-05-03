using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Models.Requests
{
    public  class CreateUserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
