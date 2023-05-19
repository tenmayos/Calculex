namespace Calculex.Views;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
        btn.Focused += Btn_Focused;
        btn.Unfocused += Btn_Unfocused;
    }

    private void Btn_Unfocused(object sender, FocusEventArgs e)
    {
		btn.BackgroundColor = Colors.Blue;
    }

    private void Btn_Focused(object sender, FocusEventArgs e)
    {
		btn.BackgroundColor = Colors.Red;
    }

    private void OnMouseEnter(object sender, EventArgs e)
	{
        btn.BackgroundColor = Colors.Red;
	}
    private void OnMouseExit(object sender, EventArgs e)
    {
        btn.BackgroundColor = Colors.White;
    }
}

