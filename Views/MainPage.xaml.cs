using Calculex.Core;

namespace Calculex.Views;

public partial class MainPage : ContentPage
{
    private MathProcessor mp;
    public MainPage()
	{
		InitializeComponent();
        mp = new MathProcessor();
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

    }
    #endregion

    private void OnNumberButtonClicked(object sender, EventArgs e)
    {
        bool inputHasValue = input.Text.Length > 0;
        var btn = (sender as Button);

        if (inputHasValue && input.Text != "0")
        {
            input.Text += btn.Text;
        }
        else
        {
            input.Text = btn.Text;
        }
    }
    private void OnOperatorButtonClicked(object sender, EventArgs e)
    {
        // Checks if the last character is an operator or not.

        string lastCharacter = input.Text[input.Text.Length - 1].ToString();
        if (!mp.IsMathOperator(lastCharacter))
        {
            input.Text += (sender as Button).ClassId;
        }
    }
    private void OnResetClicked(object sender, EventArgs e)
    {
        input.Text = "0";
        rslt.Text = "0";
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if(input.Text.Length <= 1)
        {
            input.Text = "0";
            return;
        }

        input.Text = input.Text.Remove(input.Text.Length - 1);
    }
}

