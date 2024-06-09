
using System.Collections.ObjectModel;

using System.Windows;

using WpfApp3.Models;
using WpfApp3.ViewModels;

namespace WpfApp3.Views
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public Editor(PatientData CurrentData)
        {
            InitializeComponent();
            DataContext = new EditorViewModel(CurrentData);

        }
    }
}
