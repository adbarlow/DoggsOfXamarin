using System;
using Xamarin.Forms;
using Microsoft.Azure.Documents.SystemFunctions;
using System.Linq;
namespace WatsonCosmosHackPart1
{
    public class DogTextCell : TextCell
    {
        MenuItem deleteAction;

        public DogTextCell()
        {
            this.SetBinding(TextCell.TextProperty, "Name");
            this.SetBinding(TextCell.DetailProperty, "FurColor");
           
            deleteAction = new MenuItem
            {
                Text = "Delete",
                IsDestructive = true
            };
            ContextActions.Add(deleteAction);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            deleteAction.Clicked += DeleteAction_Clicked;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            deleteAction.Clicked -= DeleteAction_Clicked;
        }

        async void DeleteAction_Clicked(object sender, EventArgs e)
        {
            var dogSelected = BindingContext as DogModel;

            var mainPage = Application.Current.MainPage as NavigationPage;
            var viewOlivePage = mainPage.CurrentPage as ViewOlivePage;

            //var viewOlivePage = mainPage.Navigation.NavigationStack.FirstOrDefault() as ViewOlivePage;

			await DocumentDbService.DeleteDogAsync(dogSelected);

			await viewOlivePage.UpdateListViewItemSource();
        }
    }
}
