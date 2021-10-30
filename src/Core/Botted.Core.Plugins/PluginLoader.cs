using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Botted.Core.Plugins
{
	public class PluginLoader
	{
		private readonly ILogger<PluginLoader> _logger;
		private readonly DirectoryInfo _pluginsDirectory;

		public PluginLoader(ILogger<PluginLoader> logger, DirectoryInfo pluginsDirectory)
		{
			_logger = logger;
			_pluginsDirectory = pluginsDirectory;
		}

		public IReadOnlyCollection<BottedPlugin> LoadPlugins()
		{
			_logger.LogInformation("Start loading plugins");
			var plugins = new List<BottedPlugin>();

			foreach (var pluginDirectory in _pluginsDirectory.EnumerateDirectories())
			{
				var metadataFile = pluginDirectory.GetFiles("pluginInfo.json", SearchOption.TopDirectoryOnly)
												  .FirstOrDefault();
				if (metadataFile is null)
				{
					_logger.LogWarning("No metadata file for plugin {0}. Skip loading.", pluginDirectory.Name);
					continue;
				}

				var metadata = ReadMetadata(metadataFile);
				if (metadata is null)
				{
					_logger.LogWarning("Metadata for plugin {0} was corrupted. Skip loading.",
									   pluginDirectory.Name);
					continue;
				}

				var plugin = LoadPlugin(pluginDirectory);
				if (plugin is null)
				{
					_logger.LogWarning("Error while loading plugin {0}", metadata.Name);
					continue;
				}

				_logger.LogInformation("Plugin {0} loaded", metadata.Name);
				plugin.Metadata = metadata;
				plugins.Add(plugin);
			}

			_logger.LogInformation("Loaded {0} plugins", plugins.Count);
			return plugins;
		}

		private PluginMetadata? ReadMetadata(FileInfo fileInfo)
		{
			try
			{
				var content = File.ReadAllText(fileInfo.FullName);
				var metadata = JsonSerializer.Deserialize<PluginMetadata>(content);
				return metadata;
			} catch (Exception e)
			{
				_logger.LogError(e, "Error while reading plugin metadata");
			}

			return null;
		}

		private BottedPlugin? LoadPlugin(DirectoryInfo pluginDirectory)
		{
			return pluginDirectory.GetFiles("*.dll", SearchOption.AllDirectories)
								  .Select(dll => Assembly.LoadFrom(dll.FullName))
								  .SelectMany(a => a.GetExportedTypes())
								  .Where(t => t.IsAssignableTo(typeof(BottedPlugin)) && !t.IsAbstract)
								  .Select(Activator.CreateInstance)
								  .Cast<BottedPlugin>()
								  .FirstOrDefault();
		}
	}
}