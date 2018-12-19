using MarsApp.ViewModel;
using Unity;

namespace MarsApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length != 1) return;

            var unityContainer = new UnityContainer();
            if (unityContainer != null)
            {
                var system = unityContainer.Resolve<SystemAppViewModel>();
                if (system != null)
                {
                    system.InitSystem();
                    system.Run(args);
                }
            }
        }
    }
}

