﻿using Domain.Auth;

namespace WebApi.Domain.Request
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }  
        
        public string? Image { get; set; }

        public string[] Roles;
    }

}
