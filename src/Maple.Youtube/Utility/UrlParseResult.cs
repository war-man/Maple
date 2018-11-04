﻿using System;
using System.Collections.Generic;

using Maple.Domain;
using Maple.Localization.Properties;

namespace Maple.Youtube
{
    public class UrlParseResult
    {
        private readonly ILoggingService _log;

        public int Count => RefreshCount();
        public ParseResultType Type { get; private set; }
        public ICollection<MediaItemModel> MediaItems { get; private set; }
        public ICollection<PlaylistModel> Playlists { get; private set; }

        private UrlParseResult(ILoggingService log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log), $"{nameof(log)} {Resources.IsRequired}");

            Playlists = new List<PlaylistModel>();
            MediaItems = new List<MediaItemModel>();
        }

        public UrlParseResult(ILoggingService log, ParseResultType type)
            : this(log)
        {
            Type = type;
        }

        public UrlParseResult(ILoggingService log, List<PlaylistModel> items)
            : this(log, ParseResultType.Playlists)
        {
            Playlists = items;

            Log();
        }

        public UrlParseResult(ILoggingService log, List<PlaylistModel> items, ParseResultType type)
            : this(log, type)
        {
            Playlists = items;

            Log();
        }

        public UrlParseResult(ILoggingService log, PlaylistModel item)
            : this(log, ParseResultType.Playlists)
        {
            Playlists = new List<PlaylistModel>()
            {
                item
            };

            Log();
        }

        public UrlParseResult(ILoggingService log, PlaylistModel item, ParseResultType type)
            : this(log, type)
        {
            Playlists = new List<PlaylistModel>()
            {
                item
            };

            Log();
        }

        public UrlParseResult(ILoggingService log, MediaItemModel item)
            : this(log, ParseResultType.MediaItems)
        {
            MediaItems = new List<MediaItemModel>()
            {
                item
            };

            Log();
        }

        public UrlParseResult(ILoggingService log, MediaItemModel item, ParseResultType type)
            : this(log, type)
        {
            MediaItems = new List<MediaItemModel>()
            {
                item
            };

            Log();
        }

        public UrlParseResult(ILoggingService log, List<MediaItemModel> items)
            : this(log, ParseResultType.MediaItems)
        {
            MediaItems = items;

            Log();
        }

        public UrlParseResult(ILoggingService log, List<MediaItemModel> items, ParseResultType type)
            : this(log, type)
        {
            MediaItems = items;

            Log();
        }

        private void Log()
        {
            if (Count != 1)
                _log.Info($"API Call for {Type} returned {Count} Entries");  // TODO localization
            else
                _log.Info($"API Call for {Type} returned {Count} Entry");  // TODO localization
        }

        private int RefreshCount()
        {
            switch (Type)
            {
                case ParseResultType.MediaItems:
                    return MediaItems.Count;

                case ParseResultType.Playlists:
                    return Playlists.Count;

                case ParseResultType.None:
                    if (MediaItems.Count > 0)
                        return MediaItems.Count;
                    if (Playlists.Count > 0)
                        return Playlists.Count;
                    return 0;

                default:
                    _log.Warn("DataParsingServiceResult misses an Implementation of DataParsingServiceResultType"); // TODO localization
                    return 0;
            }
        }
    }
}
