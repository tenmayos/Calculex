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

    private void OnLabelClicked(object sender, EventArgs e)
    {
        // Figure out how to hook the entry here, Focus() was a bit buggy, investigate further.
        entry.Focus();
    }
    #endregion

    private void OnTextChange(object sender, EventArgs e)
    {
        // Process the input text from here.
        bool entryHasValue = entry.Text.Length > 0;
        
        if (entryHasValue)
        {
            // Since the entry has value, then the first character isn't null
            if (entry.Text[0] == '0')
            {
                entry.Text = "";
            }
            else
            {
                input.Text = entry.Text;
            }
        }
        else
        {   
            input.Text = "0";
            entry.Text = "";
        }
    }

    private void OnNumberButtonClicked(object sender, EventArgs e)
    {
        bool entryHasValue = entry.Text.Length > 0;
        var btn = (sender as Button);

        if (entryHasValue && entry.Text != "0")
        {
            input.Text += btn.Text;
            entry.Text += btn.Text;
        } 
        else
        {
            input.Text = btn.Text;
            entry.Text += btn.Text;
        }
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        entry.Text = "";
        input.Text = "0";
        rslt.Text = "0";
        entry.Focus();
    }
}

