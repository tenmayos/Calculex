namespace Calculex.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
    }
    #region Behavior Events
    private void OnMouseEnter(object sender, EventArgs e)
	{
        (sender as Button).Opacity = 0.9f;
	}
    private void OnMouseExit(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 1;
    }

    private void OnEntryFocused(object sender, EventArgs e)
    {
        input.TextColor = Colors.Green;
    }

    private void OnEntryUnfocused(object sender, EventArgs e)
    {
        input.TextColor = Colors.White;
    }

    private void OnTextChange(object sender, EventArgs e)
    {
        input.Text = entry.Text;
    }

    private void OnLabelSelected(object sender, EventArgs e)
    {
        entry.Focus();
    }
    #endregion
}

