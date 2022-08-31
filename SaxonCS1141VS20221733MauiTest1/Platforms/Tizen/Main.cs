using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace SaxonCS1141VS20221733MauiTest1;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
