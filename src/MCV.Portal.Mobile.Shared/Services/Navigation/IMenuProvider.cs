using System.Collections.Generic;
using MvvmHelpers;
using MCV.Portal.Models.NavigationMenu;

namespace MCV.Portal.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}