using System.ComponentModel;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

using PropertyChanged;

namespace CPUController.UI.MVVM
{
    /// <summary>
    /// A base implementation of a view model which should be inherited by all view models.
    /// Implements the <see cref="INotifyPropertyChanged" /> interface.
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changed.
        /// </summary>
        [UsedImplicitly]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets called when a property value changed.
        /// </summary>
        /// <param name="propertyName"> The changed property name. </param>
        [NotifyPropertyChangedInvocator]
        [UsedImplicitly]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}