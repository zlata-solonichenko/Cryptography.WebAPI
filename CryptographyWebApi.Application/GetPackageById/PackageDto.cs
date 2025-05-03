namespace CryptographyWebApi.Application.GetPackageById;

public class PackageDto
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
    /// Путь к файлу на жестком диске
    /// </summary>
    public string FilePath { get; set; }
}