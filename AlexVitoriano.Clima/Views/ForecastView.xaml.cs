using AlexVitoriano.Clima.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexVitoriano.Clima.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastView : ContentPage
    {
        ForecastViewModel vm;
        public ForecastView()
        {
            InitializeComponent();
            vm = new ForecastViewModel();
            BindingContext = vm;

        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            var VM = (ForecastViewModel)this.BindingContext;

            try
            {              
                var permission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                var permissionAlways = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

                //Permissão Always
                if (permissionAlways != PermissionStatus.Granted)
                {
                    //Permissão "Só em uso"
                    if (permission != PermissionStatus.Granted)
                    {
                        if (!(PermissionStatus.Granted == await Permissions.RequestAsync<Permissions.LocationAlways>()))
                        {
                            if (!(PermissionStatus.Granted == await Permissions.RequestAsync<Permissions.LocationWhenInUse>()))
                            {
                                vm.IsLocationNotEnabled = true;
                                throw new Exception($"Para podermos exibir a previsão do tempo, precisamos que você habilite a Localização");
                            }
                            else
                                permissionAlways = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                        }
                        else
                            permission = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
                    }
                }
               

                if (permission == PermissionStatus.Granted || permissionAlways==PermissionStatus.Granted)
                {
                    vm.IsLocationNotEnabled = false;

                }
                else if (permission == PermissionStatus.Unknown || permissionAlways == PermissionStatus.Unknown)
                {
                    vm.IsLocationNotEnabled = true;
                    throw new Exception($"Não foi possível verificar a permissão de localização, favor sair do aplicativo e tentar novamente.");
                }
            }
            catch (Exception ex)
            {
                VM.DisplayAlert("AlexVitoriano.Clima",ex.Message,"OK");
            }
            finally
            {
                await Task.Factory.StartNew(async () =>
                {
                    await VM.RecuperaLocation();
                }).ContinueWith(
                       async task => { await VM.ExecuteLoadItems(); }
                       , TaskScheduler.FromCurrentSynchronizationContext());


            }

        }




    }
}