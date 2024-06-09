using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp3.Models;
using WpfApp3.Services;
using WpfApp3.ViewModels;
using WpfApp3.Views;
using Xunit;

namespace WpfApp3.Tests
{
    public class ApiServiceTests
    {
        [Fact]
        public async Task CanReachBackend()
        {
            // Act
            var patients = await ApiService.GetPatientsAsync();

            // Assert
            if (patients != null)
            {
                // Successful connection
                System.Console.WriteLine("supi hat geklappt");
            }
            else
            {
                // Failed connection
                Assert.True(false, "Could not reach backend");
            }
        }
    }
    public class MainViewModelTests
    {
       [Fact]
        public async Task SearchPatientsAsync_ShouldReturnCorrectPatients()
        {
            // Arrange
            var viewModel = new MainViewModel();
            viewModel.Patients = new ObservableCollection<PatientData>
            {
                new PatientData { PatientId = 1, FirstName = "John", LastName = "Doe" },
                new PatientData { PatientId = 2, FirstName = "Jane", LastName = "Smith" },
                new PatientData { PatientId = 3, FirstName = "John", LastName = "Smith" }
            };
            viewModel.SearchQuery = "John";

            // Act
            await viewModel.SearchPatientsAsync();

            // Assert
            Assert.Equal(2, viewModel.Patients.Count);
            Assert.Contains(viewModel.Patients, p => p.FirstName == "John" && p.LastName == "Doe");
            Assert.Contains(viewModel.Patients, p => p.FirstName == "John" && p.LastName == "Smith");
        }
       
        [Fact]
        public void Logout_ShouldOpenLoginWindow()
        {
            // Arrange
            var viewModel = new MainViewModel();

            // Simulate opening MainWindow
            var mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();

            // Act
            viewModel.LogoutCommand.Execute(null);

            // Assert
            Assert.IsType<LoginWindow>(Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault());
            
        }
    }
}





namespace DataDoc
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arange 
            int valueA = 1;
            int valueB = 2;
            int valueExpected = 3;

            //Act

            int result = valueA + valueB;


            //Assert
            Assert.Equal(valueExpected, result);
        }
    }
}