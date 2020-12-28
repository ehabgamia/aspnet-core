using System.Threading.Tasks;

namespace MCV.Portal.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}