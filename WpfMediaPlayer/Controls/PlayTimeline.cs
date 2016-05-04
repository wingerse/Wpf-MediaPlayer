namespace WpfMediaPlayer.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class PlayTimeline : Control
    {
        public static readonly DependencyProperty CurrentTimeSpanProperty =
            DependencyProperty.Register("CurrentTimeSpan", typeof (TimeSpan), typeof (PlayTimeline), new PropertyMetadata(TimeSpan.Zero));

        public static readonly DependencyProperty EndTimeSpanProperty =
            DependencyProperty.Register("EndTimeSpan", typeof (TimeSpan), typeof (PlayTimeline), new PropertyMetadata(TimeSpan.Zero));

        static PlayTimeline()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (PlayTimeline), new FrameworkPropertyMetadata(typeof (PlayTimeline)));
        }

        public TimeSpan CurrentTimeSpan {
            get { return (TimeSpan)this.GetValue(CurrentTimeSpanProperty); }
            set { this.SetValue(CurrentTimeSpanProperty, value); }
        }

        public TimeSpan EndTimeSpan {
            get { return (TimeSpan)this.GetValue(EndTimeSpanProperty); }
            set { this.SetValue(EndTimeSpanProperty, value); }
        }
    }
}
