﻿using Minsait.Challenge.Security.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Minsait.Challenge.Security.Domain
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return SHA256.Create()
                         .ComputeHash(Encoding.UTF8.GetBytes(password))
                         .Select(x => string.Format("{0:x2}", x))
                         .Aggregate((@byte, hash) => hash + @byte);
        }
    }
}
