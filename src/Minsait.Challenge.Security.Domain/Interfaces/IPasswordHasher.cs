﻿namespace Minsait.Challenge.Security.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
