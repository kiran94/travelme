namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// Mail Service
    /// </summary>
    public class MailService : IMailService
    {
        /// <summary>
        /// Logger Service
        /// </summary>
        private readonly ILoggerService _logger = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailService"/> class.
        /// </summary>
        /// <param name="logger">Logger Service</param>
        public MailService(ILoggerService logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("Logger Service, Mail Service"); 
            }

            this._logger = logger; 
        }


        /// <summary>
        /// Gets the smtp client
        /// </summary>
        /// <returns>Smtp Client configured</returns>
        public SmtpClient GetClient()
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp-mail.outlook.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("", "");

            return client;
        }

        /// <summary>
        /// Constructs and sends an email
        /// </summary>
        /// <param name="to">User to send email too</param>
        /// <param name="From">From message header</param>
        /// <param name="subject">Subject message header</param>
        /// <param name="body">Body of the message</param>
        /// <param name="isHtml">Flag indicating if body is html</param>
        /// <returns></returns>
        public bool SendMessage(IList<string> to, string From, string subject, string body, bool isHtml)
        {
            StringBuilder builder = new StringBuilder(); 
            for(int i=0; i<to.Count; i++)
            {
                builder.Append(to[i]);

                if(i != to.Count-1)
                {
                     builder.Append(",");
                }
            }

            MailMessage message = new MailMessage();
            message.To.Add(builder.ToString());
            message.From = new MailAddress(From); 
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml; 

            SmtpClient client = this.GetClient();

            try
            {
                client.Send(message);
                return true; 
            }
            catch(Exception e)
            {
                this._logger.Log(new Log(e.Message, true));
                return false; 
            }
        }
    }
}