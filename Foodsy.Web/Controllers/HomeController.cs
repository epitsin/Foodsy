namespace Foodsy.Web.Controllers
{
    using System;
    using System.Net.Mail;
    using System.Web.Mvc;

    using Foodsy.Data;
    using Foodsy.Data.Models;

    using Postal;

    public class HomeController : BaseController
    {
        public HomeController(IFoodsyData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult Contact()
        {
            var feedback = new Message();
            return View(feedback);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Message model)
        {
            string text = "<html> <head> </head>" +
            " <body style= \" font-size:12px; font-family: Arial\">" +
            model.Text +
            "</body></html>";

            SendEmail("epitsin@yahoo.com", text);

            var feedback = new Message();
            return View(feedback);
        }


        public static bool SendEmail(string sentTo, string text)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("epitsin@yahoo.com");
            msg.To.Add(sentTo);
            msg.Subject = "Try to send email from asp project";
            msg.Body = text;
            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.yahoo.com", 25);



            client.UseDefaultCredentials = true;
            //client.EnableSsl = false;
            //client.Credentials = new NetworkCredential("TestLogin", "TestPassword");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult SendEmail()
        {
            dynamic email = new Email("Example");
            email.To = "epitsin@yahoo.com";
            email.From = "epitsin@gmail.com";
            email.Message = "Strong typed message";
            email.Send();
            return View();
        }
    }
}