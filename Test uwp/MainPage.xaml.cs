using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Test_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public System.Collections.ObjectModel.ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>();
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HmburgerMenu()
        {
            mySplit.IsPaneOpen = !mySplit.IsPaneOpen;
        }

       
    private async void RunAsyncDialog()
    {
        MessageDialog closDilog = new MessageDialog("");
        closDilog.Title = "Closing Confirmation";
        closDilog.Content = "are you sure about it ?";
        closDilog.Commands.Add(new UICommand() { Id = 0, Label = "Yes" });
        closDilog.Commands.Add(new UICommand() { Id = 1, Label = "No" });

        IUICommand res = await closDilog.ShowAsync();
        if ((int)res.Id == 0)
        {
            Application.Current.Exit();
        }
    }
        private async void CamBtn()
        {
            CameraCaptureUI cam = new CameraCaptureUI();

            cam.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Png;
            cam.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.HighestAvailable;
            // cam.PhotoSettings.CroppedAspectRatio = new Size(400, 400);
            //cam.PhotoSettings.
            cam.PhotoSettings.CroppedSizeInPixels = new Size(400, 400);
            StorageFile file = await cam.CaptureFileAsync(CameraCaptureUIMode.Photo);

            ImagePath.Text = file.Path;


            MainImg.Source = new BitmapImage(new Uri(file.Path));
        }
     private void ChangeColor()
        {

        }
        private void AddComment()
        {

        }
        private async void Border_Drop(object sender, DragEventArgs e)
        {
            // dataview is snoop in to the file..
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                //any file (>0)
                if (items.Any())
                {
                    StorageFile selected = items[0] as StorageFile;
                    var type = selected.ContentType;
                    //if image
                    if (type == "image/jpeg")
                    {
                        var imgCopy = await selected.CopyAsync(ApplicationData.Current.LocalFolder, selected.Name, NameCollisionOption.ReplaceExisting);
                        MainImg.Source = new BitmapImage(new Uri(imgCopy.Path));

                        //  Images.Add(new Image() {Height=100, Width=100,  Source = new BitmapImage(new Uri(imgCopy.Path)) });
                        //Images.Add(imgCopy );
                        Images.Add(imgCopy.Path);
                    }

                    //if mp3
                    //if (type == "audio/wav" || type == "audio/mpeg")
                    //{
                    //    StorageFile musicFile = await selected.CopyAsync(ApplicationData.Current.LocalFolder);
                    //    player.SetSource(await musicFile.OpenAsync(FileAccessMode.Read), type);
                    //    player.AutoPlay = true;
                    //    //player.SetSource()
                    //}

                }
            }
        }

        private void Border_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }
    }
}
    

