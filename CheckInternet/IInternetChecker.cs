using System.Threading.Tasks;

namespace ConnectionDetection.CheckInternet
{
    public interface IInternetChecker
    {
        Task<bool> IsInternetAvailable();
    }
}
