using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class PutPostPatientData
    {
        public class PatientData
        {
            public int PatientId { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public DateTime? BirthDate { get; set; }
            public int Gender { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public string? Notes { get; set; }

            public List<ImageData>? Images { get; set; }
            public AddressData? Address { get; set; }
        }
    }
}