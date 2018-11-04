using System;
using System.Linq;
using System.Threading.Tasks;

using Maple.Core;
using Maple.Domain;
using Maple.Localization.Properties;

namespace Maple
{
    public sealed class MediaPlayers : BaseDataListViewModel<MediaPlayer, MediaPlayerModel>, IMediaPlayersViewModel
    {
        private readonly Func<IMediaPlayer> _playerFactory;
        private readonly AudioDevices _devices;
        private readonly IDialogViewModel _dialog;
        private readonly Func<IUnitOfWork> _repositoryFactory;
        private readonly IMediaPlayerMapper _mediaPlayerMapper;
        private readonly ILoggingNotifcationService _notificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPlayers"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="playerFactory">The player factory.</param>
        /// <param name="repositoryFactory">The repo.</param>
        /// <param name="devices">The devices.</param>
        /// <param name="dialog">The dialog.</param>
        public MediaPlayers(ViewModelServiceContainer container,
                            IMediaPlayerMapper mediaPlayerMapper,
                            Func<IMediaPlayer> playerFactory,
                            Func<IUnitOfWork> repositoryFactory,
                            AudioDevices devices,
                            IDialogViewModel dialog)
            : base(container)
        {
            _playerFactory = playerFactory ?? throw new ArgumentNullException(nameof(playerFactory), $"{nameof(playerFactory)} {Resources.IsRequired}");
            _devices = devices ?? throw new ArgumentNullException(nameof(devices), $"{nameof(devices)} {Resources.IsRequired}");
            _dialog = dialog ?? throw new ArgumentNullException(nameof(dialog), $"{nameof(dialog)} {Resources.IsRequired}");
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory), $"{nameof(repositoryFactory)} {Resources.IsRequired}");
            _mediaPlayerMapper = mediaPlayerMapper ?? throw new ArgumentNullException(nameof(mediaPlayerMapper), $"{nameof(mediaPlayerMapper)} {Resources.IsRequired}");

            _notificationService = container.NotificationService;
        }

        private async Task SaveInternal()
        {
            _log.Info($"{_translationService.Translate(nameof(Resources.Saving))} {_translationService.Translate(nameof(Resources.MediaPlayers))}");
            using (var context = _repositoryFactory())
            {
                await context.SaveChanges().ConfigureAwait(false);
            }
        }

        public void Add()
        {
            var sequence = _sequenceProvider.Get(Items.Cast<ISequence>().ToList());
            Add(_mediaPlayerMapper.GetNewMediaPlayer(sequence));
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                foreach (var player in Items)
                    player?.Dispose();

                // Free any other managed objects here.
            }

            // Free any unmanaged objects here.
            Disposed = true;
        }

        public override Task Save()
        {
            return SaveInternal();
        }

        public override async Task Load()
        {
            _notificationService.Info($"{_translationService.Translate(nameof(Resources.Loading))} {_translationService.Translate(nameof(Resources.MediaPlayers))}");
            Clear();

            using (var context = _repositoryFactory())
            {
                var main = await context.MediaPlayerRepository.GetMainMediaPlayerAsync().ConfigureAwait(true);
                var viewModel = _mediaPlayerMapper.Get(main);
                Add(viewModel);
                SelectedItem = viewModel;

                var others = await context.MediaPlayerRepository.GetOptionalMediaPlayersAsync().ConfigureAwait(true);
                AddRange(_mediaPlayerMapper.GetMany(others));
            }

            OnLoaded();
        }
    }
}
