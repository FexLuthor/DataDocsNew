using CommunityToolkit.Mvvm.ComponentModel;
using WpfApp3.Models;

namespace WpfApp3.ViewModels
{
    public partial class AddressViewModel : ObservableObject
    {
        [ObservableProperty]
        private PatientData selectedPatient;

        public AddressViewModel(PatientData selectedPatient)
        {
            SelectedPatient = selectedPatient;
        }
    }
}
