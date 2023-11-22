namespace Reminder.Application.Interfaces.Providers;

public interface IEncryptionProvider
{
    /// <summary>
    /// Hash string, mostly used in passwords
    /// </summary>
    /// <param name="str">String to hash</param>
    /// <returns>Hashed string, hash algorithms is defined in interface child class</returns>
    string Hash(string str);
}