using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS_EasyExceptionHandling.Models
{
    [Table("tblAppErrorLogs")]
    public sealed class RS_AppErrorLogs
    {
        public Guid Id { get; set; }
        [MaxLength(500)]
        public string ErrorTitle { get; set; }
        public string ErrorDetail { get; set; }
        public string ErrorSource { get; set; }
        public DateTime ErrorDate { get; set; }
        public string LogType { get; set; }
        public int ErrorCount { get; set; }
        public DateTime MailSentDate { get; set; }
        public bool IsMailSent { get; set; }
    }
}
