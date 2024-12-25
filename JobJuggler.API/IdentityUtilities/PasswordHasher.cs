using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using JobJuggler.Domain.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace JobJuggler.API.IdentityUtilities;

public sealed class PasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : AppUser
{
    private readonly IConfiguration _configuration;

    public PasswordHasher(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string HashPassword(TUser user, string password)
    {
        var config = GetConfig(password);
        
        using var argon  = new Argon2(config);
        using var argonHash = argon.Hash();
        
        return config.EncodeString(argonHash.Buffer);
    }

    public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
    {
        var config = GetConfig(providedPassword);
        SecureArray<byte>? hashB = null;
        try
        {
            if (config.DecodeString(hashedPassword, out hashB) && hashB != null)
            {
                var argon2ToVerify = new Argon2(config);
                using var hashToVerify = argon2ToVerify.Hash();
                if (Argon2.FixedTimeEquals(hashB, hashToVerify))
                {
                    return PasswordVerificationResult.Success;
                }
            }
        }
        finally
        {
            hashB?.Dispose();
        }
        return PasswordVerificationResult.Failed;
    }

    private Argon2Config GetConfig(string providedPassword)
    {
        return new Argon2Config
        {
            Type = Argon2Type.HybridAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 10,
            MemoryCost = 32768,
            Lanes = 5,
            Threads = Environment.ProcessorCount, // higher than "Lanes" doesn't help (or hurt)
            Password = Encoding.UTF8.GetBytes(providedPassword),
            Salt = GenerateSalt(), // >= 8 bytes if not null
            Secret = Encoding.UTF8.GetBytes(_configuration["Identity:SecurityTokenKey"]), // from somewhere
            // AssociatedData = associatedData, // from somewhere
            HashLength = 20, // >= 4
        };
    }

    private byte[] GenerateSalt()
    {
        var salt = new byte[32];
        RandomNumberGenerator.Fill(salt);
        
        return salt;
    }
}