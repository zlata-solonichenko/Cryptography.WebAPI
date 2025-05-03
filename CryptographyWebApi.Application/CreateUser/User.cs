using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using CryptographyWebApi.Application.CreatePackage;

namespace CryptographyWebApi.Application.CreateUser;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Certificate { get; private set; }
    public string Thumbprint { get; private set; }
    
    public User() { } 
    public User(string email, string certificate)
    {
        Id = Guid.NewGuid();
        Email = ValidateEmail(email);
        SetCertificate(certificate);
    }

    public User(Guid id, string email, string certificate)
    {
        Id = ValidateId(id);
        Email = ValidateEmail(email);
        SetCertificate(certificate);
    }

    private Guid ValidateId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id не может быть null!");
        }

        return id;
    }

    private string ValidateEmail(string email)
    {
        bool isValid = new EmailAddressAttribute().IsValid(email);
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email отсутствует!");
        }

        if (!isValid)
        {
            throw new ArgumentException("Неверное заполнение сертификата");
        }

        return email;
    }

    private void SetCertificate(string certificate)
    {
        if (string.IsNullOrWhiteSpace(certificate))
        {
            throw new ArgumentException("Сертификат отсутствует!");
        }

        try
        {
            var certificateBytes = Convert.FromBase64String(certificate);
            var x509Certificate = new X509Certificate2(certificateBytes);
            Certificate = certificate;
            Thumbprint = x509Certificate.Thumbprint;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Некорректный сертификат!", ex);
        }
    }
}