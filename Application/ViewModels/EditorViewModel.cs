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
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.Json;

namespace WpfApp3.ViewModels
{
    public partial class EditorViewModel : ObservableObject
    {
        public PatientData _currentData;

        public EditorViewModel(PatientData currentData)
        {
            _currentData = currentData;        
            EditPatientCommand = new AsyncRelayCommand(EditPatientAsync);
            CancelCommand = new RelayCommand(Cancel);
        }

        public IAsyncRelayCommand EditPatientCommand { get; }
        public IRelayCommand CancelCommand { get; }

        public PatientData CurrentData
        {
            get { return _currentData; }
            set
            {
                if (_currentData != value)
                {
                    _currentData = value;
                    OnPropertyChanged(nameof(CurrentData));
                }
            }
        }
        private async Task EditPatientAsync()
        {
            // Validate input and add patient logic here
            //                ""birthDate"":""{_currentData.BirthDate}"",
            //var maxId = _patients.Any() ? _patients.Max(p => p.PatientId) : 0;
            //string jsonbirthday = JsonConvert.SerializeObject(_currentData.BirthDate);
            string AlteredPatient = $@"{{
                ""patientId"":{_currentData.PatientId},
                ""firstName"":""{_currentData.FirstName}"",
                ""lastName"":""{_currentData.LastName}"",
                ""gender"":{_currentData.Gender},
                ""phone"":""{_currentData.Phone}"",
                ""email"":""{_currentData.Email}"",
                ""notes"":""{_currentData.Notes}""}}";

            bool success = await ApiService.EditPatient(AlteredPatient,_currentData.PatientId);
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
