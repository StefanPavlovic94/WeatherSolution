using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net.Mail;

namespace WeatherApp.Services
{
    public class AccountsService
    {
        ApplicationConfiguration configuration;

        public AccountsService()
        {
            this.configuration = new ApplicationConfiguration();
            this.configuration.Load();
        }
        /// <summary>
        /// Return true if account is found, else return false
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Account SignIn(Account account) {
            try
            {
                using (var db = new AccountsContext(configuration.ConnectionString))
                {
                   account.Password = GetHashedPassword(account.Password);
                   Account acc = db.Accounts.Where(a => a.Username == account.Username && 
                                                        a.Password == account.Password && 
                                                        a.Status != UserStatus.Pending).FirstOrDefault();
                    if (acc != null)
                    {
                        return acc;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Return a hashed password
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetHashedPassword(string value)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }

        /// <summary>
        /// Add account to db and return true if sign up info is valid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool SignUp(Account account) {
            try
            {
                using (var db = new AccountsContext(configuration.ConnectionString)) {
                    List<Account> list = new List<Account>();
                    list = db.Accounts.Where(a => a.Username == account.Username || a.Email == account.Email).ToList();
                    if (list.Count == 0)
                    {
                        account.Username = account.Username.Trim();
                        account.Name = account.Name.Trim();
                        account.Email = account.Email.Trim();
                        account.Password = account.Password.Trim();
                        account.Password = GetHashedPassword(account.Password);
                        db.Accounts.Add(account);
                        db.SaveChanges();
                        SendConfirmationEmail(account.Id, account.Email);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// if activationToken is valid change user status and return true
        /// </summary>
        /// <param name="activationToken"></param>
        /// <returns></returns>
        public bool Activation(string activationToken) {
            using (var db = new AccountsContext(configuration.ConnectionString)) {

                Account account = db.Accounts.Where(a => a.Id == activationToken).FirstOrDefault();
                if (account != null)
                {
                    account.Status = UserStatus.Registrated;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Sends confirmation email to user
        /// </summary>
        /// <param name="activationToken"></param>
        /// <param name="email"></param>
        public void SendConfirmationEmail(string activationToken, string email) {
            try
            {
                string activationUrl = HttpContext.Current.Request.Url.Scheme+"://"+HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "Accounts/Activation?activationToken=" + activationToken + "";

                MailMessage messageClient = new MailMessage("MyWeatherSolution@gmail.com", email);
                messageClient.IsBodyHtml = true;
                messageClient.Subject = "Please confirm identity";
                messageClient.Body = @"To confirm your identity please click <a href='"+activationUrl+"'>here</a> Weather solution";

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = configuration.AppsEmail,
                    Password = configuration.AppsMailPassword
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(messageClient);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}