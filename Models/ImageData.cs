using System;
using System.Net;
using System.Windows.Media.Imaging;

namespace WpfApp3.Models
{
    public class ImageData
    {
        private const string BaseUrl = "https://www.datadoctors.patpatwithhat.de/";
        private const string Username = "docData";
        private const string Password = "DataIsMyJam321#";

        public int? ImageId { get; set; }
        public string? FilePath { get; set; }
        public string? ContentType { get; set; }
        public string? UploadDate { get; set; }
        public int? PatientId { get; set; }

        //No Singelton anymore, in the future sum up with the API call 
        public BitmapImage ImageBitmap
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    Console.WriteLine("FilePath is null or empty.");
                    return null;
                }

                try
                {
                    var uri = new Uri(BaseUrl + FilePath, UriKind.Absolute);
                    Console.WriteLine($"Loading image from URL: {uri}");

                    using (var webClient = new WebClient())
                    {
                        webClient.Credentials = new NetworkCredential(Username, Password);
                        byte[] imageBytes = webClient.DownloadData(uri);

                        var bitmap = new BitmapImage();
                        using (var stream = new System.IO.MemoryStream(imageBytes))
                        {
                            bitmap.BeginInit();
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.StreamSource = stream;
                            bitmap.EndInit();
                            bitmap.Freeze(); 
                        }
                        return bitmap;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading image: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
