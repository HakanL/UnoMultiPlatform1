#pragma warning disable 109 // Remove warning for Window property on iOS

using Uno.Extensions.Hosting;
using Uno.Extensions.Navigation;
using System;
using System.Linq;
using Uno.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Uno.Extensions.Configuration;
using Uno.Extensions.Serialization;
using App3.Services;
//Not compatible with UWP - using Uno.Extensions.Navigation.Toolkit;
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
#endif

namespace App3
{
	public sealed partial class App
	{
		public IServiceProvider Container => _host.Services;

		private readonly IHost _host = BuildAppHost();

		private static IHost BuildAppHost()
		{
			return UnoHost
					.CreateDefaultBuilder()
#if DEBUG
					// Switch to Development environment when running in DEBUG
					.UseEnvironment(Environments.Development)
#endif

					// Add platform specific log providers
					.UseLogging(configure: (context, logBuilder) =>
								// Configure log levels for different categories of logging
								logBuilder
										.SetMinimumLevel(
											context.HostingEnvironment.IsDevelopment() ?
												LogLevel.Information :
												LogLevel.Warning))

					.UseConfiguration(configure: configBuilder =>
						configBuilder
							// Load configuration information from appconfig.json
							.EmbeddedSource<App>()
							.EmbeddedSource<App>("platform")

							// Load OAuth configuration
							//.Section<Auth>()

							// Load Mock configuration
							//.Section<Mock>()

							// Enable app settings
							//.Section<ToDoApp>()
					)

					// Register Json serializers (ISerializer and IStreamSerializer)
					.UseSerialization()

					// Register services for the application
					.ConfigureServices(
						(context, services) =>
						{
							//ConfigureLogging(services);
							//services.UseAuthentication();

							services.AddSingleton<INetworkConnectivityService, NetworkConnectivityService>();
							services.AddTransient<INavigationService, NavigationService>();
							services.AddTransient<IGraphFileService, GraphFileService>();
							services.AddTransient<ICachedGraphFileService, CachedGraphFileService>();


							//var section = context.Configuration.GetSection(nameof(Mock));
							//var useMocks = bool.TryParse(section[nameof(Mock.IsEnabled)], out var isMocked) ? isMocked : false;
#if USE_MOCKS
						// This is required for UI Testing where USE_MOCKS is enabled
						useMocks=true;;
#endif

							services
								.AddScoped<IAppTheme, AppTheme>()
								//.AddEndpoints(context, useMocks: useMocks)
								.AddServices();
						})

					// Enable navigation, including registering views and viewmodels
					//.UseNavigation(
					//		RegisterRoutes,
					//		createViewRegistry: sc => new ReactiveViewRegistry(sc, ReactiveViewModelMappings.ViewModelMappings))
					//	.ConfigureServices(services =>
					//	{
					//		services
					//			.AddSingleton<IRouteResolver, ReactiveRouteResolver>();
					//	})

					// Add navigation support for toolkit controls such as TabBar and NavigationView
					//Not working with UWP .UseToolkitNavigation()

					//// Add localization support
					//.UseLocalization()

					.Build(enableUnoLogging: true);
		}
	}
}
