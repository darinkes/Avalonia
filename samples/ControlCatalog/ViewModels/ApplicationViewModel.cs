using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform;
using Avalonia.Threading;
using MiniMvvm;

namespace ControlCatalog.ViewModels
{
    public class ApplicationViewModel : ViewModelBase
    {
        private WindowIcon? _trayIcon;
        public WindowIcon? TrayIcon
        {
            get => _trayIcon;
            set => this.RaiseAndSetIfChanged(ref _trayIcon, value);
        }

        private int _animationIndex;

        public ApplicationViewModel()
        {
            ExitCommand = MiniCommand.Create(() =>
            {
                if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
                {
                    lifetime.Shutdown();
                }
            });

            SetTrayIcon("test_icon");

            var animationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(300)
            };
            animationTimer.Tick += AnimationTimerOnTick;
            animationTimer.Start();

            ToggleCommand = MiniCommand.Create(() => { });
        }

        public MiniCommand ExitCommand { get; }

        public MiniCommand ToggleCommand { get; }

        private void SetTrayIcon(string iconName)
        {
            Console.WriteLine($"Setting TrayIcon to {iconName}");
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            TrayIcon = new WindowIcon(assets.Open(new Uri($"avares://ControlCatalog/Assets/{iconName}.ico")));
        }

        private void AnimationTimerOnTick(object? sender, EventArgs e)
        {
            var direction = _animationIndex == 1 ? "left" : "right";
            SetTrayIcon($"arrow-{direction}");
            _animationIndex = (_animationIndex + 1) % 2;
        }
    }
}
