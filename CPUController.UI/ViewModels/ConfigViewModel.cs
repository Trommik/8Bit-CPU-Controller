﻿using System.IO;
using System.Text.Json;
using System.Windows.Input;

using CPUController.Core;
using CPUController.Services;
using CPUController.UI.MVVM;

using JetBrains.Annotations;

using PropertyChanged;

namespace CPUController.UI.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {
        private const string DefaultConfigPath = "config.json";

        private readonly ICpuControllerService _cpuControllerService;

        #region Properties

        /// <summary>
        /// The current loaded config. 
        /// </summary>
        public Config Config { get; set; }

        /// <summary>
        /// The endpoint of the cpus rest api. 
        /// </summary>
        [DependsOn(nameof(Config))]
        public string Endpoint
        {
            get => Config.Endpoint;
            set => Config.Endpoint = value;
        }

        [UsedImplicitly]
        private async void OnEndpointChanged()
        {
            await _cpuControllerService.Initialize(Config.Endpoint, Config.RefreshRate);
        }

        /// <summary>
        /// The refresh rate in milliseconds.
        /// </summary>
        [DependsOn(nameof(Config))]
        public int RefreshRate
        {
            get => Config.RefreshRate;
            set => Config.RefreshRate = value;
        }

        [UsedImplicitly]
        private async void OnRefreshRateChanged()
        {
            await _cpuControllerService.Initialize(Config.Endpoint, Config.RefreshRate);
        }

        #endregion

        /// <summary>
        /// Initializes a new <see cref="ConfigViewModel"/> and loads the <see cref="Config"/> from the <see cref="DefaultConfigPath"/>.
        /// </summary>
        public ConfigViewModel(ICpuControllerService cpuControllerService)
        {
            _cpuControllerService = cpuControllerService;

            Load();
        }

        #region Commands

        /// <summary>
        /// Command to save the <see cref="Config"/> to the <see cref="DefaultConfigPath"/>.
        /// </summary>
        public ICommand SaveCommand => new RelayCommand(Save);

        private void Save()
        {
            File.WriteAllText(DefaultConfigPath, JsonSerializer.Serialize(Config));
        }

        /// <summary>
        /// Command to load the <see cref="Config"/> from the <see cref="DefaultConfigPath"/>.
        /// </summary>
        public ICommand LoadCommand => new RelayCommand(Load);

        private void Load()
        {
            Config = JsonSerializer.Deserialize<Config>(File.ReadAllText(DefaultConfigPath));
        }

        #endregion
    }
}