using Calculex.Core;

namespace Calculex.Views;

public partial class MainPage : ContentPage
{
    private MathProcessor Mp;
    private List<string> Container;
    private bool OperatorPressed;
    private bool IsBetweenParentheses;
    public MainPage()
	{
		InitializeComponent();

        Mp = new MathProcessor();
        Container = new List<string>();
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

        Container.Add(btn.Text);
        
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

        string lastCharacter = input.Text[input.Text.Length - 1].ToString();
        if (!Mp.IsMathOperator(lastCharacter))
        {
            string op = (sender as Button).ClassId;
            OperatorPressed = true;
            input.Text += op;
            Container.Add(op);
        }
    }
    private void OnResetClicked(object sender, EventArgs e)
    {
        input.Text = "0";
        rslt.Text = "0";
        Container = new List<string>();
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

        if (!IsBetweenParentheses)
        {
            IsBetweenParentheses = true;
            input.Text += "(";
            return;
        }

        if (IsBetweenParentheses)
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
            input.Text = "0";
            rslt.Text = "0";
            Container = new List<string>();
            IsBetweenParentheses = false;
            return;
        }

        input.Text = input.Text.Remove(input.Text.Length - 1);
        Container.RemoveAt(Container.Count - 1);
        rslt.Text = Mp.Compute(input.Text).ToString();
    }
}

