﻿using Maple.Domain;

namespace Maple.Core
{
    public abstract class ValidableBaseDataListViewModel<TViewModel, TModel, TKeyDataType> : BaseDataListViewModel<TViewModel, TModel, TKeyDataType>
        where TViewModel : VirtualizationViewModel<TViewModel, TModel, TKeyDataType>, ISequence
        where TModel : class, IBaseModel<TKeyDataType>
    {
        protected ValidableBaseDataListViewModel(ViewModelServiceContainer container, IMapleRepository<TModel, TKeyDataType> repository)
            : base(container, repository)
        {
        }

        // TODO add logic for handling INotifyDataErrorInfo for children and on this
    }
}
