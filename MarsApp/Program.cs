using MarsApp.ViewModel;
using Unity;

namespace MarsApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length != 1) return;

            var baseApp = new Base();
            if (baseApp != null)
            {
                if (baseApp.InitBase())
                {
                    var system = baseApp.Container.Resolve<SystemAppViewModel>();
                    if (system != null)
                        system.Run(args, baseApp.Container);
                }

                baseApp.Dispose();
            }
        }
    }
}

