using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

namespace CPUController.UI.MVVM
{
    /// <summary>
    /// A abstract implementation of the <see cref="INotifyPropertyChanged" /> interface from which all models should inherit.
    /// </summary>
    [Serializable]
    public abstract class AbstractModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation

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

        #endregion INotifyPropertyChanged Implementation
    }
}