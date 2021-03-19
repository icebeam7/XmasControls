using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XmasControls
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Views.XmasDecorationView();
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
