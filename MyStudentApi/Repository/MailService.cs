

using Microsoft.Extensions.Options;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MyStudentApi.Repository.IRepo;
using System.Net;
using System.Net.Mail;

public class MailServices : IEMailServices
{
    private readonly MailSettings _mailSettings;
    public MailServices(IOptions<MailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
    }

    public bool SendEmail(Student student, SchoolClass schoolClass)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("valentine.onodu.247160@unn.edu.ng", "Killer_queen1"),
        };

        try
        {
            smtpClient.EnableSsl = true;
            smtpClient.Send("valentine.onodu.247160@unn.edu.ng", student.Email, "Absent Student", $"Y From Trusted Child Teacher .your Child {student.FullName} was not present for their {schoolClass.ClasssName}");
            Console.WriteLine("Success");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

  
}












