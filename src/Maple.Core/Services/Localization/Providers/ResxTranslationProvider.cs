﻿using System.Collections.Generic;
using System.Globalization;
using System.Resources;

using Maple.Domain;

namespace Maple.Core
{
    public class ResxTranslationProvider : ITranslationProvider
    {
        private readonly ResourceManager _resourceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResxTranslationProvider"/> class.
        /// </summary>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="assembly">The assembly.</param>
        public ResxTranslationProvider()
        {
            _resourceManager = new ResourceManager(typeof(Localization.Properties.Resources));
        }

        /// <summary>
        /// See <see cref="ITranslationProvider.Translate" />
        /// </summary>
        public string Translate(string key)
        {
            return _resourceManager.GetString(key);
        }

        /// <summary>
        /// See <see cref="ITranslationProvider.AvailableLanguages" />
        /// </summary>
        /// <value>
        /// The available languages.
        /// </value>
        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                yield return new CultureInfo("de");
                yield return new CultureInfo("en");
            }
        }
    }
}
