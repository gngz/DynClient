using DynClient.Plugins;
using DynClient.Retriever;
using System;
using System.Collections;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Reflection;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DynClient
{
    using ConfigType = IDictionary<String, Object>;
    class Program
    {
        
        static IDictionary<string,object> GetConfig(String path)
        {
            if(File.Exists(path))
            {
                String rawConfig = File.ReadAllText(path);
                return JsonSerializer.Deserialize<Dictionary<String, object>>(rawConfig);
            }
            return default;
        }
        static private void UpdateDns(IProviderPlugin plugin, ConfigType config) => DynClient.UpdateDns(plugin, config);

        static public void Execute(FileInfo config, Boolean verbose)
        {
            PluginLoader loader = new PluginLoader();
            var plugins = loader.GetPlugins();

            if (verbose)
            {
                plugins.ForEach(plugin =>
                {
                    Console.WriteLine($"Loaded {plugin.Name} plugin!");
                });
                Console.WriteLine($"Loaded {plugins.Count} plugins!");
            }

            if(config != null)
            {

                if(!File.Exists(config.FullName)) {
                    Console.WriteLine("The configuration file does not exist!");
                    return;
                }

                var configOptions = GetConfig(config.FullName);

                if(configOptions.ContainsKey("plugin"))
                {
                    string pluginName = configOptions["plugin"].ToString();
                    
                    var plugin = plugins
                        .Where(plugin => plugin.Name.Equals(pluginName))
                        .FirstOrDefault();

                    if(plugin == null)
                    {
                        Console.WriteLine("The specified plugin was not loaded!");
                        return;
                    }

                    UpdateDns(plugin, configOptions);
                } else
                {
                    Console.WriteLine("The configuration file does not specify any plugin!");
                }

                
            }

            Console.WriteLine($"File: {config?.FullName}, Verbose: {verbose}");

            

        

        }
        static int Main(string[] args)
        {
            var configOption = new Option<FileInfo>("--config", "Config File Location");
            configOption.AddAlias("-c");

            var verboseOption = new Option<Boolean>("--verbose", "Verbose");

            var rootCommand = new RootCommand
            {
                configOption,
                verboseOption

            };

            rootCommand.Description = "Yet Another Extensible Dynamic Dns Updater";
            rootCommand.Handler = CommandHandler.Create<FileInfo, Boolean>(Execute);
            return rootCommand.InvokeAsync(args).Result;
            
        }
    }
}
