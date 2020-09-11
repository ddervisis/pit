using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using PointInTime.App.Helpers;

namespace PointInTime.App.ViewModels
{
    public class CountdownViewModel : INotifyPropertyChanged
    {
        private DateTime endTime;
        private TimeSpan remainingTime;
        private bool running;
        private readonly DelegateCommand _startCountdownCommand;

        /// <summary>
        /// The end datetime defined in the settings.
        /// </summary>
        public DateTime EndTime
        {
            get => endTime;
            set => UpdateField(ref endTime, value);
        }

        /// <summary>
        /// The remaining time until the countdown finishes.
        /// It will increase when the end time is smaller than the start time.
        /// </summary>
        public TimeSpan RemainingTime
        {
            get => remainingTime;
            private set => UpdateField(ref remainingTime, value);
        }

        public bool Running
        {
            get => running;
            private set => UpdateField(ref running, value);
        }

        public ICommand StartCountdownCommand 
        {
            get => _startCountdownCommand;
        }

        public CountdownViewModel()
        {
            endTime = Properties.Settings.Default.DefinedDate + Properties.Settings.Default.DefinedTime;
            _startCountdownCommand = new DelegateCommand(StartCounter, () => !Running);
        }

        /// <summary>
        /// Method that starts the countdown (or countup).
        /// </summary>
        private async void StartCounter()
        {
            Running = true;
            DateTime startTime = DateTime.Now;
            TimeSpan remainingTime, interval = TimeSpan.FromMilliseconds(100);

            bool reverse = false;
            if (startTime > endTime)
            {
                reverse = true;
                DateTime earlierTime = endTime;
                endTime = startTime;
                startTime = earlierTime;
            }
            remainingTime = endTime - startTime;

            while (remainingTime > TimeSpan.Zero)
            {
                RemainingTime = remainingTime;
                if (RemainingTime < interval)
                {
                    interval = RemainingTime;
                }
                await Task.Delay(interval);
                if (reverse)
                {
                    remainingTime = DateTime.Now - startTime;
                }
                else
                {
                    remainingTime = endTime - DateTime.Now;
                }
            }

            RemainingTime = TimeSpan.Zero;
            Running = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdateField<T>(ref T field, T newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return;
            }

            T oldValue = field;
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
