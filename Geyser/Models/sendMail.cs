using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Geyser.Models
{
    public class sendMail
    {
        public static void sendmail(string nameTitle, string nameFb, string urlHomes, string name, string fromEmail, string address, string mobile, string email, string content)
        {

            GeyserContext db = new GeyserContext();
            //TblContact TblContact = new TblContact();
            //TblContact.Name = name;
            //if (address == null)
            //    address = "online";
            //if(mobile==null)
            //{
            //    mobile = "12345";
            //}
            //if (content == null)
            //    content = "chưa có thông tin";
            //if (email == null)
            //    email = "vvtcntt@gmail.com";
            //TblContact.Address = address;
            //TblContact.Mobile = mobile;
            //TblContact.Email = email;
            //TblContact.Content = content;
            //db.TblContact.Add(TblContact);
            //db.SaveChanges();
            var config = db.TblConfig.FirstOrDefault();
            string nFrommail = config.Email;
            if (fromEmail != null && fromEmail != "")
            {
                nFrommail = nFrommail + "," + fromEmail;
            }

            string subject = "" + nameTitle + "  " + nameFb + "";
            string chuoihtml = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Thông tin đơn hàng</title></head><body style=\"background:#f2f2f2; font-family:Arial, Helvetica, sans-serif\"><div style=\"width:750px; height:auto; margin:5px auto; background:#FFF; border-radius:5px; overflow:hidden\">";
            chuoihtml += "<div style=\"width:100%; height:40px; float:left; margin:0px; background:#1c7fc4\"><span style=\"font-size:14px; line-height:40px; color:#FFF; margin:auto 20px; float:left\">" + DateTime.Now.Date + "</span><span style=\"font-size:14px; line-height:40px; float:right; margin:auto 20px; color:#FFF; text-transform:uppercase\">Hotline : " + config.HotlineIn + "</span></div>";
            chuoihtml += "<div style=\"width:100%; height:auto; float:left; margin:0px\"><div style=\"width:35%; height:100%; margin:0px; float:left\"><a href=\"/\" title=\"\"><img src=\"" + urlHomes + "" + config.Logo + "\" alt=\"Logo\" style=\"margin:8px; display:block; max-height:95% \" /></a></div><div style=\"width:60%; height:100%; float:right; margin:0px; text-align:right\"><span style=\"font-size:18px; margin:20px 5px 5px 5px; display:block; color:#ff5a00; text-transform:uppercase\">" + config.Name + "</span><span style=\"display:block; margin:5px; color:#515151; font-size:13px; text-transform:uppercase\">Lớn nhất - Chính hãng - Giá rẻ nhất việt nam</span> </div>  </div>";
            chuoihtml += "<span style=\"text-align:center; margin:10px auto; font-size:20px; color:#000; font-weight:bold; text-transform:uppercase; display:block\">Thông tin Liên hệ</span>";
            chuoihtml += " <div style=\"width:90%; height:auto; margin:10px auto; background:#f2f2f2; padding:15px\">";
            chuoihtml += "<p style=\"font-size:14px; color:#464646; margin:5px 20px\">Tiêu đề sản phẩm/tin tức : <span style=\"color:#1c7fc4\">" + nameFb + "</span></p>";

            chuoihtml += "<p style=\"font-size:14px; color:#464646; margin:5px 20px\">Thông tin liên hệ từ website : <span style=\"color:#1c7fc4\">" + urlHomes + "</span></p>";
            chuoihtml += "<p style=\"font-size:14px; color:#464646; margin:5px 20px\">Ngày gửi liên hệ : <span style=\"color:#1c7fc4\">Vào lúc " + DateTime.Now.Hour + " giờ " + DateTime.Now.Minute + " phút ( ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year + ") </span></p>";

            chuoihtml += "<div style=\" width:100%; margin:15px 0px\">";
            chuoihtml += "<div style=\"width:100%; height:auto; float:left; margin:0px; border:1px solid #d5d5d5\">";
            chuoihtml += "<div style=\" width:100%; height:30px; float:left; background:#1c7fc4; font-size:12px; color:#FFF; text-indent:15px; line-height:30px\">    	Thông tin người gửi     </div>";

            chuoihtml += "<div style=\"width:100%; height:auto; float:left\">";
            chuoihtml += "<p style=\"font-size:12px; margin:5px 10px\">Họ và tên :<span style=\"font-weight:bold\"> " + name + "</span></p>";
            chuoihtml += "<p style=\"font-size:12px; margin:5px 10px\">Địa chỉ :<span style=\"font-weight:bold\"> " + address + "</span></p>";
            chuoihtml += "<p style=\"font-size:12px; margin:5px 10px\">Điện thoại :<span style=\"font-weight:bold\"> " + mobile + " </span></p>";
            chuoihtml += "<p style=\"font-size:12px; margin:5px 10px\">Email :<span style=\"font-weight:bold\">" + email + "</span></p>";

            chuoihtml += "<div style=\"width:90%; height:auto; margin:10px auto; border:1px solid #d5d5d5\">";
            chuoihtml += "<div style=\" width:100%; height:30px; float:left; background:#1c7fc4; font-size:12px; color:#FFF; text-indent:15px; line-height:30px\">   	Nội dung     </div>";
            chuoihtml += " <div style=\"width:100%; height:auto; float:left\">";
            chuoihtml += "<p style=\"font-size:12px; margin:5px 10px; font-weight:bold; color:#F00\"> - " + content + "</p>";
            chuoihtml += "</div>";
            chuoihtml += "</div>";
            chuoihtml += "</div>";
            chuoihtml += "<div style=\"width:100%; height:auto; float:left; margin:0px\">";
            chuoihtml += "<hr style=\"width:80%; height:1px; background:#d8d8d8; margin:20px auto 10px auto\" />";
            chuoihtml += "<p style=\"font-size:12px; text-align:center; margin:5px 5px\">" + address + "</p>";
            chuoihtml += "<p style=\"font-size:12px; text-align:center; margin:5px 5px\">Điện thoại : " + config.HotlineIn + "</p>";
            chuoihtml += " <p style=\"font-size:12px; text-align:center; margin:5px 5px; color:#ff7800\">Thời gian mở cửa : Từ 7h30 đến 18h30 hàng ngày (làm cả thứ 7, chủ nhật). Khách hàng đến trực tiếp xem hàng giảm thêm giá.</p>";
            chuoihtml += "</div>";
            chuoihtml += "<div style=\"clear:both\"></div>";
            chuoihtml += " </div>";
            chuoihtml += " <div style=\"width:100%; height:40px; float:left; margin:0px; background:#1c7fc4\">";
            chuoihtml += "<span style=\"font-size:12px; text-align:center; color:#FFF; line-height:40px; display:block\">Copyright (c) 2002 – 2015 SONHA VIET NAM. All Rights Reserved</span>";
            chuoihtml += " </div>";
            chuoihtml += "</div>";
            chuoihtml += "</body>";
            chuoihtml += "</html>";
            string body = chuoihtml;

            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = config.Host;
                smtp.Port = int.Parse(config.Port.ToString());
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(config.UserEmail, config.PassEmail);
                smtp.Timeout = int.Parse(config.Timeout.ToString());
            }
            using (var message = new MailMessage(config.UserEmail, nFrommail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,


            })
            {
                smtp.Send(message);
            }

        }
    }
}