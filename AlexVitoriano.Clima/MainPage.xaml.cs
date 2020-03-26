using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexVitoriano.Clima
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

           // tudo.BackgroundColor = tempToRGB(33d);
        }

        public Color tempToRGB(double temp)
        {
            double red = 198d / 255d;
            double green = 0;
            double blue = 29d / 255d;

           return new Color(red, green, blue, 1d);
        }
    }
}
