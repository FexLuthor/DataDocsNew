using System.Windows;
using WpfApp3.Models;
using WpfApp3.ViewModels;
using System.Collections.ObjectModel;

namespace WpfApp3.Views
{
    public partial class AddPatientView : Window
    {
        public AddPatientView(ObservableCollection<PatientData> patients)
        {
            InitializeComponent();
            DataContext = new AddPatientViewModel(patients);

        }
    }
}
