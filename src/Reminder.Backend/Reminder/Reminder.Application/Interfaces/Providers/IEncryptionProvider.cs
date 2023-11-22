namespace Reminder.Application.Interfaces.Providers;

public interface IEncryptionProvider
{
    string Hash(string str);
}