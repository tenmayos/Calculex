namespace Calculex.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
    }

    private void OnMouseEnter(object sender, EventArgs e)
	{
        (sender as Button).Opacity = 0.8f;
	}
    private void OnMouseExit(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 1;
    }
}

