﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Maple.Core;

namespace Maple
{
    public interface IDialogViewModel
    {
        Task ShowExceptionDialog(Exception exception);
        Task<(bool Result, IList<IFileSystemFile> Files)> ShowFileBrowserDialog(FileSystemBrowserOptions options, CancellationToken token);
        Task<(bool Result, IFileSystemDirectory Directory)> ShowFolderBrowserDialog(FileSystemBrowserOptions options, CancellationToken token);
        Task<(bool Result, ICollection<MediaItem> MediaItems)> ShowMediaItemFolderSelectionDialog(FileSystemBrowserOptions options, CancellationToken token);
        Task<(bool Result, ICollection<MediaItem> MediaItems)> ShowMediaItemSelectionDialog(FileSystemBrowserOptions options, CancellationToken token);
        Task ShowMessageDialog(string message, string title);
        Task ShowProgressDialog();
        Task<List<MediaItem>> ShowUrlParseDialog();
    }
}