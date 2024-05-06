using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protrac1.Models;

public class JobStartForm
{
    [Required][Key]
    public int ProjectId { get; set; }
    public int SerialNumber { get; set; }
    public string? ProjectTitle { get; set; }
    public string? ProjectDetail { get; set; }
    public string? ClientName { get; set; }
    public string? ProjectManagerName { get; set; }
    public string? TypeofJob { get; set; }
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    [Display(Name = "ReNewal Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    public string? Sector { get; set; }
    public string? Office { get; set; }
    public string? Region { get; set; }
    public string? SPMName { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal EstimatedProjectCost { get; set; }
    public int Duration { get; set; }
}