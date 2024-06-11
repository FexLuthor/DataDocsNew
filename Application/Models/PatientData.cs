using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class PatientData
    {
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? BirthDate { get; set; }
        public int Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
        public AddressData? Address { get; set; }
        public ObservableCollection<PatientData>? Patients { get; set; }
        public PatientData? SelectedPatient { get; set; }
        public string? SearchQuery { get; set; }
        public List<ImageData>? Images { get; set; }
    }

    public class AddressData
    {
        public int? AddressId { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Town { get; set; }
        public int? PostalCode { get; set; }
        public int? PatientId { get; set; }
    }
}
