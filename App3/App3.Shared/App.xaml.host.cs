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
	public sealed partial class App : Application
	{
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

							//var section = context.Configuration.GetSection(nameof(Mock));
							//var useMocks = bool.TryParse(section[nameof(Mock.IsEnabled)], out var isMocked) ? isMocked : false;
#if USE_MOCKS
						// This is required for UI Testing where USE_MOCKS is enabled
						useMocks=true;;
#endif

							services
								.AddScoped<IAppTheme, AppTheme>();
								//.AddEndpoints(context, useMocks: useMocks)
								//.AddServices(useMocks: useMocks);
						})

					// Enable navigation, including registering views and viewmodels
					.UseNavigation(
							RegisterRoutes,
							createViewRegistry: sc => new ReactiveViewRegistry(sc, ReactiveViewModelMappings.ViewModelMappings))
						.ConfigureServices(services =>
						{
							services
								.AddSingleton<IRouteResolver, ReactiveRouteResolver>();
						})

					// Add navigation support for toolkit controls such as TabBar and NavigationView
					//Not working with UWP .UseToolkitNavigation()

					//// Add localization support
					//.UseLocalization()

					.Build(enableUnoLogging: true);
		}

		private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
		{
			//LocalizableMessageDialogViewMap BuildDialogViewMap(string section, bool delayUserInput, int defaultButtonIndex, params (object Id, string labelKeyPath)[] buttons)
			//{
			//	return new LocalizableMessageDialogViewMap
			//	(
			//		Content: localizer => localizer[ResourceKey(ResourceKeys.DialogContent)],
			//		Title: localizer => localizer[ResourceKey(ResourceKeys.DialogTitle)],
			//		DelayUserInput: delayUserInput,
			//		DefaultButtonIndex: defaultButtonIndex,
			//		Buttons: buttons
			//			.Select(x => new LocalizableDialogAction(LabelProvider: localizer => localizer[ResourceKey(x.labelKeyPath)], Id: x.Id))
			//			.ToArray()
			//	);
			//	string ResourceKey(string keyPath)
			//	{
			//		// map absolute/relative path accordingly
			//		return keyPath.StartsWith("./") ? keyPath.Substring(2) : $"Dialog_{section}_{keyPath}";
			//	}
			//}

			//var deleteButton = (DialogResults.Affirmative, ResourceKeys.DeleteButton);
			//var cancelButton = (DialogResults.Negative, ResourceKeys.CancelButton);
			//var confirmDeleteListDialog = BuildDialogViewMap(Dialog.ConfirmDeleteList, true, 0, deleteButton, cancelButton);
			//var confirmDeleteTaskDialog = BuildDialogViewMap(Dialog.ConfirmDeleteTask, true, 0, deleteButton, cancelButton);
			//var confirmSignOutDialog = BuildDialogViewMap(Dialog.ConfirmSignOut, true, 0, (DialogResults.Affirmative, ResourceKeys.SignOutButton), cancelButton);

			//views.Register(
			//	// Dialogs and Flyouts
			//	new ViewMap<AddTaskFlyout, AddTaskViewModel>(),
			//	new ViewMap<AddListFlyout, AddListViewModel>(),
			//	new ViewMap<ExpirationDateFlyout, ExpirationDateViewModel>(Data: new DataMap<PickedDate>()),
			//	new ViewMap<RenameListFlyout, RenameListViewModel>(),

			//	// Views
			//	new ViewMap<HomePage, HomeViewModel>(),
			//	new ViewMap<TaskSearchFlyout>(),
			//	new ViewMap<SearchPage, SearchViewModel>(),
			//	new ViewMap<SettingsFlyout, SettingsViewModel>(),
			//	new ViewMap<ShellControl, ShellViewModel>(),
			//	new ViewMap<WelcomePage, WelcomeViewModel>(),
			//	new ViewMap<TaskListPage, TaskListViewModel>(Data: new DataMap<TaskList>()),
			//	new ViewMap(
			//		ViewSelector: () => (App.Current as App)?.Window?.Content?.ActualSize.X > (double)App.Current.Resources[ResourceKeys.WideMinWindowWidth] ? typeof(TaskControl) : typeof(TaskPage),
			//		ViewModel: typeof(TaskViewModel), Data: new DataMap<ToDoTask>()),
			//	confirmDeleteListDialog,
			//	confirmDeleteTaskDialog,
			//	confirmSignOutDialog
			//);

			routes.Register(
				//new RouteMap("", View: views.FindByViewModel<ShellViewModel>(), Nested: new RouteMap[]
				//{
				//new("Welcome", View: views.FindByViewModel<WelcomeViewModel>()),
				//new("Home", View: views.FindByViewModel<HomeViewModel>()),
				//new("TaskList", View: views.FindByViewModel<TaskListViewModel>(), Nested: new[]
				//{
				//	new RouteMap("MultiTaskLists", IsDefault: true, Nested: new[]
				//	{
				//		new RouteMap("ToDo", IsDefault:true),
				//		new RouteMap("Completed")
				//	})
				//}),
				//new("Task", View: views.FindByViewModel<TaskViewModel>(), DependsOn:"TaskList"),
				//new("TaskSearch", View: views.FindByView<TaskSearchFlyout>(), Nested: new RouteMap[]
				//{
				//	new("Search", View: views.FindByViewModel<SearchViewModel>(), IsDefault: true)
				//}),
				//new("Settings", View: views.FindByViewModel<SettingsViewModel>()),
				//new("AddTask", View: views.FindByViewModel<AddTaskViewModel>()),
				//new("AddList", View: views.FindByViewModel<AddListViewModel>()),
				//new("ExpirationDate", View: views.FindByViewModel<ExpirationDateViewModel>()),
				//new("RenameList", View: views.FindByViewModel<RenameListViewModel>()),
				//new(Dialog.ConfirmDeleteList, confirmDeleteListDialog),
				//new(Dialog.ConfirmDeleteTask, confirmDeleteTaskDialog),
				//new(Dialog.ConfirmSignOut, confirmSignOutDialog)
				//})
			);
		}
	}
}
