using System;

using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WatsonCosmosHackPart1
{
    public class ViewOlivePage : ContentPage
    {
        ToolbarItem addButtonToolBar;
        Button getDataButton;
        DogModel whiskey = new DogModel() { Name = "Wiskey", FurColor = "Gold", Id = "2" };
        ListView dogListView = new ListView();

        public ViewOlivePage()
        {
            var nameLabel = new Label { BackgroundColor = Color.LightBlue };
            var furColorLabel = new Label { BackgroundColor = Color.LightBlue };
            getDataButton = new Button { Text = "Get data from Cosmos" };
            addButtonToolBar = new ToolbarItem
            {
                Text = "+"
            };
            ToolbarItems.Add(addButtonToolBar);

            dogListView.ItemTemplate = new DataTemplate(typeof(DogTextCell));

            Content = dogListView;

        }

        public async Task UpdateListViewItemSource()
        {
			var results = await DocumentDbService.GetAllDogsAsync();
			Device.BeginInvokeOnMainThread(() => dogListView.ItemsSource = results);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            getDataButton.Clicked += GetDataButton_Clicked;
            addButtonToolBar.Clicked += AddButtonToolBar_Clicked;

            Task.Run(async () => await UpdateListViewItemSource());

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            getDataButton.Clicked -= GetDataButton_Clicked;
            addButtonToolBar.Clicked -= AddButtonToolBar_Clicked;
        }

        async void GetDataButton_Clicked(object sender, EventArgs e)
        {
            await DocumentDbService.PostDogAsync(whiskey);
        }

        void AddButtonToolBar_Clicked(object sender, EventArgs e)
        {
            //if (Application.Current.MainPage.Navigation.NavigationStack.Any().Equals((dynamic)AddDogPage))
            //{
                Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new AddDogPage()));
            //}
            //else
            //{
            //    Device.BeginInvokeOnMainThread(() => Navigation.PopAsync());
            //}
        }
    }
}

