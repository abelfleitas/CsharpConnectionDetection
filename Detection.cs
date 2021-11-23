using ConsoleApp.CheckInternet;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ConnectionDetection
{
    public static class Detection
    {
        private static event EventHandler<bool> ConnectionEvent;
        private static bool aviable = false;

        private static void SuscribeDetection()
        {
            var observable = Observable.FromEventPattern<bool>(
                e => ConnectionEvent += e,
                e => ConnectionEvent -= e
            );

            var observer = observable.Subscribe(x => {
                aviable = x.EventArgs;
            });
        }

        public static async Task<bool> Instance(string url)
        {
            SuscribeDetection();
            var checker = new InternetChecker(url);
            bool status = await new ConnectionHandler(checker).Status();
            ConnectionEvent(null, status);
            return await Task.FromResult(aviable);
        }

    }
}
