﻿namespace com.kiransprojects.travelme.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Net.Mail;

    /// <summary>
    /// Contract Mail Service
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Gets the smtp client
        /// </summary>
        /// <returns>Smtp Client configured</returns>
        SmtpClient GetClient();

        /// <summary>
        /// Constructs and sends an email
        /// </summary>
        /// <param name="to">Users to send email too</param>
        /// <param name="From">From message header</param>
        /// <param name="subject">Subject message header</param>
        /// <param name="body">Body of the message</param>
        /// <param name="isHtml">Flag indicating if body is html</param>
        /// <returns></returns>
        bool SendMessage(IList<string> to, string From, string subject, string body, bool isHtml);
    }
}