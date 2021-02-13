using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynClient.Plugins
{
    class PluginLoader
    {
        const String pluginFolder = "Providers";

        private List<String> GetPluginsFileInfo()
        {
            String path = Path.Combine(Directory.GetCurrentDirectory(), pluginFolder);
            if(Directory.Exists(path))
            {
                return Directory.EnumerateFiles(path, "*.dll").ToList();
            }

            return new List<String>();
            
        }

        public List<IProviderPlugin> GetPlugins()
        {
            var paths = GetPluginsFileInfo();
            var plugins = new List<IProviderPlugin>();

            foreach(var path in paths)
            {
                Assembly.LoadFile(path);
            }

            Type pluginType = typeof(IProviderPlugin);

            List<Type> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => pluginType.IsAssignableFrom(t) && t.IsClass)
                .ToList();

            foreach(var type in types)
            {
                plugins.Add(Activator.CreateInstance(type) as IProviderPlugin);
            }

            return plugins;
        }
        
       
    }
}
