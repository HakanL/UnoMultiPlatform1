using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Uno.Extensions.Navigation;
using Microsoft.Extensions.Logging;

namespace App3
{

	// Need an attribute to identify this as the static class where
	// the ViewModelMappings should be source generated
	// [ReactiveMappings]
	public static partial class ReactiveViewModelMappings
	{
	}

	// ********* Generated ********* //
	public static partial class ReactiveViewModelMappings
	{
		public static IDictionary<Type, Type> ViewModelMappings = new Dictionary<Type, Type>()
			{
				//{ typeof(HomeViewModel), typeof(HomeViewModel.BindableHomeViewModel)},
				//{ typeof(SearchViewModel),typeof(SearchViewModel.BindableSearchViewModel)},
				//{ typeof(SettingsViewModel),typeof(SettingsViewModel.BindableSettingsViewModel)},
				//{ typeof(WelcomeViewModel),typeof(WelcomeViewModel.BindableWelcomeViewModel)},
				//{ typeof(TaskListViewModel),typeof(TaskListViewModel.BindableTaskListViewModel )},
				//{ typeof(TaskViewModel),typeof(TaskViewModel.BindableTaskViewModel)},
				//{ typeof(ExpirationDateViewModel),typeof(ExpirationDateViewModel.BindableExpirationDateViewModel)}
			};

	}
	// ***************************** //



	// ********* Classes to be added to Reactive.Navigation ********* //
	public class ReactiveViewRegistry : ViewRegistry
	{
		public IDictionary<Type, Type> ViewModelMappings { get; }
		public ReactiveViewRegistry(IServiceCollection services, IDictionary<Type, Type> viewModelMappings) : base(services)
		{
			ViewModelMappings = viewModelMappings;
		}

		protected override void InsertItem(ViewMap item)
		{
			if (item.ViewModel != null &&
				ViewModelMappings.TryGetValue(item.ViewModel, out var bindableViewModel))
			{
				throw new NotImplementedException();
				//FIXMe item = new ReactiveViewMap(item.View, item.ViewSelector, item.ViewModel, item.Data, item.ResultData, bindableViewModel);
			}

			base.InsertItem(item);
		}
	}

	public class ReactiveRouteResolver : RouteResolver
	{
		private readonly IDictionary<Type, Type> _viewModelMappings;
		public ReactiveRouteResolver(
			ILogger<ReactiveRouteResolver> logger,
			IRouteRegistry routes,
			ReactiveViewRegistry views) : base(logger, routes, views)
		{
			_viewModelMappings = views.ViewModelMappings;
		}

		//protected override RouteInfo FromRouteMap(RouteMap drm)
		//{
		//	var viewFunc = (drm.View?.View != null) ?
		//									() => drm.View.View :
		//									drm.View?.ViewSelector;
		//	var result = base.FromRouteMap(drm);
		//	result.ViewModel = (drm.View is ReactiveViewMap rvmp) ? rvmp.BindableViewModel : drm.View?.ViewModel;

		//	return result;
		//}

		public override RouteInfo FindByViewModel(Type viewModelType)
		{
			if (viewModelType != null &&
				_viewModelMappings.TryGetValue(viewModelType, out var bindableViewModel))
			{
				return base.FindByViewModel(bindableViewModel);
			}
			return base.FindByViewModel(viewModelType);
		}
	}

	// FIXME
	//public ViewMap ReactiveViewMap(
	//		Type? View = null,
	//		Func<Type?>? ViewSelector = null,
	//		Type? ViewModel = null,
	//		DataMap? Data = null,
	//		Type? ResultData = null,
	//		Type? BindableViewModel = null
	//	) : ViewMap(View, ViewSelector, ViewModel, Data, ResultData)
	//{
	//	public override void RegisterTypes(IServiceCollection services)
	//	{
	//		if (BindableViewModel != null)
	//		{
	//			services.AddTransient(BindableViewModel);
	//		}

	//		base.RegisterTypes(services);
	//	}
	//}
}
