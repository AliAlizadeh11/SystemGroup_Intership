// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Reflection;
// using System.Threading.Tasks;
//
// namespace LoyaltyClub.Plugin
// {
//     public class LoyaltyPointsCalculator
//     {
//         private readonly List<ILoyaltyPointsPlugin> _plugins = new List<ILoyaltyPointsPlugin>();
//
//         public void LoadPlugins(string pluginDirectory)
//         {
//             var pluginFiles = Directory.GetFiles(pluginDirectory, "*.dll");
//             var tasks = pluginFiles.Select(file => Task.Run(() => LoadPlugin(file))).ToArray();
//             Task.WaitAll(tasks);
//         }
//
//         private void LoadPlugin(string filePath)
//         {
//             var assembly = Assembly.LoadFrom(filePath);
//             var pluginTypes = assembly.GetTypes().Where(t => typeof(ILoyaltyPointsPlugin).IsAssignableFrom(t) && !t.IsInterface);
//
//             foreach (var type in pluginTypes)
//             {
//                 if (Activator.CreateInstance(type) is ILoyaltyPointsPlugin plugin)
//                 {
//                     _plugins.Add(plugin);
//                 }
//             }
//         }
//
//         public int CalculateTotalPoints(CustomerPurchaseData purchaseData)
//         {
//             return _plugins.AsParallel().Sum(plugin => plugin.CalculatePoints(purchaseData));
//         }
//     }
// }



////22222222
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LoyaltyClub.Plugin
{
    public class LoyaltyPointsCalculator
    {
        private List<ILoyaltyPointsPlugin> _plugins = new List<ILoyaltyPointsPlugin>();

        public LoyaltyPointsCalculator()
        {
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            var pluginTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(ILoyaltyPointsPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            var tasks = pluginTypes.Select(t => Task.Run(() =>
            {
                var plugin = (ILoyaltyPointsPlugin)Activator.CreateInstance(t);
                lock (_plugins)
                {
                    _plugins.Add(plugin);
                }
            })).ToArray();

            Task.WaitAll(tasks);
        }

        public int CalculateTotalPoints(CustomerPurchaseData purchaseData)
        {
            var tasks = _plugins.Select(p => Task.Run(() => p.CalculatePoints(purchaseData)));
            Task.WaitAll(tasks.ToArray());

            return tasks.Sum(t => t.Result);
        }
    }
}