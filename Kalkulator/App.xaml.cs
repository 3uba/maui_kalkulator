using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Kalkulator;

public partial class App : Application
{

	public App()
	{
		InitializeComponent();

        MainPage = new AppShell();
	}

}
