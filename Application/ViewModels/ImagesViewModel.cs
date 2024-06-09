using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WpfApp3.Models;

namespace WpfApp3.ViewModels
{
    public partial class ImagesViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ImageData> images;

        [ObservableProperty]
        private ImageData selectedImage;

        private int _currentIndex;

        public ImagesViewModel(PatientData selectedPatient)
        {
            Images = new ObservableCollection<ImageData>(selectedPatient.Images);
            _currentIndex = 0;

            if (Images.Count > 0)
            {
                SelectedImage = Images[_currentIndex];
            }

            PreviousImageCommand = new RelayCommand(PreviousImage);
            NextImageCommand = new RelayCommand(NextImage);
        }

        public IRelayCommand PreviousImageCommand { get; }
        public IRelayCommand NextImageCommand { get; }

        private void PreviousImage()
        {
            if (Images.Count > 0)
            {
                _currentIndex = (_currentIndex - 1 + Images.Count) % Images.Count;
                SelectedImage = Images[_currentIndex];
            }
        }

        private void NextImage()
        {
            if (Images.Count > 0)
            {
                _currentIndex = (_currentIndex + 1) % Images.Count;
                SelectedImage = Images[_currentIndex];
            }
        }
    }
}
