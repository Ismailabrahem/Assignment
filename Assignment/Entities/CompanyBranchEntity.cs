using System.ComponentModel.DataAnnotations;

namespace Assignment.Entities;

public class CompanyBranchEntity
{
    [Key]
    public int Id { get; set; }
    public string Branch { get; set; } = null!;
}
