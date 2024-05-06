namespace DeviceOrientation
{
    using DeviceOrientation.Platform;
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Landscape_Clicked(object sender, EventArgs e)
        {
            ScreenOrientation.Set(DisplayOrientation.Landscape);
        }

        private void Portrait_Clicked(object sender, EventArgs e)
        {
            ScreenOrientation.Set(DisplayOrientation.Portrait);
        }

        private void Device_Clicked(object sender, EventArgs e)
        {
            ScreenOrientation.Set(DisplayOrientation.Unknown);
        }
    }
}
