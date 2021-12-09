using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

using CPUController.UI.MVVM;

namespace CPUController.UI.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {
        private const string DefaultConfigPath = "config.json";

        private readonly Config _config;

        /// <summary>
        /// The endpoint of the cpus rest api. 
        /// </summary>
        public string Endpoint
        {
            get => _config.Endpoint;
            set => _config.Endpoint = value;
        }

        /// <summary>
        /// The refresh rate in milliseconds.
        /// </summary>
        public int RefreshRate
        {
            get => _config.RefreshRate;
            set => _config.RefreshRate = value;
        }

        public ConfigViewModel()
        {
            _config = JsonSerializer.Deserialize<Config>(File.ReadAllText(DefaultConfigPath));
        }

        public ConfigViewModel(Config config)
        {
            _config = config;
        }
    }
}