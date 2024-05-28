using System.Windows;
using WpfApp3.Models;
using WpfApp3.ViewModels;

namespace WpfApp3.Views
{
    public partial class ImagesWindow : Window
    {
        public ImagesWindow(PatientData selectedPatient)
        {
            InitializeComponent();
            DataContext = new ImagesViewModel(selectedPatient);
        }
    }
}
