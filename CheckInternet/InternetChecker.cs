using ConnectionDetection.CheckInternet;
using RestSharp;
using System.Threading.Tasks;

namespace ConsoleApp.CheckInternet
{
    public class InternetChecker : IInternetChecker
    {
        private string _url;

        public InternetChecker(string url)
        {
            _url = url;
        }

        public async Task<bool> IsInternetAvailable()
        {
            var client = new RestClient(_url);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false); ;
            }
        }

    }
}
