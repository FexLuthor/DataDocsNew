using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using WpfApp3.Models;

namespace WpfApp3.ViewModels
{
    public partial class AddPatientViewModel : ObservableObject
    {
        private readonly ObservableCollection<PatientData> _patients;

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string birthDate;

        [ObservableProperty]
        private int gender;

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string notes;

        public AddPatientViewModel(ObservableCollection<PatientData> patients)
        {
            _patients = patients;
            AddPatientCommand = new RelayCommand(AddPatient);
            CancelCommand = new RelayCommand(Cancel);
        }

        public IRelayCommand AddPatientCommand { get; }
        public IRelayCommand CancelCommand { get; }

        private void AddPatient()
        {
            // Validate input and add patient logic here

            var maxId = _patients.Any() ? _patients.Max(p => p.PatientId) : 0;

            var newPatient = new PatientData
            {
                PatientId = maxId + 1,
                FirstName = FirstName,
                LastName = LastName,
                //BirthDate = BirthDate,
                Gender = Gender,
                Phone = Phone,
                Email = Email,
                Notes = Notes,
               /* Address = new Address
                {
                    Street = "New Street",
                    Number = "1",
                    Town = "New Town",
                    PostalCode = "12345"
                },
                Images = new ObservableCollection<ImageData>()
               */
            };

            _patients.Add(newPatient);

            // Close the window
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }

        private void Cancel()
        {
            // Close the window
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }
    }
}
