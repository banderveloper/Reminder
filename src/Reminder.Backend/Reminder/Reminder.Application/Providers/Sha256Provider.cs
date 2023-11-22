using System.Security.Cryptography;
using System.Text;
using Reminder.Application.Interfaces.Providers;

namespace Reminder.Application.Providers;

public class Sha256Provider : IEncryptionProvider
{
    public string Hash(string str)
    {
        var hashValue = SHA256.HashData(Encoding.UTF8.GetBytes(str));
        return BitConverter.ToString(hashValue);
    }
}