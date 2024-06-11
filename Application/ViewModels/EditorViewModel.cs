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
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WpfApp3.ViewModels
{
    public partial class EditorViewModel : ObservableObject
    {
        public PatientData _currentData;

        
        private string? genderString;
        public EditorViewModel(PatientData currentData)
        {
            _currentData = currentData;
            LoadData(currentData);
            EditPatientCommand = new AsyncRelayCommand(EditPatientAsync);
            CancelCommand = new RelayCommand(Cancel);
        }
        public string GenderString
        {
            get => genderString;
            set
            {
                if (genderString != value)
                {
                    genderString = value;
                    OnPropertyChanged();
                }
            }
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
        public void LoadData(PatientData currentData)
        {
            _currentData = currentData;
            switch (_currentData.Gender)
            {
                case 0:
                    genderString = "male";
                    break;
                case 1:
                    genderString = "female";
                    break;
                case 2:
                    genderString = "diverse";
                    break;
                case 3:
                    genderString = "other";
                    break;
                default:
                    genderString = "";
                    break;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private async Task EditPatientAsync()
        {
            int Gender = 0;
            switch (genderString)
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

            string AlteredPatient = $@"{{
                ""patientId"":{_currentData.PatientId},
                ""firstName"":""{_currentData.FirstName}"",
                ""lastName"":""{_currentData.LastName}"",
                ""birthDate"":""{_currentData.BirthDate}"",
                ""gender"":{Gender},
                ""phone"":""{_currentData.Phone}"",
                ""email"":""{_currentData.Email}"",
                ""notes"":""{_currentData.Notes}"",
                ""images"": [],
                ""address"": {{
                 ""addressId"": {_currentData.Address.AddressId},
                 ""street"": ""{_currentData.Address.Street}"",
                 ""number"": ""{_currentData.Address.Number}"",
                 ""town"": ""{_currentData.Address.Town}"",
                 ""postalCode"": ""{_currentData.Address.PostalCode}"",
                 ""patientId"": {_currentData.Address.PatientId}
                }}}}";
                

            bool success = await ApiService.EditPatient(AlteredPatient,_currentData.PatientId);
            if (success)
            {
                // _patients.Add(newPatient);
                // Close the window
                
                MessageBox.Show("Changes have been saved. You can close the window now.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to edit patient data. Please check your entries.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel()
        {
            // Close the window
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }
    }
}
