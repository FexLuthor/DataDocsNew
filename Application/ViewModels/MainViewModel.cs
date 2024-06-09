using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfApp3.Models;
using WpfApp3.Services;
using System;
using WpfApp3.Views;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace WpfApp3.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<PatientData>? patients = new ObservableCollection<PatientData>();

        [ObservableProperty]
        private PatientData? selectedPatient;

        [ObservableProperty]
        private string? searchQuery = string.Empty;

        [ObservableProperty]
        private string? errorMessage = string.Empty;

        public MainViewModel()
        {
            Patients = new ObservableCollection<PatientData>();
            SearchPatientsCommand = new RelayCommand(async () => await SearchPatientsAsync());
            NavigateToAddressCommand = new RelayCommand(NavigateToAddress);
            NavigateToImagesCommand = new RelayCommand(NavigateToImages);
            AddPatientCommand = new RelayCommand(AddPatient);
            LogoutCommand = new RelayCommand(Logout);
            EditPatientCommand = new RelayCommand(EditPatient);
        }

        public IRelayCommand SearchPatientsCommand { get; }
        public IRelayCommand NavigateToAddressCommand { get; }
        public IRelayCommand NavigateToImagesCommand { get; }
        public IRelayCommand AddPatientCommand { get; }
        public IRelayCommand LogoutCommand { get; }
        public ICommand EditPatientCommand { get; }

        public async Task SearchPatientsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                ErrorMessage = "Please enter a search query.";
                return;
            }

            try
            {
                var patientDataList = await ApiService.GetPatientsAsync();
                if (patientDataList != null)
                {
                    Patients?.Clear();
                    foreach (var patient in patientDataList)
                    {
                        if (patient.FirstName.Contains(SearchQuery) || patient.LastName.Contains(SearchQuery))
                        {
                            Patients?.Add(patient);
                        }
                    }

                    if (Patients == null || Patients.Count == 0)
                    {
                        ErrorMessage = "No patients found.";
                    }
                    else
                    {
                        ErrorMessage = string.Empty;
                    }
                }
                else
                {
                    ErrorMessage = "Failed to load patient data.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
        }

        private void NavigateToAddress()
        {
            AddressWindow addressWindow = new Views.AddressWindow(SelectedPatient);
            addressWindow.Show();
        }

        private void NavigateToImages()
        {
            if (SelectedPatient != null)
            {
                var imagesWindow = new Views.ImagesWindow(SelectedPatient);
                imagesWindow.Show();
            }
        }

        private void AddPatient()
        {
            var addPatientView = new Views.AddPatientView(patients);
            addPatientView.Show();
        }

        private void EditPatient()
        {
            var addPatientView = new Views.Editor(selectedPatient);
            addPatientView.Show();
        }
        private void Logout()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();

                // Schließen Sie alle Fenster außer das Login-Fenster
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() != typeof(LoginWindow))
                    {
                        window.Close();
                    }
                }
            });
        }
    }
}
