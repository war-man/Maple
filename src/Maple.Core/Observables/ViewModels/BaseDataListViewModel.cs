﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Maple.Domain;
using Maple.Localization.Properties;

namespace Maple.Core
{
    /// <summary>
    /// ListViewModel implementation for ObservableObjects related to the DataAccessLayer (DB)
    /// </summary>
    /// <typeparam name="TViewModel">a wrapper class implementing <see cref="BaseViewModel" /></typeparam>
    /// <typeparam name="TModel">a DTO implementing <see cref="BaseObject" /></typeparam>
    /// <seealso cref="Maple.Core.BaseListViewModel{T}" />
    public abstract class BaseDataListViewModel<TViewModel, TModel> : BaseListViewModel<TViewModel>, ILoadableViewModel
        where TViewModel : BaseDataViewModel<TViewModel, TModel>, ISequence
        where TModel : class, IBaseObject
    {
        protected readonly ISequenceService _sequenceProvider;
        protected readonly ILocalizationService _translationService;
        protected readonly ILoggingService _log;

        /// <summary>
        /// Gets the load command.
        /// </summary>
        /// <value>
        /// The load command.
        /// </value>
        public ICommand LoadCommand => AsyncCommand.Create(Load, () => !IsLoaded);

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        /// <value>
        /// The refresh command.
        /// </value>
        public ICommand RefreshCommand => AsyncCommand.Create(Load);

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand => AsyncCommand.Create(Save);

        protected BaseDataListViewModel(ViewModelServiceContainer container)
            : base(container.Messenger)
        {
            _log = container.Log;
            _translationService = container.LocalizationService;
            _sequenceProvider = container.SequenceService;
        }

        public abstract Task Load();

        public abstract Task Save();

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public override void Remove(TViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), $"{nameof(item)} {Resources.IsRequired}");

            using (BusyStack.GetToken())
            {
                while (Items.Contains(item))
                {
                    item.Model.IsDeleted = true;
                    base.Remove(item);
                }
            }
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="items">The items.</param>
        public override void RemoveRange(IEnumerable<TViewModel> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items), $"{nameof(items)} {Resources.IsRequired}");

            using (BusyStack.GetToken())
            {
                items.ForEach(p => p.Model.IsDeleted = true);
                base.RemoveRange(items);
            }
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <exception cref="System.ArgumentNullException">items</exception>
        public override void RemoveRange(IList items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items), $"{nameof(items)} {Resources.IsRequired}");

            using (BusyStack.GetToken())
            {
                foreach (var item in items)
                    Remove(item as TViewModel);
            }
        }

        public override void Add(TViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), $"{nameof(item)} {Resources.IsRequired}");

            item.Sequence = _sequenceProvider.Get(Items.Cast<ISequence>().ToList());
            base.Add(item);
        }

        public override void AddRange(IEnumerable<TViewModel> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items), $"{nameof(items)} {Resources.IsRequired}");

            using (BusyStack.GetToken())
            {
                var added = false;
                var sequence = _sequenceProvider.Get(Items.Cast<ISequence>().ToList());

                foreach (var item in items)
                {
                    item.Sequence = sequence;
                    Add(item);

                    sequence++;
                    added = true;
                }

                if (SelectedItem == null && added)
                    SelectedItem = Items.First();
            }
        }
    }
}
