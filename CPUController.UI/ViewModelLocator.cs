using CPUController.UI.ViewModels;

using Unity;

namespace CPUController.UI
{
    public class ViewModelLocator
    {
        #region ViewModels

        private MainViewModel _mainVm;

        public MainViewModel MainVm => _mainVm ??= Bootstrapper.Instance.Container.Resolve<MainViewModel>();

        private ModeViewModel _modeVm;

        public ModeViewModel ModeVm => _modeVm ??= Bootstrapper.Instance.Container.Resolve<ModeViewModel>();

        private ConfigViewModel _configVm;

        public ConfigViewModel ConfigVm => _configVm ??= Bootstrapper.Instance.Container.Resolve<ConfigViewModel>();

        private ProgramViewModel _programVm;

        public ProgramViewModel ProgramVm => _programVm ??= Bootstrapper.Instance.Container.Resolve<ProgramViewModel>();

        private StatusViewModel _statusVm;

        public StatusViewModel StatusVm => _statusVm ??= Bootstrapper.Instance.Container.Resolve<StatusViewModel>();

        private ControlWordViewModel _controlWordVm;

        public ControlWordViewModel ControlWordVm => _controlWordVm ??= Bootstrapper.Instance.Container.Resolve<ControlWordViewModel>();

        #endregion
    }
}