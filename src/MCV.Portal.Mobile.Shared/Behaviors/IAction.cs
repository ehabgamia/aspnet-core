using Xamarin.Forms.Internals;

namespace MCV.Portal.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}