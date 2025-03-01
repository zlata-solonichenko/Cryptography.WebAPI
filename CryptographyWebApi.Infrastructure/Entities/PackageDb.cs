namespace CryptographyWebApi.Infrastructure.Entities;

public class PackageDb
{
    /// <summary>
    /// Айди пакета
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public UserDb Sender { get; set; }
    
    /// <summary>
    /// Айди навигационног свойства
    /// </summary>
    public Guid SenderID { get; set; }
    
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

    public UserDb UserDb { get; set; }
}