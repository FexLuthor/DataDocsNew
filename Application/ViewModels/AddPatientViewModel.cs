using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using WpfApp3.Models;
using WpfApp3.Services;

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
        private string gender;

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string notes;

        [ObservableProperty]
        private string street;

        [ObservableProperty]
        private string number;

        [ObservableProperty]
        private string town;

        [ObservableProperty]
        private string postalCode;

        public AddPatientViewModel(ObservableCollection<PatientData> patients)
        {
            _patients = patients;
            AddPatientCommand = new AsyncRelayCommand(AddPatientAsync);
            CancelCommand = new RelayCommand(Cancel);
        }

        public IAsyncRelayCommand AddPatientCommand { get; }
        public IRelayCommand CancelCommand { get; }

        private async Task AddPatientAsync()
        {
            int Gender = 0;
            switch (gender)
            {
                case "male":
                    Gender = 0;
                    break;
                case "female":
                    Gender = 1;
                    break;
                case "diverse":
                    Gender = 2;
                    break;
                case "other":
                    Gender = 3;
                    break;
                default:
                    Gender = 3;
                    break;
            }
            string newPatient = $@"{{
                ""patientId"":0,
                ""firstName"":""{firstName}"",
                ""lastName"":""{lastName}"",
                ""birthDate"":""{birthDate}"",
                ""gender"":{Gender},
                ""phone"":""{phone}"",
                ""email"":""{email}"",
                ""notes"":""{notes}"",
                ""images"": [],
                ""address"": {{
                 ""addressId"": 0,
                  ""street"": ""{street}"",
                  ""number"": ""{number}"",
                  ""town"": ""{town}"",
                  ""postalCode"": ""{postalCode}"",
                  ""patientId"": 0
                }}}}";

            bool success = await ApiService.AddPatientAsync(newPatient);
            if (success)
            {
                // _patients.Add(newPatient);
                // Close the window
                MessageBox.Show("Succesfully added patient to the backend.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
            }
            else
            {
                MessageBox.Show("Failed to add patient to the backend. Please check your entries.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel()
        {
            // Close the window
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }
    }
}
