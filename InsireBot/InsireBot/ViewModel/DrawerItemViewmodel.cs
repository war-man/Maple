﻿using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using InsireBotCore;

namespace InsireBot
{
    /// <summary>
    /// ViewModel that stores and controls which UserControl(Page/View) whatever is displayed in the mainwindow of this app)
    /// </summary>
    public class DrawerItemViewmodel : DefaultViewModelBase<DrawerItem>
    {
        public ICommand SetDrawerItemCommand { get; private set; }

        private object _selectedPage;
        public object SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
                RaisePropertyChanged(nameof(SelectedPage));
            }
        }

        public DrawerItemViewmodel()
        {
            var content = new[]
            {
                new DrawerItem
                {
                    Content = new MediaPlayerPage(),
                    Name = "Home",
                    Detail = new OptionsPage()
                },

                new DrawerItem
                {
                    Content = new NewMediaItemPage(),
                    Name = "Add a Video",
                    Detail = new NewMediaItemOptionsPage(),
                },

                new DrawerItem
                {
                    Content = new NewPlaylistPage(),
                    Name = "Add a Playlist",
                    Detail = new NewPlaylistOptionsPage(),
                },

                new DrawerItem
                {
                    Content = new NewMediaItemPage(),
                    Name = "Discord",
                    Detail = new OptionsPage(),
                },

                new DrawerItem
                {
                    Content = new NewMediaItemPage(),
                    Name = "Twitch",
                    Detail = new OptionsPage(),
                },

                new DrawerItem
                {
                    Content = new NewMediaItemPage(),
                    Name = "Log",
                    Detail = new OptionsPage(),
                },
            };

            Items.AddRange(content);
            SelectedPage = Items.First().Content;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SetDrawerItemCommand = new RelayCommand<DrawerItem>((page) =>
            {
                if (page != null)
                    SelectedPage = page.Detail;
            });
        }
    }
}