using System.Runtime.InteropServices.JavaScript;
using CryptographyWebApi.Application.CreateUser;

namespace CryptographyWebApi.Application.CreatePackage;

/// <summary>
/// Сущность пакета
/// </summary>
public class Package
{
    /// <summary>
    /// Айди пакета
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Дата отправки
    /// </summary>
    public DateTime SentDate { get; private set; }
    
    /// <summary>
    /// Дата получения
    /// </summary>
    public DateTime? CompletionDate { get; private set; }
    
    /// <summary>
    /// Отправитель
    /// </summary>
    public User Sender { get; private set; }
    
    /// <summary>
    /// Получатель
    /// </summary>
    public User Recipient { get; set; }
    
    /// <summary>
    /// Путь к файлу на жестком диске
    /// </summary>
    public string FilePath { get; private set; }

    public Package() { }
    
    public Package(Guid id, User sender, User recipient, DateTime sentDate, DateTime? completionDate, string filePath)
    {
        Id = id;
        Sender = sender;
        Recipient = recipient;
        SentDate = sentDate;
        CompletionDate = completionDate;
        FilePath = filePath;
    }
    public Package(Guid id, DateTime sentDate, DateTime? completionDate, string filePath)
    {
        Id = id;
        SentDate = sentDate;
        CompletionDate = completionDate;
        FilePath = filePath;
    }
    
    public Package(User sender, User recipient, DateTime sentDate, DateTime? completionDate, string filePath)
    {
        Id = Guid.NewGuid();
        Sender = sender;
        Recipient = recipient;
        SentDate = sentDate;
        CompletionDate = completionDate;
        FilePath = filePath;
    }

    public Package(User sender, User recipient, DateTime sentDate, string filePath)
    {
        Id = Guid.NewGuid();
        Sender = sender;
        SentDate = sentDate;
        Recipient = recipient;
        FilePath = filePath;
    }
}