using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        private DateTime birthDate;

        [ObservableProperty]
        private int gender;

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string notes;

        [ObservableProperty]
        private ImageData images;

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
            // Validate input and add patient logic here

            //var maxId = _patients.Any() ? _patients.Max(p => p.PatientId) : 0;
            string newPatient = $@"{{
                ""patientId"":0,
                ""firstName"":""{firstName}"",
                ""lastName"":""{lastName}"",
                ""gender"":{gender},
                ""phone"":""{phone}"",
                ""email"":""{email}"",
                ""notes"":""{notes}""}}";

            bool success = await ApiService.AddPatientAsync(newPatient);
            if (success)
            {
               // _patients.Add(newPatient);
                // Close the window
                Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
            }
            else
            {
                MessageBox.Show("Failed to add patient to the backend.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel()
        {
            // Close the window
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }
    }
}
