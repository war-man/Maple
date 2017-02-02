﻿using Maple.Core;
using System.Collections.Generic;

namespace Maple.Youtube
{
    public class UrlParseResult
    {
        private IBotLog _log;
        public int Count => RefreshCount();
        public ParseResultType Type { get; private set; }
        public IList<MediaItem> MediaItems { get; private set; }
        public IList<Playlist> Playlists { get; private set; }

        private UrlParseResult(IBotLog log)
        {
            _log = log;

            Playlists = new List<Playlist>();
            MediaItems = new List<MediaItem>();
        }

        /// <summary>
        /// essentially no data was returned, when using this constructor
        /// </summary>
        public UrlParseResult(IBotLog log, ParseResultType type = ParseResultType.None) : this(log)
        {
            Type = type;
        }

        public UrlParseResult(IBotLog log, IList<Playlist> items) : this(log, ParseResultType.Playlists)
        {
            Playlists = items;

            Log();
        }

        public UrlParseResult(IBotLog log, IList<Playlist> items, ParseResultType type) : this(log, type)
        {
            Playlists = items;

            Log();
        }

        public UrlParseResult(IBotLog log, Playlist item) : this(log, ParseResultType.Playlists)
        {
            Playlists = new List<Core.Playlist>()
            {
                item
            };

            Log();
        }

        public UrlParseResult(IBotLog log, Playlist item, ParseResultType type) : this(log, type)
        {
            Playlists = new List<Core.Playlist>()
            {
                item
            };

            Log();
        }

        public UrlParseResult(IBotLog log, MediaItem item) : this(log, ParseResultType.MediaItems)
        {
            MediaItems = new List<Core.MediaItem>()
            {
                item
            };

            Log();
        }

        public UrlParseResult(IBotLog log, MediaItem item, ParseResultType type) : this(log, type)
        {
            MediaItems = new List<Core.MediaItem>()
            {
                item
            };

            Log();
        }

        public UrlParseResult(IBotLog log, IList<MediaItem> items) : this(log, ParseResultType.MediaItems)
        {
            MediaItems = items;

            Log();
        }

        public UrlParseResult(IBotLog log, IList<MediaItem> items, ParseResultType type) : this(log, type)
        {
            MediaItems = items;

            Log();
        }

        private void Log()
        {
            if (Count != 1)
                _log.Info($"API Call for {Type} returned {Count} Entries");
            else
                _log.Info($"API Call for {Type} returned {Count} Entry");
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
                    _log.Warn("DataParsingServiceResult misses an Implementation of DataParsingServiceResultType");
                    return 0;
            }
        }
    }
}
