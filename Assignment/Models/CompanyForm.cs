using Assignment.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models;

internal class CompanyForm
{
    public string CompanyName { get; set; } = null!;
    public string CompanyDescription { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Branch { get; set; } = null!;
}