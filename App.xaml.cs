using Calculex.Core;

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
		if (File.Exists(ConfigHolder.path)) return;

        var readStatus = Permissions.RequestAsync<Permissions.StorageRead>();
        var writeStatus = Permissions.RequestAsync<Permissions.StorageWrite>();

		if (readStatus.Result == PermissionStatus.Granted && writeStatus.Result == PermissionStatus.Granted)
		{
            File.Create(ConfigHolder.path);
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
