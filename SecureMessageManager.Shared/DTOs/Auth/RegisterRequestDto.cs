using System;
using System.ComponentModel.DataAnnotations;

namespace SecureMessageManager.Shared.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
