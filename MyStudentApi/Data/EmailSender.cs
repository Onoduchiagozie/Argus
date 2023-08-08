using System.Net.Mail;
using System.Net;
using MyStudentApi.Models;
namespace MyStudentApi.Data
{
    public class EmailSender
    {
       public static void Sender(Student obj,SchoolClass schoolClass)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential(AdminDetails.Email, AdminDetails.Password);
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(AdminDetails.Email),
                    Subject = "Absentee Child",
                    Body = $"Dear Sir/Ma Your child {obj.FullName} was absent for their Cos"+ $"{schoolClass.CourseCode}" + $" {schoolClass.ClasssName} " + 
                                                  " during hours of {schoolClass.StartTime.Hour}-{schoolClass.StopTime.Hour}"
                };

                mail.To.Add(new MailAddress(obj.Email));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                // Send it...         
                client.Send(mail);
                Console.WriteLine($"Email for {obj.FullName} successfully sent to ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending email: " + ex.Message);
            }
            
            // Console.ReadKey();
        }
    }
}
