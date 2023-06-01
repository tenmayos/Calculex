using Calculex.Core;

namespace Calculex.Views;

public partial class MainPage : ContentPage
{
    private MathProcessor Mp;
    private bool OperatorPressed;
    private bool IsBetweenParentheses;
    public MainPage()
	{
		InitializeComponent();

        Mp = new MathProcessor();
        OperatorPressed = false;
        IsBetweenParentheses = false;
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
        
        if (IsBetweenParentheses)
        {
            return;
        }

        if (!OperatorPressed)
        {
            rslt.Text = input.Text;
            return;
        }

        rslt.Text = Mp.Compute(input.Text).ToString();
    }
    private void OnOperatorButtonClicked(object sender, EventArgs e)
    {
        // Checks if the last character is an operator or not.

        char lastCharacter = input.Text[input.Text.Length - 1];
        if (!Mp.IsMathOperator(lastCharacter) && lastCharacter != '(')
        {
            string op = (sender as Button).ClassId;
            OperatorPressed = true;
            input.Text += op;
        }
    }
    private void OnResetClicked(object sender, EventArgs e)
    {
        input.Text = "0";
        rslt.Text = "0";
        OperatorPressed = false;
        IsBetweenParentheses = false;
    }

    private void OnParenthesisButtonClicked(object sender, EventArgs e)
    {
        if (!IsBetweenParentheses && input.Text == "0")
        {
            IsBetweenParentheses = true;
            input.Text = "(";
            return;
        }

        char lastChar = input.Text[input.Text.Length - 1];

        if (!IsBetweenParentheses && Mp.IsMathOperator(lastChar))
        {
            IsBetweenParentheses = true;
            input.Text += "(";
            return;
        }

        if (IsBetweenParentheses && lastChar != '(')
        {
            IsBetweenParentheses = false;
            input.Text += ")";
            rslt.Text = Mp.Compute(input.Text).ToString();
            return;
        }
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (input.Text.Length <= 1)
        {
            OnResetClicked(sender, e);
            return;
        }

        int index = input.Text.Length - 1;

        char lastCharacter = input.Text[index];

        input.Text = input.Text.Remove(index);

        if (lastCharacter == ')')
        {
            IsBetweenParentheses = true;
            return;
        }
        if (!IsBetweenParentheses)
        {
            rslt.Text = Mp.Compute(input.Text).ToString();
        }
    }
}

