namespace WpfMediaPlayer.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    public class PlayPauseButton : Button
    {
        public static readonly DependencyProperty IsPlayStateProperty =
            DependencyProperty.Register("IsPlayState", typeof (bool), typeof (PlayPauseButton));

        public static readonly DependencyProperty PausedShapeProperty =
            DependencyProperty.Register("PausedShape", typeof (Shape), typeof (PlayPauseButton));

        public static readonly DependencyProperty PlayShapeProperty =
            DependencyProperty.Register("PlayShape", typeof (Shape), typeof (PlayPauseButton));

        public bool IsPlayState {
            get { return (bool)this.GetValue(IsPlayStateProperty); }
            set { this.SetValue(IsPlayStateProperty, value); }
        }

        public Shape PausedShape {
            get { return (Shape)this.GetValue(PausedShapeProperty); }
            set { this.SetValue(PausedShapeProperty, value); }
        }

        public Shape PlayShape {
            get { return (Shape)this.GetValue(PlayShapeProperty); }
            set { this.SetValue(PlayShapeProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            this.ChangeToPauseState();

            base.OnApplyTemplate();
        }

        public void ChangeToPauseState()
        {
            this.IsPlayState = false;
            this.Content = this.PausedShape;
        }

        public void ChangeToPlayingState()
        {
            this.IsPlayState = true;
            this.Content = this.PlayShape;
        }
    }
}
