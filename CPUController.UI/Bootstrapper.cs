﻿using CPUController.Services;
using CPUController.UI.ViewModels;

using Unity;

namespace CPUController.UI
{
    public class Bootstrapper
    {
        #region Singelton Instance

        private static Bootstrapper _instance;

        /// <summary>
        /// Singleton instance of the Bootstrapper. 
        /// </summary>
        public static Bootstrapper Instance => _instance ??= new Bootstrapper();

        #endregion

        /// <summary>
        /// The unity container. 
        /// </summary>
        public IUnityContainer Container { get; }

        private Bootstrapper()
        {
            var container = new UnityContainer();

            // Register all services 
            container.RegisterSingleton<ICpuControllerService, CpuControllerService>();

            // Register all view models
            container.RegisterType<MainViewModel>();

            container.RegisterType<ModeViewModel>();
            container.RegisterType<ConfigViewModel>();
            container.RegisterType<ProgramViewModel>();
            container.RegisterType<StatusViewModel>();

            container.RegisterType<ControlWordViewModel>();

            // container.RegisterType<ValueInstructionViewModel>();
            // container.RegisterType<OpCodeInstructionViewModel>();

            Container = container;
        }
    }
}