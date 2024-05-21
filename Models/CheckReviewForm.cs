using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protrac1.Models;

public class CheckReviewForm
{
    [Required][Key]
    public int ProjectId { get; set; }
    public int SerialNumber { get; set; }
    public string? ProjectTitle { get; set; }
    public int ActivityNumber { get; set; }
    public string? ActivityName { get; set; }
    public string? Objectives { get; set; }
    public string? ReferenceStandards { get; set; }
    public string? FileName { get; set; }
    public string? SpecificQualityIssues { get; set; }    
    public bool Completion { get; set; }     
    public string? CheckedBy { get; set; }  
    [DataType(DataType.Date)]
    public DateTime CheckedByDate { get; set; }
    public string? ApprovedBy { get; set; } 
    [DataType(DataType.Date)]
    public DateTime ApprovedByDate { get; set; }
    public string? ActionTaken { get; set; }

}