using Lisans.Data.Models;
using Lisans.Data.ResponseModels;
using Lisans.Utils.ObjectMapper;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading;

namespace Lisans.Utils.Helpers
{
    public class MailHelper
    {
        #region Single Section

        private static readonly Lazy<MailHelper> _instance = new Lazy<MailHelper>(() => new MailHelper());

        #endregion

        #region MailProperties
        private readonly string _mailUser = Environment.GetEnvironmentVariable("MAIL_USER");
        private readonly string _pass = Environment.GetEnvironmentVariable("MAIL_PASSWORD");
        private readonly string _mailHost = Environment.GetEnvironmentVariable("MAIL_HOST");
        private readonly int _port = Convert.ToInt32(Environment.GetEnvironmentVariable("MAIL_PORT"));
        #endregion

        public MailHelper()
        {
        }

        public static MailHelper Instance => _instance.Value;





        /// <summary>
        /// Mail Gönderimi
        /// </summary>
        /// <param name="mailAddress">Mail gönderilecek adress</param>
        /// <param name="messageBody">Mailin Detayı</param>
        /// <param name="subject">Mailin Konusu</param>
        public void SendMailLicenseInfoAsync(LisansModel lisansModel)
        {
            
            ThreadPool.QueueUserWorkItem(delegate
            {
                OfflineResponseModel offlineResponseModel = new OfflineResponseModel();
                ObjectMapper.ObjectMapper.Map(offlineResponseModel, lisansModel);
                var serializeObject = JsonConvert.SerializeObject(offlineResponseModel);
                var offlineProductKey = LisansHelper.Instance.GetBase64String(serializeObject);

                var messageBody = GetMessageBody(lisansModel.OnlineProductKey, offlineProductKey, lisansModel.HardwareId);

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_mailUser));
                message.To.Add(new MailboxAddress(lisansModel.Email));
                message.Subject = "Oyis Lisans Bilgi";
                message.Body = messageBody.ToMessageBody();

                SendMail(message);
            });
        }

        public BodyBuilder GetMessageBody(string onlineProductKey, string offlineProductKey, string hwid)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();

            string template =
                string.Format(
                    @"<html><p style=""text-align: center;""><strong>Oyis Uygulaması Lisans Anahtarınız</strong></p>
                    <p>Uygulamayı internet bağantısı ile aktifleştirebilmek i&ccedil;in aşağıdaki anahtarı kullanabilirsiniz.</p>
                    <p><strong>{0}</strong></p>
                    <p>&nbsp;</p>
                    <p>İnternet bağlantı problemi yaşıyorsanız aşağıdaki anahtar ile &ccedil;evrimdışı etkinleştirme yapabilirsiniz.</p>
                    <p><strong>{1}</strong></p>
                    <p>&nbsp;</p>
                    <p>Bu anahtar <strong>{2}</strong> seri numarasına sahip bilgisayar i&ccedil;in oluşturulmuştur.</p>
                    <p>L&uuml;tfen anahtarlarınızı paylaşmayınız.</p></html>", onlineProductKey, offlineProductKey, hwid);

            bodyBuilder.HtmlBody = template;

            return bodyBuilder;
        }

        /// <summary>
        /// Mailin Gönderilmesi
        /// </summary>
        /// <param name="message">Mailin Detayı</param>
        private void SendMail(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(_mailHost, _port);
                client.Authenticate(new NetworkCredential(_mailUser, _pass));
                

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
