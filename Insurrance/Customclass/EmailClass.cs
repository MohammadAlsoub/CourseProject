using Insurrance.Data;
using MailKit.Net.Smtp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;


namespace Insurrance.Customclass
{

    public class EmailClass
    {
        private readonly AppDbContext _context;

        public EmailClass(AppDbContext context)
        {
            _context = context;
        }

        //password cqqy xogw epmf erxs

        public void SendEmail(int Customer, int type)
        {

            var Email = _context.Customers.Find(Customer);
            if (Email != null)
            {
                var Setting = _context.Configurations.Find(1);
                if (Setting!.Send == true)
                {
                    string Messege = "";
                    string subject = "";
                    switch (type)
                    {
                        case 1: Messege = Setting.Wellcome; subject = "Wellcome " + Email.CustName; break;
                        case 2: Messege = Setting.Update; subject = "Update personal informatin"; break;
                        case 3: Messege = Setting.AddedCar; subject = "Add new Car"; break;
                        case 4: Messege = Setting.Accident; subject = "Accident check Result"; break;
                        case 5: Messege = Setting.compensation; subject = "Accident Cheque compensation"; break;
                    }





                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress(Setting.CompanyName, Setting.CompanyEmail));
                    email.To.Add(new MailboxAddress("Mr/Ms", Email.Email));
                    email.Subject = subject;
                    email.Body = new TextPart(TextFormat.Html) { Text = Messege };

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Connect(Setting.SMTP, int.Parse(Setting.Port), false);
                        smtp.Authenticate(Setting.CompanyEmail, Setting.EmailPassword);
                        smtp.Send(email);
                        smtp.Disconnect(true);
                    }
                }
            }
        }


    }
}
