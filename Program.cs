using DynClient.Plugins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Reflection;

namespace DynClient
{
    class Program
    {
        static IDictionary<string,object> GetConfig(String path)
        {
            var config = new Dictionary<String, Object>();



            return config;

        }
        static public void UpdateDns(IProviderPlugin plugin)
        {
            IAddressRetriever retriever = new HttpAddressRetriever();

            DynClient.UpdateDns(retriever, plugin, null);

        }

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
                var configOptions = GetConfig(config.FullName);
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
