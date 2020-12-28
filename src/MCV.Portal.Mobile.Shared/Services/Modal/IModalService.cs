using System.Threading.Tasks;
using MCV.Portal.Views;
using Xamarin.Forms;

namespace MCV.Portal.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
