using System.Net;
using System.Net.Mail;

public class emails
    {
        private SmtpClient smtp;

        public emails()
        {
            smtp = new SmtpClient()
            {
                //Host = "smtp.gmail.com",
                //Port = 587,
                //EnableSsl = false,
                //UseDefaultCredentials = false,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //Credentials = new NetworkCredential("noreply.yiff@gmail.com", "SuperSoft@123")

                Host = "win10.hosterpk.com",
                Port = 25,
                EnableSsl = false,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("noreply@jobtalaash.com", "0%pcmG36")

            };
        }

        private string Name = string.Empty;
        private string Username = string.Empty;
        private string Msg = string.Empty;
        private string Sub = string.Empty;
        private string mailTo = string.Empty;
        private string mailFrom = string.Empty;
        private string Bcc = string.Empty;
        private string Cc1 = string.Empty;
        private string Cc2 = string.Empty;
        private string password = string.Empty;
        private string message = string.Empty;
        private string subject = string.Empty;

        public string NAME
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

        public string USERNAME
        {
            get { return this.Username; }
            set { this.Username = value; }
        }

        public string MAILTO
        {
            get { return this.mailTo; }
            set { this.mailTo = value; }
        }

        public string MAILFROM
        {
            get { return this.mailFrom; }
            set { this.mailFrom = value; }
        }

        public string BCC
        {
            get { return this.Bcc; }
            set { this.Bcc = value; }
        }

        public string CC1
        {
            get { return this.Cc1; }
            set { this.Cc1 = value; }
        }

        public string CC2
        {
            get { return this.Cc2; }
            set { this.Cc2 = value; }
        }

        public string PASSWORD
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public string MESSAGE
        {
            get { return this.message; }
            set { this.message = value; }
        }

        public string SUBJECT
        {
            get { return this.subject; }
            set { this.subject = value; }
        }

        public int usersRegistrationEmail(string url, string loginId, string password)
        {
            MailMessage message = new MailMessage(MAILFROM, MAILTO);
            Sub = "Welcome to YIFF";
            Msg = "<html><body><br>Dear <b style=" + "" + "color:Blue" + "" + "> User " + NAME + " </b>," +
                   "<p>Thank you very much for joining YIFF.</p>" +
                   "<p>You can useYIFF - Hospital Management System (Portal) with below mentioned URL and cresentials.</p>" +
                   "<p>YIFF URL: " + url + "</p>" +
                   "<p>Your Login id is " + loginId + "</p>" +
                   "<p>Your password is " + password + "</p>" +
                   "<br />" +
                   "<p>Note: Please change your passsword at your first logon and keep secure your user id and password.</p>" +
                   "<br />" +
                   "<p>Thank you </p>" +
                   "<br />" +
                   "<p>YIFF </p>" +
                   "<p>&nbsp;</p><p>&nbsp;</p>" +
                   "<hr/>" +
                   "<b style=" + "" + "color:Gray" + "" + ">For further details please contact Supersoft Technologies, System administrator.</b><br>" +
                   "<b style=" + "" + "color:Gray" + "" + ">Attention: Do not reply to this email. This email is auto-generated!</b><br>" +
                   "<i>&copy; 2020 YIFF. All rights reserved</i></body></html>";

            message.Subject = Sub;
            message.Body = Msg;
            message.IsBodyHtml = true;
            message.Bcc.Add(BCC);
            if (message != null)
            {
                smtp.Send(message);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int usersResetPassword(string password)
        {
            MailMessage message = new MailMessage(MAILFROM, MAILTO);
            Sub = "Confirmation of password change";
            Msg = "<html><body><br>Dear <b style=" + "" + "color:Blue" + "" + "> User " + NAME + " </b>," +
                   "<p>Your password has been changed successfully. </p>" +
                   "<p>Your new password is " + password + " </p>" +
                   "<br />" +
                   "<br />" +
                   "<br />" +
                   "<p>Thank you </p>" +
                   "<br />" +
                   "<p>YIFF </p>" +
                   "<p>&nbsp;</p><p>&nbsp;</p>" +
                   "<hr/>" +
                   "<b style=" + "" + "color:Gray" + "" + ">For further details please contact Supersoft Technologies, System administrator.</b><br>" +
                   "<b style=" + "" + "color:Gray" + "" + ">Attention: Do not reply to this email. This email is auto-generated!</b><br>" +
                   "<i>&copy; 2020 YIFF. All rights reserved</i></body></html>";

            message.Subject = Sub;
            message.Body = Msg;
            message.IsBodyHtml = true;
            message.Bcc.Add(BCC);
            if (message != null)
            {
                smtp.Send(message);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int usersResetCode(string resetCode)
        {
            MailMessage message = new MailMessage(MAILFROM, MAILTO);
            Sub = "Password Reset Code";
            Msg = "<html><body><br>Dear <b style=" + "" + "color:Blue" + "" + "> User " + NAME + " </b>," +
                   "<p>Your reset code is " + resetCode + " </p>" +
                   "<br />" +
                   "<br />" +
                   "<br />" +
                   "<p>Thank you </p>" +
                   "<br />" +
                   "<p>YIFF </p>" +
                   "<p>&nbsp;</p><p>&nbsp;</p>" +
                   "<hr/>" +
                   "<b style=" + "" + "color:Gray" + "" + ">For further details please contact Supersoft Technologies, System administrator.</b><br>" +
                   "<b style=" + "" + "color:Gray" + "" + ">Attention: Do not reply to this email. This email is auto-generated!</b><br>" +
                   "<i>&copy; 2020 YIFF. All rights reserved</i></body></html>";

            message.Subject = Sub;
            message.Body = Msg;
            message.IsBodyHtml = true;
            message.Bcc.Add(BCC);
            if (message != null)
            {
                smtp.Send(message);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }