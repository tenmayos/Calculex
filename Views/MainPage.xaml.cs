using Calculex.Core;

namespace Calculex.Views;

public partial class MainPage : ContentPage
{
    private MathProcessor Mp;
    private int indexOfX;
    private bool OperatorPressed;
    private bool IsBetweenParentheses;
    private bool IsCompletingEq;
    public MainPage()
	{
		InitializeComponent();

        indexOfX = -1;
        Mp = new MathProcessor();
        OperatorPressed = false;
        IsBetweenParentheses = false;
        IsCompletingEq = false;
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
        indexOfX = -1;
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

    private void OnAddEquationClicked(object sender, EventArgs e)
    {

    }

    private void OnNextVariableClicked(object sender, EventArgs e)
    {
        if (input.Text == "" || input.Text == null || !input.Text.Contains('X'))
            return;

        IsCompletingEq = true;
        indexOfX = input.Text.IndexOf("X") + 1;

        string firstPart = input.Text.Substring(0, input.Text.Length - indexOfX);
        string lastPart = input.Text.Substring(indexOfX, input.Text.Length - indexOfX);

        var formattedStr = new FormattedString();

        Span firstHalf = new Span{ Text = firstPart };
        Span highlight = new Span { Text = "X", BackgroundColor = Colors.Orange };
        Span lastHalf = new Span { Text = lastPart };

        formattedStr.Spans.Add(firstHalf);
        formattedStr.Spans.Add(highlight);
        formattedStr.Spans.Add(lastHalf);

        input.FormattedText = formattedStr;
    }
}

