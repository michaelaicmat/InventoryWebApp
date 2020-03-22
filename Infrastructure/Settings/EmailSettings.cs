using SendGrid.Helpers.Mail;

namespace InventoryWebApp.Infrastructure.Settings
{
    public class EmailSettings
    {
        public string FromEmail { get; set; }
        public string FromITEmail { get; set; }
        public string FromName { get; set; }
        public string SendGridApiKey { get; set; }

        public EmailSettings()
        {
            FromEmail = "micmat@bramptonbrick.com";
            FromITEmail = "micmat@bramptonbrick.com";
            FromName = "Inventory App";
            SendGridApiKey = "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";
        }

        public EmailAddress GetFromEmail()
        {
            return new EmailAddress(FromEmail, FromName);
        }

        public EmailAddress GetITSupportEmail()
        {
            return new EmailAddress(FromITEmail, FromName);
        }
    }
}