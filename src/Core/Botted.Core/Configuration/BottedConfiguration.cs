namespace Botted.Core.Configuration
{
	public sealed class BottedConfiguration
	{
		internal BottedConfiguration()
		{ }

		public string ConfigPath { get; set; } = "Configuration";

		public string PluginsPath { get; set; } = "Plugins";

		public string LibsPath { get; set; } = "Libs";
	}
}