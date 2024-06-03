using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtracV1.Models;

public class EmailSettings
{
   [Required][Key]
    public int EmailId { get; set; }
    public string MailServer { get; set; }
    public int MailPort { get; set; }
    public string SenderName { get; set; }
    public string Sender { get; set; }
    public string Password { get; set; }
}