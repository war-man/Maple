﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InsireBot
{
    public class IoCWindow : ConfigurableWindow, IIocFrameworkElement
    {
        private IConfigurableWindowSettings _settings;
        private UIColorsViewModel _colorsViewModel;
        public ITranslationManager TranslationManager { get; private set; }

        public IoCWindow() : base()
        {
        }

        public IoCWindow(ITranslationManager container, UIColorsViewModel vm) : base()
        {
            TranslationManager = container;
            _colorsViewModel = vm;
            _colorsViewModel.PrimaryColorChanged += PrimaryColorChanged;
        }

        protected override IConfigurableWindowSettings CreateSettings()
        {
            return _settings = _settings ?? new ShellSettings(this);
        }

        private void PrimaryColorChanged(object sender, UiPrimaryColorEventArgs e)
        {
            var data = string.Empty;
            if (PackIcon.TryGet(PackIconKind.ApplicationIcon, out data))
            {
                var geo = Geometry.Parse(data);
                Icon = SetImage(geo, e.Color);
            }
        }

        private BitmapSource SetImage(Geometry geo, Color color)
        {
            var canvas = new Canvas
            {
                Width = 36,
                Height = 36,
                Background = new SolidColorBrush(Colors.Transparent)
            };

            var path = new System.Windows.Shapes.Path()
            {
                Data = geo,
                Stretch = Stretch.Fill,
                Fill = new SolidColorBrush(color),
                Width = 36,
                Height = 36,
            };

            canvas.Children.Add(path);

            var size = new Size(36, 36);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            var rtb = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(canvas);

            var png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));

            using (var memory = new MemoryStream())
            {
                png.Save(memory);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
