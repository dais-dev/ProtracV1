using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DAISv1.Models;


public class User
{
    [Required][Key][AllowNull]
    public string UserName { get; set; }
    [AllowNull]
    public string Password { get; set; }
}