using System.ComponentModel.DataAnnotations;

namespace MedicalScan.Models;

public class DoctorsModel
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public ICollection<SpecialityModel> Specialties { get; set; }
}
