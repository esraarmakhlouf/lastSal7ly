using BL.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Services
{
    public static class UIHelper
    {


        public static string GeneratCode(EN_Screens screen, IUnitOfWork _uow)
        {
            var count = 0;
            string code = "";
            switch (screen)
            {
                case EN_Screens.Groups:
                    count = _uow.GroupsRepository.GetMany(ent => true).Count();
                    code = "GRP-" + (count + 1);
                    break;
                case EN_Screens.Users:
                    count = _uow.UsersRepository.GetMany(ent => true).Count();
                    code = "US-" + (count + 1);
                    break;
                case EN_Screens.Country:
                    count = _uow.CountryRepository.GetMany(ent => true).Count();
                    code = "COU-" + (count + 1);
                    break;
                case EN_Screens.City:
                    count = _uow.CityRepository.GetMany(ent => true).Count();
                    code = "CTY-" + (count + 1);
                    break;
                case EN_Screens.Customer:
                    count = _uow.CustomerRepository.GetMany(ent => true).Count();
                    code = "CUS-" + (count + 1);
                    break;
                case EN_Screens.JobTitle:
                    count = _uow.JobTitlesRepository.GetMany(ent => true).Count();
                    code = "JOB-" + (count + 1);
                    break;

                case EN_Screens.ItemFavourite:
                    count = _uow.ItemFavouriteRepository.GetMany(ent => true).Count();
                    code = "ITM-" + (count + 1);
                    break;
                //case EN_Screens.OrderTrackAction:
                //    count = _uow.OrderTrackActionRepository.GetMany(ent => true).Count();
                //    code = "OAC-" + (count + 1);
                //    break;
                case EN_Screens.Order:
                    count = _uow.OrderRepository.GetMany(ent => true).Count();
                    code = "ORD-" + (count + 1);
                    break;
                default:
                    count = 1;
                    break;

            }
            return code;
        }
        public static int GetItemRate(List<Order> orders)
        {
            int itemRate = 0;
            if (orders.Any())
            { itemRate = (int)(orders.Sum(ent => ent.Rate) / orders.Count()); }
            return itemRate;
        }
        public static int GetAverageProfit(List<Order> orders, Technical tech)
        {
            int price = 0;
            if (orders.Any())
            {
                price = (int)(orders.Sum(ent => ent.OrderService.Price - (ent.OrderService.Price * ent.TechCommission)));
            }
            return price;
        }

        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public static int GetNumOfStatus(List<OrderTrack> model)
        {
            int num = -1;
            foreach (var item in model)
            {
                if (item.OrderTrackAction.Id == (long)EN_OrderActions.ordered || item.OrderTrackAction.Id == (long)EN_OrderActions.in_progress ||
                    item.OrderTrackAction.Id == (long)EN_OrderActions.accpted_by_user || item.OrderTrackAction.Id == (long)EN_OrderActions.delivered)
                    num += 1;
            }

            return num;
        }
        public static int GetNumOfStatusService(List<OrderTrack> model)
        {
            int num = -1;
            foreach (var item in model)
            {
                if (item.OrderTrackActionId == (long)EN_OrderActions.ordered ||
                    item.OrderTrackActionId == (long)EN_OrderActions.accpted_by_user || item.OrderTrackActionId == (long)EN_OrderActions.delivered)
                    num += 1;
            }

            return num;
        }

        /// <summary>
        ///   will  use  in  back end  when  you  invest  api  for example  forget  password  it  will  call this  method
        /// </summary>
        /// <param name="emailModel"></param>
        /// <returns></returns>
        public static bool SendEmail(EmailModel emailModel)
        {
            bool isSent = true;
            try
            {
                var fromAddress = new MailAddress(AppSession.CompanyEmail, string.IsNullOrEmpty(emailModel.FromName) ?
                    AppSession.CompanyEmailDisplayName : emailModel.FromName);
                MailMessage mail = new MailMessage();
                mail.From = fromAddress;
                mail.To.Add(new MailAddress(emailModel.To));
                if (!string.IsNullOrEmpty(emailModel.CC))
                { mail.CC.Add(new MailAddress(emailModel.CC)); }

                mail.Subject = emailModel.Subject;
                mail.Body = emailModel.Body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(fromAddress.Address, AppSession.CompanyEmailPassword);
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                var exception = $" Oops! Message could not be sent. Error:  {ex.Message}";
            }
            return isSent;
        }
        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}
