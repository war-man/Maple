﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Maple.Core
{
    public abstract class ViewModelListBase<T> : ObservableObject where T : INotifyPropertyChanged
    {
        //TODO add change tracking
        protected object _itemsLock;

        public EventHandler SelectionChanged;
        public EventHandler SelectionChanging;

        private bool _isBusy;
        /// <summary>
        /// Indicates if there is an operation running.
        /// Modified by adding <see cref="BusyToken"/> to the <see cref="BusyStack"/> property
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            private set { SetValue(ref _isBusy, value); }
        }

        private BusyStack _busyStack;
        /// <summary>
        /// Provides IDisposable tokens for running async operations
        /// </summary>
        protected BusyStack BusyStack
        {
            get { return _busyStack; }
            private set { SetValue(ref _busyStack, value); }
        }

        private T _selectedItem;
        public virtual T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (EqualityComparer<T>.Default.Equals(_selectedItem, value))
                    return;

                SelectionChanging?.Raise(this);
                _selectedItem = value;
                SelectionChanged?.Raise(this);

                OnPropertyChanged();
            }
        }

        private RangeObservableCollection<T> _items;
        /// <summary>
        /// Contains all the UI relevant Models and notifies about changes in the collection and inside the Models themself
        /// </summary>
        public RangeObservableCollection<T> Items
        {
            get { return _items; }
            private set { SetValue(ref _items, value); }
        }

        private ICollectionView _view;
        /// <summary>
        /// For grouping, sorting and filtering
        /// </summary>
        public ICollectionView View
        {
            get { return _view; }
            protected set { SetValue(ref _view, value); }
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public T this[int index]
        {
            get { return Items[index]; }
        }

        public ICommand RemoveRangeCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        public ViewModelListBase()
        {
            InitializeProperties();
            InitializeCommands();

            BindingOperations.EnableCollectionSynchronization(Items, _itemsLock);
        }

        public ViewModelListBase(IList<T> items) : this()
        {
            Items.AddRange(items);
        }

        public ViewModelListBase(IEnumerable<T> items) : this()
        {
            Items.AddRange(items);
        }

        private void InitializeProperties()
        {
            _itemsLock = new object();

            Items = new RangeObservableCollection<T>();
            Items.CollectionChanged += ItemsCollectionChanged;

            BusyStack = new BusyStack();
            BusyStack.OnChanged = (hasItems) => IsBusy = hasItems;

            View = CollectionViewSource.GetDefaultView(Items);

            // initial Notification, so that UI recognizes the value
            OnPropertyChanged(nameof(Count));
        }

        private void InitializeCommands()
        {
            RemoveCommand = new RelayCommand<T>(Remove, CanRemove);
            RemoveRangeCommand = new RelayCommand<IList>(RemoveRange, CanRemoveRange);
            ClearCommand = new RelayCommand(() => Clear(), CanClear);
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Count));
        }

        public virtual void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            using (BusyStack.GetToken())
                Items.Add(item);
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            using (BusyStack.GetToken())
                Items.AddRange(items);
        }

        protected virtual bool CanAdd()
        {
            return Items != null;
        }

        public virtual void Remove(T item)
        {
            using (BusyStack.GetToken())
                Items.Remove(item);
        }

        public virtual void RemoveRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            using (BusyStack.GetToken())
                Items.RemoveRange(items);
        }

        public virtual void RemoveRange(IList items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            using (BusyStack.GetToken())
                Items.RemoveRange(items);
        }

        protected virtual bool CanRemove(T item)
        {
            return CanClear() && item != null && Items.Contains(item);
        }

        /// <summary>
        /// Checks if any of the submitted items can be removed
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        protected virtual bool CanRemoveRange(IEnumerable<T> items)
        {
            return CanClear() && items != null && items.Any(p => Items.Contains(p));
        }

        protected virtual bool CanRemoveRange(IList items)
        {
            return items == null ? false : CanRemoveRange(items.Cast<T>());
        }

        public virtual void Clear()
        {
            using (BusyStack.GetToken())
                Items.Clear();
        }

        protected virtual bool CanClear()
        {
            return Items?.Any() == true;
        }
    }
}
