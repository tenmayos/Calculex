using Calculex.Core;
using System.Reflection;

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

#if ANDROID
	protected override void OnStart()
    {
		string fileName = "/equations.json";

		bool equationsFileExists = File.Exists(fileName);

		if (equationsFileExists) return;

        var readStatus = Permissions.RequestAsync<Permissions.StorageRead>();
        var writeStatus = Permissions.RequestAsync<Permissions.StorageWrite>();
		var granted = PermissionStatus.Granted;

		if (readStatus.Result == granted && writeStatus.Result == granted)
		{
            File.Create(fileName);
        }
		else
		{
			throw new NotImplementedException();
		}
    }
#endif

    // If the OS is windows, we override the window creation.
#if WINDOWS
    protected override void OnStart()
    {
		string path = ConfigHolder.path;

        if (File.Exists(path)) return;

        File.Create(path);
    }
    protected override Window CreateWindow(IActivationState activationState)
    {
		Window mainAppWindow = base.CreateWindow(activationState);
		mainAppWindow.Width = 500;
		mainAppWindow.Height = 750;
		return mainAppWindow;
    }
#endif
}
