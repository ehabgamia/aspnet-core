using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MCV.Portal.ApiClient;
using MCV.Portal.Core.Threading;
using MCV.Portal.Models.NavigationMenu;
using MCV.Portal.Services.Navigation;
using MCV.Portal.ViewModels.Base;
using Xamarin.Forms.Internals;

namespace MCV.Portal.ViewModels
{
    public class InitialViewModel : XamarinViewModel
    {
        private readonly IMenuProvider _menuProvider;
        private readonly IApplicationContext _appContext;
        private bool _noAuthorizedMenuItem;

        public bool NoAuthorizedMenuItem
        {
            get => _noAuthorizedMenuItem;
            set
            {
                _noAuthorizedMenuItem = value;
                RaisePropertyChanged(() => NoAuthorizedMenuItem);
            }
        }

        public ICommand PageAppearingCommand => AsyncCommand.Create(SetInitialPageAsync);
        
        public InitialViewModel(IMenuProvider menuProvider, IApplicationContext appContext)
        {
            _menuProvider = menuProvider;
            _appContext = appContext;
        }

        private async Task SetInitialPageAsync()
        {
            var firstAuthorizedMenuItem = GetFirstAuthorizedMenuItemOrNull(out var authorizedMenuItems);

            NoAuthorizedMenuItem = firstAuthorizedMenuItem == null;

            if (firstAuthorizedMenuItem != null)
            {
                await NavigationService.SetDetailPageAsync(firstAuthorizedMenuItem.ViewType);
                SetSelectedMenuItem(authorizedMenuItems, firstAuthorizedMenuItem);
            }
        }

        private NavigationMenuItem GetFirstAuthorizedMenuItemOrNull(out ObservableRangeCollection<NavigationMenuItem> authorizedMenuItems)
        {
            authorizedMenuItems = _menuProvider.GetAuthorizedMenuItems(_appContext.Configuration.Auth.GrantedPermissions);
            var firstMenuItem = authorizedMenuItems.FirstOrDefault();
            if (firstMenuItem?.ViewType == null)
            {
                return null;
            }

            return firstMenuItem;
        }

        private static void SetSelectedMenuItem(IEnumerable<NavigationMenuItem> menuItems, NavigationMenuItem selectedMenuItem)
        {
            menuItems.ForEach(m => m.IsSelected = false);
            selectedMenuItem.IsSelected = true;
        }
    }
}
