namespace CryptographyWebApi.Infrastructure.Entities;

public class PackageDb
{
    /// <summary>
    /// Айди пакета
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Дата отправки
    /// </summary>
    public DateTime SentDate { get; set; }
    
    /// <summary>
    /// Дата получения
    /// </summary>
    public DateTime? CompletionDate { get; set; }
    
    /// <summary>
    /// Путь к файлу на жстком диске
    /// </summary>
    public string FilePath { get; set; }
    
    /// <summary>
    /// Айди отправителя
    /// </summary>
    public Guid SenderId { get; set; }
    
    /// <summary>
    /// Отправитель
    /// </summary>
    public UserDb Sender { get; set; }

    /// <summary>
    /// Айди получателя
    /// </summary>
    public Guid RecipientId { get; set; }
    
    /// <summary>
    /// Получатель
    /// </summary>
    public UserDb Recipient { get; set; }
    
}