using System.Windows;
using WpfApp3.Models;
using WpfApp3.ViewModels;
namespace WpfApp3.Views
{
    /// <summary>
    /// Interaktionslogik für AddressWindow.xaml
    /// </summary>
    public partial class AddressWindow : Window
    {
        public AddressWindow(PatientData selectedPatient)
        {
            InitializeComponent();
            DataContext = new AddressViewModel(selectedPatient);
        }
    }
}
