using AlexVitoriano.Clima.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexVitoriano.Clima
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ForecastView();
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
