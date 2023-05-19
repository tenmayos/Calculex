using Microsoft.Maui.Controls;

namespace Calculex;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		// MainPage behaves like the MainWindow rather than a page.
		MainPage = new AppShell();
		MainPage.Title = "Calculex";
	}
}
