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

	// If the OS is windows, we override the window creation.
#if WINDOWS
	protected override Window CreateWindow(IActivationState activationState)
    {
		Window mainAppWindow = base.CreateWindow(activationState);
		mainAppWindow.Width = 500;
		mainAppWindow.Height = 750;
		return mainAppWindow;
    }
#endif
}
