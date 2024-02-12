using emedit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emedit.DAO
{
    public class sendmail
    {
        // 이메일 발송 시 실행되는 class 파일
        string text = "님 , 요청사항 접수가 완료되었습니다.";
        public sendmail() {
           
        }
        public void sendtomail(TaskDTO dto)
        {
            MailMessage mail = new MailMessage();
            // 보내는사람
            mail.To.Add(dto.email);
            // 회신받을 주소
            mail.From = new MailAddress(dto.email);
            // 메일 내용
            mail.Body = dto.reqnm + text;
            // 제목
            mail.Subject = "[요청사항 접수 완료]";
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            mail.SubjectEncoding = Encoding.UTF8;
            // 메일 제목 인코딩은 UTF-8
            mail.BodyEncoding = Encoding.UTF8;
            // 메일 내용 인코딩은 UTF-8

            SmtpClient smtp = smtpclient();

            try
            {
                smtp.Send(mail);
                // smtp 객체를 통해 mail 발송
                mail.Dispose();

                MessageBox.Show("전송완료", "전송 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                // 메일 발송 실패시 오류 메시지 출력
            }
        }

        private SmtpClient smtpclient()
        {
            SmtpClient smtp = new SmtpClient();
            // SmtpClient 사용을 위한 smtp 객체 생성
            smtp.Host = "smtp.gmail.com";
            // smtp 메일 서버 주소 입력
            smtp.Port = 587;
            // smtp 메일 포트 주소 입력
            smtp.Timeout = 10000;
            smtp.UseDefaultCredentials = true;
            // 서버 기본 인증 이용
            smtp.EnableSsl = true;
            // smtp SSL 보안 설정
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            // 이메일을 네트워크를 통해 SMTP 서버로 전송
            smtp.Credentials = new System.Net.NetworkCredential("id", "password");
            // 사용자 아이디와 비밀번호
            return smtp;
        }
        
        public void sendtorev(string email, string text, string filenames)
        {
            MailMessage mail = new MailMessage();
            // 보내는사람
            mail.To.Add(email);
            // 회신받을 주소
            mail.From = new MailAddress(email);
            // 메일 내용
            mail.Body = text;
            // 제목
            mail.Subject = "[요청사항 처리 완료]";
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            mail.SubjectEncoding = Encoding.UTF8;
            // 메일 제목 인코딩은 UTF-8
            mail.BodyEncoding = Encoding.UTF8;

            if(filenames != "x")

            {   // theexcelfilename : 첨부파일의 전체 경로
                Attachment theexcel = new Attachment(filenames);
            // theexcelfile_short_name : 메일에서 보이게 되는 첨부파일명

            mail.Attachments.Add(theexcel);

            }
          
           
            //MessageBox.Show(theexcel.Name);
            SmtpClient smtp = smtpclient();

            try
            {
                smtp.Send(mail);
                // smtp 객체를 통해 mail 발송
                mail.Dispose();

                MessageBox.Show("전송완료", "전송 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                // 메일 발송 실패시 오류 메시지 출력
            }
        }
    }

}

