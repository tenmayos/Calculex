using System.Runtime.Serialization;

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
    private void OnTextChange(object sender, EventArgs e)
    {
        // Process the input text from here.
        input.Text += entry.Text;
    }

    private void OnLabelClicked(object sender, EventArgs e)
    {
        // Figure out how to hook the entry here, Focus() was a bit buggy, investigate further.
        entry.Focus();
    }
    #endregion
}

