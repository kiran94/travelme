namespace com.kiransprojects.travelme.Services.Interfaces
{
    using System.Net;

    /// <summary>
    /// Contract Mail Service
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Gets the network credentails for the smtp client
        /// </summary>
        /// <returns></returns>
        private NetworkCredential GetCredentials(); 

        /// <summary>
        /// Constructs and sends an email
        /// </summary>
        /// <param name="to">User to send email too</param>
        /// <param name="From">From message header</param>
        /// <param name="subject">Subject message header</param>
        /// <param name="body">Body of the message</param>
        /// <param name="isHtml">Flag indicating if body is html</param>
        /// <returns></returns>
        bool SendMessage(string to, string From, string subject, string body, bool isHtml);
    }
}