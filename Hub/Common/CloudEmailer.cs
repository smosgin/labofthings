﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using HomeOS.Hub.Platform;
using HomeOS.Hub.Platform.Views;
using HomeOS.Shared;

namespace HomeOS.Hub.Common
{
    public class CloudEmailer : EmailerBase
    {
        Uri serviceHostUri;
        public CloudEmailer(Uri serviceHostUri, string smtpServer, string smtpUsername, string smtpPassword, VLogger logger) : base(smtpServer, smtpUsername, smtpPassword, logger)
        {
            this.serviceHostUri = serviceHostUri;
        }

        public override Tuple<bool, string> Send(Notification notification)
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(base.smtpServer) ||
                string.IsNullOrWhiteSpace(base.smtpUsername) ||
                string.IsNullOrWhiteSpace(base.smtpPassword) ||
                string.IsNullOrWhiteSpace(notification.toAddress) || 
                (this.serviceHostUri == null) ||
                string.IsNullOrWhiteSpace(this.serviceHostUri.OriginalString))
            {
                error = "Cannot send email. Email Setup not done correctly";
                base.logger.Log(error);
                return new Tuple<bool, string>(false, error);
            }

            try
            {
                // TODO: add support for attachments for cloud email relay
                EmailRequestInfo emailRequestInfo = new EmailRequestInfo(
                                                            base.smtpUsername,
                                                            base.smtpPassword,
                                                            base.smtpServer,
                                                            notification.toAddress,
                                                            notification.subject,
                                                            notification.body,
                                                            notification.attachmentList
                                                            );
                if (null != emailRequestInfo)
                {
                    string jsonString = emailRequestInfo.SerializeToJsonStream();
                    logger.Log("Sending cloud email : {0}", jsonString);
                    WebClient webClient = new WebClient();
                    webClient.Headers["Content-type"] = "application/json";
                    webClient.Encoding = Encoding.UTF8;
                    webClient.UseDefaultCredentials = true;
                    string jsonEmailStatus = webClient.UploadString(new Uri(this.serviceHostUri.OriginalString + "/SendEmail"), "POST", jsonString);
                    EmailStatus emailStatus = SerializerHelper<EmailStatus>.DeserializeFromJsonStream(jsonEmailStatus);
                    return new Tuple<bool, string>(emailStatus.SendStatus == EmailSendStatus.SentSuccessfully, emailStatus.SendFailureMessage);
                }

            }
            catch (Exception exception)
            {
                error = string.Format("Exception while sending message: {0}", exception.ToString());
                base.logger.Log(error);
                return new Tuple<bool, string>(false, error);
            }

            return new Tuple<bool, string>(true, "");
        }
    }
}
