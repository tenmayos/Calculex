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
        indexOfX = 0;
        Mp = new MathProcessor();
        formattedString = new FormattedString();
        //input.SetBinding(Label.FormattedTextProperty, "formattedString");
        
        formattedString.SetBinding(Label.FormattedTextProperty, "input");
        input.BindingContext = formattedString;
        formattedString.Spans.Add(new Span { Text = "0" });
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
        bool inputHasValue = formattedString.Spans.Count > 0;
        var lastSpan = formattedString.Spans[formattedString.Spans.Count - 1];
        var lastCharacter = lastSpan.Text[lastSpan.Text.Length - 1];

        var btn = (sender as Button);

        string digit = btn.Text;

        if (IsCompletingEq)
        {
            Span targetSpan = formattedString.Spans[indexOfX];

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

        if (inputHasValue && formattedString.Spans.First().Text != "0")
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

        var lastSpanText = formattedString.Spans[formattedString.Spans.Count - 1].Text;
        // bugs out when just coming out of IsCompletingEq
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
        formattedString.RemoveBinding(Label.FormattedTextProperty);

        formattedString = new FormattedString();
        formattedString.Spans.Add(new Span { Text = "0" });

        formattedString.SetBinding(Label.FormattedTextProperty, "input");
        input.BindingContext = formattedString;
    }

    private void OnParenthesisButtonClicked(object sender, EventArgs e)
    {
        char lastChar = char.Parse(formattedString.Spans[formattedString.Spans.Count - 1].Text);

        if (!IsBetweenParentheses && formattedString.Spans[0].Text == "0")
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

            return;
        }

        int index = currentLength - 1;

        formattedString.Spans.RemoveAt(index);

        string currentString = formattedString.Spans[index - 1].Text;
        char lastCharacter = currentString[currentString.Length - 1];

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
    }

    private void OnVariableButtonClicked(object sender, EventArgs e)
    {
        int currentLength = formattedString.Spans.Count;
        char lastChar = char.Parse(formattedString.Spans[currentLength - 1].Text);
        
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
            WriteDigit("X");
        }
        OperatorPressed = false;
    }

    private async void OnAddEquationClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync(
            title: "Save Equation?",
            message: "Equation Name",
            placeholder: "Name",
            keyboard: Keyboard.Default);

        if (result == null) return;

        if (result == "")
        {
            await DisplayAlert("Failed", "Your Equation name needs to have atleast 1 character", "Ok!");
            return;
        }
        // Do saving logic here.
        await DisplayAlert("Success", $"Your Equation {result} was saved successfully", "Hurray!");
        OnResetClicked(sender, e);
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
    }

    private void ResetSpanColors(bool synchronize = false)
    {
        foreach (var span in formattedString.Spans)
        {
            span.BackgroundColor = null;
            span.TextColor = Colors.White;
        }
    }

    private void OnPastEqClicked(object sender, EventArgs e)
    {
        // To be implemented after file saving is complete.
    }

    private void OnListClicked(object sender, EventArgs e)
    {
        // To be implemented last.
    }
}

