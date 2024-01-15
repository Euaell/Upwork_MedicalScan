namespace MedicalScan.ViewModel;

public class DoctorViewModel
{
    public int Id { get; set; } // Assuming an ID for database operations
    public string Name { get; set; }
    public IEnumerable<string> Specialties { get; set; }
}
