﻿using System.ComponentModel.DataAnnotations;

namespace AuthService.DTO
{
    public class UserLoginDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
