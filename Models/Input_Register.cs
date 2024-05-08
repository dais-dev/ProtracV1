using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protrac1.Models;

public class InputRegister
{
    [Required][Key]
    public int ProjectId { get; set; }
    public int SerialNumber { get; set; }
    public string? ProjectTitle { get; set; }
    public string? DataReceived { get; set; }
        
    [DataType(DataType.Date)]
    public DateTime ReceivedDate { get; set; }
    public string? ReceivedFrom { get; set; }
    public string? ProjectManagerName { get; set; }
    public string? FilesFormat { get; set; }
    public int NumberofFiles { get; set; }
    public bool FitforPurpose { get; set; }
    public bool Check { get; set; }
    public string? CheckedBy { get; set; }
    [Display(Name = "Checked Date")]
    [DataType(DataType.Date)]
    public DateTime CheckedDate { get; set; }

    public string? Custodian { get; set; }
    public string? StoragePath { get; set; }
    public string? Remarks { get; set; }

}