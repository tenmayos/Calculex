using Calculex.Core;

namespace Calculex.Views;

public partial class MainPage : ContentPage
{
    private MathProcessor Mp;
    private static FormattedString formattedString;
    private int indexOfX;
    private bool OperatorPressed;
    private bool IsBetweenParentheses;
    private bool IsCompletingEq;
    public MainPage()
	{
		InitializeComponent();

        indexOfX = -1;
        Mp = new MathProcessor();
        formattedString = new FormattedString();
        formattedString.Spans.Add(new Span { Text = "0" });
        input.FormattedText = formattedString;
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
        bool inputHasValue = input.FormattedText.Spans.Count > 0;
        var lastSpan = input.FormattedText.Spans[input.FormattedText.Spans.Count - 1];
        var lastCharacter = lastSpan.Text[lastSpan.Text.Length - 1];

        var btn = (sender as Button);

        string digit = btn.Text;
        if (IsCompletingEq)
        {
            Span targetSpan = input.FormattedText.Spans[indexOfX];

            if (targetSpan.Text == "X")
            {
                targetSpan.Text = digit;
            }
            else
            {
                targetSpan.Text += digit;
            }
            rslt.Text = Mp.Compute(formattedString.ToString()).ToString();
            return;
        }

        if (inputHasValue && input.FormattedText.Spans.First().Text != "0")
        {
            if (lastCharacter == 'X') return;

            WriteDigit(digit);
        }
        else
        {
            WriteDigit(digit, true);
        }

        OperatorPressed = false;
        rslt.Text = Mp.Compute(formattedString.ToString()).ToString();
    }
    private void OnOperatorButtonClicked(object sender, EventArgs e)
    {
        // Checks if the last character is an operator or not.
        if (IsCompletingEq) return;

        var lastSpanText = input.FormattedText.Spans[input.FormattedText.Spans.Count - 1].Text;
        char lastChar = char.Parse(lastSpanText);

        if (!OperatorPressed && lastChar != '(')
        {
            string op = (sender as Button).ClassId;
            OperatorPressed = true;
            WriteDigit(op);
        }
    }
    private void OnResetClicked(object sender, EventArgs e)
    {
        rslt.Text = "0";
        indexOfX = -1;
        OperatorPressed = false;
        IsBetweenParentheses = false;
        IsCompletingEq = false;

        ResetSpanColors();
        formattedString = new();
        formattedString.Spans.Add(new Span { Text = "0" });
        input.FormattedText = formattedString;
    }

    private void OnParenthesisButtonClicked(object sender, EventArgs e)
    {
        char lastChar = char.Parse(input.FormattedText.Spans[input.FormattedText.Spans.Count - 1].Text);

        if (!IsBetweenParentheses && input.FormattedText.Spans[0].Text == "0")
        {
            IsBetweenParentheses = true;
            WriteDigit("(", true);
            return;
        }

        if (!IsBetweenParentheses && Mp.IsMathOperator(lastChar))
        {
            IsBetweenParentheses = true;
            WriteDigit("(");
            return;
        }

        if (IsBetweenParentheses && lastChar != '(')
        {
            IsBetweenParentheses = false;
            WriteDigit(")");
            rslt.Text = Mp.Compute(formattedString.ToString()).ToString();
            return;
        }
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        int currentLength = formattedString.Spans.Count;

        if (currentLength <= 1)
        {
            OnResetClicked(sender, e);
            return;
        }

        

        
        if (IsCompletingEq)
        {
            Span xSpan = formattedString.Spans[indexOfX];

            if (xSpan.Text.Length <= 1)
            {
                xSpan.Text = "X";
            }
            else
            {
                xSpan.Text = xSpan.Text.Remove(xSpan.Text.Length - 1);
                ResetSpanColors();
                xSpan.BackgroundColor = Colors.Orange;
                xSpan.TextColor = Colors.Black;
            }

            input.FormattedText = formattedString;
            return;
        }

        int index = currentLength - 1;

        formattedString.Spans.RemoveAt(index);

        char lastCharacter = char.Parse(formattedString.Spans[index].Text);

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
            rslt.Text = Mp.Compute(formattedString.ToString()).ToString();
        }
        input.FormattedText = formattedString;
    }

    private void OnVariableButtonClicked(object sender, EventArgs e)
    {
        int currentLength = input.FormattedText.Spans.Count;
        char lastChar = char.Parse(input.FormattedText.Spans[currentLength - 1].Text);
        
        if (currentLength > 1)
        {
            if (int.TryParse(lastChar.ToString(), out _))
            {
                return;
            }
            WriteDigit("X");
        }
        else
        {
            WriteDigit("X", true);
        }
        OperatorPressed = false;
    }

    private async void OnAddEquationClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync(title: "Save Equation?", message: "Equation Name", placeholder: "Name", keyboard: Keyboard.Default);

        if (result == null) return;

        if (result == "")
        {
            await DisplayAlert("Failed", "Your Equation name needs to have atleast 1 character", "Ok!");
            return;
        }
        // Do saving logic here.
        await DisplayAlert("Success", $"Your Equation {result} was saved successfully", "Hurray!");
    }

    private void OnNextVariableClicked(object sender, EventArgs e)
    {
        bool canContinue = false;

        for (int i = 0; i < formattedString.Spans.Count; i++)
        {
            if (formattedString.Spans[i].Text == "X")
            {
                canContinue = true;
                break;
            }
        }

        if (canContinue == false)
        {
            IsCompletingEq = false;
            return;
        }
        if (IsCompletingEq) ResetSpanColors();

        IsCompletingEq = true;
        
        for (int i = 0; i < formattedString.Spans.Count; i++)
        {
            if (formattedString.Spans[i].Text == "X")
            {
                formattedString.Spans[i].BackgroundColor = Colors.Orange;
                formattedString.Spans[i].TextColor = Colors.Black;
                indexOfX = i;
                input.FormattedText = formattedString;
                break;
            }
        }
    }

    private void WriteDigit(string digit, bool isFirstTime = false)
    {
        if (isFirstTime)
        {
            formattedString.Spans.RemoveAt(0);
        }
        formattedString.Spans.Add(new Span { Text = digit });
        input.FormattedText = formattedString;
    }

    private void ResetSpanColors(bool synchronize = false)
    {
        foreach (var span in input.FormattedText.Spans)
        {
            span.BackgroundColor = null;
            span.TextColor = Colors.White;
        }
        if (synchronize)
        {
            foreach (var span in formattedString.Spans)
            {
                span.BackgroundColor = null;
                span.TextColor = Colors.White;
            }
        }
    }
}

