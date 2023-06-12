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
    #endregion

    private void OnNumberButtonClicked(object sender, EventArgs e)
    {
        bool inputHasValue = input.Text.Length > 0;
        char lastCharacter = input.Text[input.Text.Length - 1];
        var btn = (sender as Button);
        
        if (inputHasValue && input.Text != "0")
        {
            if (lastCharacter == 'X') return;

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

    private void OnHistory1Clicked(object sender, EventArgs e)
    {

    }

    private void OnHistory2Clicked(object sender, EventArgs e)
    {

    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        int currentLength = input.Text.Length;

        if (currentLength <= 1)
        {
            OnResetClicked(sender, e);
            return;
        }

        int index = currentLength - 1;

        char lastCharacter = input.Text[index];

        input.Text = input.Text.Remove(index);

        if (lastCharacter == ')')
        {
            IsBetweenParentheses = true;
            return;
        }

        if (lastCharacter == '(')
        {
            IsBetweenParentheses = false;
        }

        if (!IsBetweenParentheses)
        {
            rslt.Text = Mp.Compute(input.Text).ToString();
        }
    }

    private void OnVariableButtonClicked(object sender, EventArgs e)
    {
        /*var formattedStr = new FormattedString();
        var existingStr = new Span { Text = input.Text };
        Span xSpan = new Span { TextColor = Colors.Red, Text = "X" };
        string currentInput = input.Text;

        formattedStr.Spans.Add(existingStr);
        formattedStr.Spans.Add(xSpan);

        input.FormattedText = formattedStr;
        input.Text = currentInput + "X";*/
        int currentLength = input.Text.Length;
        bool inputHasValue = currentLength > 0;
        
        if (inputHasValue && input.Text != "0")
        {
            char lastChar = input.Text[currentLength - 1];

            if (lastChar == 'X' || int.TryParse(lastChar.ToString(), out _))
            {
                return;
            }
            input.Text += "X";
        }
        else
        {
            input.Text = "X";
        }
    }
}

