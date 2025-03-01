using System.Runtime.InteropServices.JavaScript;
using CryptographyWebApi.Application.CreateUser;

namespace CryptographyWebApi.Application.CreatePackage;

/// <summary>
/// Сущность для обмена данными
/// </summary>
public class Package
{
    /// <summary>
    /// Айди пакета
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public User Sender { get; private set; }
    
    /// <summary>
    /// Айди навигационног свойства
    /// </summary>
    public Guid SenderID { get; private set; }
    
    /// <summary>
    /// Дата отправки
    /// </summary>
    public DateTime SentDate { get; private set; }
    
    /// <summary>
    /// Дата получения
    /// </summary>
    public DateTime? CompletionDate { get; private set; }
    
    /// <summary>
    /// Путь к файлу на жестком диске
    /// </summary>
    public string FilePath { get; private set; }

    public Package(Guid id, User sender, Guid senderId, DateTime sentDate, DateTime? completionDate, string filePath)
    {
        Id = id;
        Sender = sender;
        SenderID = senderId;
        SentDate = sentDate;
        CompletionDate = completionDate;
        FilePath = filePath;
    }
    
    public Package(Guid id, Guid senderId, DateTime sentDate, DateTime? completionDate, string filePath)
    {
        Id = id;
        SenderID = senderId;
        SentDate = sentDate;
        CompletionDate = completionDate;
        FilePath = filePath;
    }
    
    public Package(User sender, Guid senderId, DateTime sentDate, DateTime? completionDate, string filePath)
    {
        Sender = sender;
        SenderID = senderId;
        SentDate = sentDate;
        CompletionDate = completionDate;
        FilePath = filePath;
    }
}