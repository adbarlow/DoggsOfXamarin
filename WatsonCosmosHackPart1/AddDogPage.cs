using System;

using Xamarin.Forms;
using Plugin.Media;

namespace WatsonCosmosHackPart1
{
    public class AddDogPage : ContentPage
    {
        ToolbarItem saveDogButtonToolBar;
        Label _dogNameLabel = new Label { Text = "Enter Dog's Name: ", HorizontalOptions = LayoutOptions.Start };
        Entry _dogNameEntry = new Entry();
        Label _dogFurColorLabel = new Label { Text = "Enter Color of Dog's Coat: " };
        Entry _dogFurColorEntry = new Entry();
        Button _photoButton = new Button { Text = "Take photo" };
        Image _image = new Image { Source = "+" };

        public AddDogPage()
        {
            saveDogButtonToolBar = new ToolbarItem
            {
                Text = "Save"
            };
            ToolbarItems.Add(saveDogButtonToolBar);

            Content = new StackLayout
            {
                Margin = 20,
                Children = {
                    
                    _dogNameLabel,
                    _dogNameEntry,
                    _dogFurColorLabel,
                    _dogFurColorEntry,
					_photoButton,
					_image

                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            saveDogButtonToolBar.Clicked += SaveDogButtonToolBar_Clicked;
            _photoButton.Clicked += _photoButton_Clicked;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            saveDogButtonToolBar.Clicked -= SaveDogButtonToolBar_Clicked;
            _photoButton.Clicked -= _photoButton_Clicked;
        }

        async void SaveDogButtonToolBar_Clicked(object sender, EventArgs e)
        {
            DogModel dogTemplateSubmit = new DogModel() { Name = _dogNameEntry.Text, FurColor = _dogFurColorEntry.Text };

            await DocumentDbService.PostDogAsync(dogTemplateSubmit);
            Device.BeginInvokeOnMainThread(() => Navigation.PopAsync()); //ViewOlivePage()));
        }

        async void _photoButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
            {
                return;
            }

            await DisplayAlert("File Location", file.Path, "OK");

            _image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
    }
}

