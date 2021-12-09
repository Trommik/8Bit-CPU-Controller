using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Xml.Serialization;

namespace CPUController.UI.MVVM
{
    /// <summary>
    /// Abstract base class for a collection which holds AbstractModels.
    /// </summary>
    /// <typeparam name="T"> A AbstractModel as base type. </typeparam>
    [Serializable]
    public class AbstractModelCollection<T> : AbstractModel where T : AbstractModel
    {
        #region Properties

        private ObservableCollection<T> _items;

        /// <summary>
        /// All Items of this UndoableCollection.
        /// </summary>
        [XmlElement]
        public ObservableCollection<T> Items
        {
            get => _items;
            protected set
            {
                if (value == _items)
                    return;

                if (_items != null)
                    _items.CollectionChanged -= CollectionChanged;

                _items = value;

                _items.CollectionChanged += CollectionChanged;
                OnItemsChanged();
            }
        }

        /// <summary>
        /// Gets called when one or multiple items gets added or removed from the Items collection.
        /// Validates all new models automatically and adds all changes to the MUF Undo/Redo framework.
        /// </summary>
        protected virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine($"Items {e.Action}.", ToString(), "COLLECTION CHANGE");

            if (e.NewItems != null)
                Debug.WriteLine($"{e.NewItems.Count} items added.", ToString(), "COLLECTION CHANGE");

            if (e.OldItems != null)
                Debug.WriteLine($"{e.OldItems.Count} items removed.", ToString(), "COLLECTION CHANGE");
        }

        /// <summary>
        /// Gets called when the Items collection changes.
        /// </summary>
        protected virtual void OnItemsChanged()
        {
            Debug.WriteLine($"All Items changed, new item count {Items.Count}.", ToString(), "COLLECTION CHANGE");
        }

        #endregion Properties

        /// <summary>
        /// Initializes a new empty <see cref="AbstractModelCollection{T}" />.
        /// </summary>
        public AbstractModelCollection()
        {
            Items = new ObservableCollection<T>();
        }

        /// <summary>
        /// Initializes a new <see cref="AbstractModelCollection{T}" /> which contains all given items.
        /// </summary>
        /// <param name="items">
        /// A Enumerable with all items which the <see cref="AbstractModelCollection{T}" /> should contain.
        /// </param>
        public AbstractModelCollection(IEnumerable<T> items)
        {
            Items = new ObservableCollection<T>(items);
        }

        #region Methods

        /// <summary>
        /// Adds each item from the items enumerable to this collection.
        /// </summary>
        /// <param name="items"> The items to add. </param>
        public virtual void AddRange(IEnumerable<T> items)
        {
            if (items == null)
                return;

            foreach (var item in items)
                Items.Add(item);
        }

        #endregion Methods
    }
}