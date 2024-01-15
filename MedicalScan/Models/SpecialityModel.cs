using System.ComponentModel.DataAnnotations;

namespace MedicalScan.Models;

public class SpecialityModel
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Code { get; set; }
    
    public ICollection<DoctorsModel> Doctors { get; set; }
}
