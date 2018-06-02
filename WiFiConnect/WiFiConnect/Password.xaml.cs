using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace WiFiConnect
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class Password : Page
    {
        private String SSID = "";

        public Password()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.Current.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String Pass = PassBox.Password;
            Cache.SaveToCache(SSID, Pass);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SSID = (String)e.Parameter;
            PassBox.Password = await Cache.GetPassFromCache(SSID);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
