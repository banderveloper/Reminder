using System.Security.Cryptography;
using System.Text;

namespace Reminder.Application.Providers;

public class EncryptionProvider
{
    public string ToSha256(string str)
    {
        var hashValue = SHA256.HashData(Encoding.UTF8.GetBytes(str));
        return BitConverter.ToString(hashValue);
    } 
}