namespace WpfMediaPlayer
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    using Controls;

    internal static class PlayTimelineEx
    {
        public static readonly DependencyProperty MediaElementProperty =
            DependencyProperty.RegisterAttached("MediaElement", typeof (MediaElement), typeof (PlayTimelineEx), new PropertyMetadata(MediaElementChangedCallback));

        private static readonly DependencyProperty IsDueToTimerProperty =
            DependencyProperty.RegisterAttached("IsDueToTimer", typeof (bool), typeof (PlayTimelineEx));

        private static readonly DependencyProperty TimerProperty =
            DependencyProperty.RegisterAttached("Timer", typeof (DispatcherTimer), typeof (PlayTimelineEx));

        public static bool GetIsDueToTimer(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDueToTimerProperty);
        }

        public static MediaElement GetMediaElement(DependencyObject obj)
        {
            return (MediaElement)obj.GetValue(MediaElementProperty);
        }

        private static void SetIsDueToTimer(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDueToTimerProperty, value);
        }

        public static void SetMediaElement(DependencyObject obj, MediaElement value)
        {
            obj.SetValue(MediaElementProperty, value);
        }

        private static DispatcherTimer GetTimer(DependencyObject obj)
        {
            return (DispatcherTimer)obj.GetValue(TimerProperty);
        }

        private static void MediaElementChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var playTimeline = (PlayTimeline)o;
            var mediaElement = (MediaElement)e.NewValue;
            mediaElement.MediaOpened += (oo, ee) => SynchnorizeWithMediaElement(playTimeline, mediaElement);
        }

        private static void SetTimer(DependencyObject obj, DispatcherTimer value)
        {
            obj.SetValue(TimerProperty, value);
        }

        private static void SynchnorizeWithMediaElement(PlayTimeline playTimeline, MediaElement mediaElement)
        {
            playTimeline.EndTimeSpan = mediaElement.NaturalDuration.TimeSpan;

            var timer = new DispatcherTimer(TimeSpan.FromMilliseconds(200), DispatcherPriority.Normal,
                                            (s, ee) =>
                                            {
                                                SetIsDueToTimer(playTimeline, true);
                                                playTimeline.CurrentTimeSpan = mediaElement.Position;
                                                SetIsDueToTimer(playTimeline, false);
                                            }, Dispatcher.CurrentDispatcher);

            DependencyPropertyDescriptor.FromProperty(PlayTimeline.CurrentTimeSpanProperty, typeof (PlayTimeline))
                                        .AddValueChanged(playTimeline, (sender, args) =>
                                                                       {
                                                                           if (GetIsDueToTimer(playTimeline))
                                                                           {
                                                                               return;
                                                                           }

                                                                           mediaElement.Position = playTimeline.CurrentTimeSpan;
                                                                       });
            timer.Start();
            SetTimer(playTimeline, timer);
        }
    }
}
