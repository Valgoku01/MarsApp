using MarsApp.ViewModel;
using Unity;

namespace MarsApp
{
    /// <summary>
    /// Simulate rovers moves on Mars
    /// Program made by Valentin BIAUD
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length != 1) return;

            var baseApp = new BaseApp();
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

