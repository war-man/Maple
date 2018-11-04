﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

using Maple.Domain;

namespace Maple.Core
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="Maple.Core.IRefreshable" />
    public interface ILocalizationService : INotifyPropertyChanged, IRefreshable
    {
        /// <summary>
        /// Gets or sets the current language.
        /// </summary>
        /// <value>
        /// The current language.
        /// </value>
        CultureInfo CurrentLanguage { get; set; }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        IEnumerable<CultureInfo> Languages { get; }

        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Translate(string key);
    }
}
