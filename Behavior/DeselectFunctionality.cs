using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tasks.Behavior
{
    public class DeselectBehavior
    {
        #region IsActive
        /// <summary>
        /// IsActive Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(DeselectBehavior),
        new PropertyMetadata((bool)false,
        new PropertyChangedCallback(OnIsActiveChanged)));

        /// <summary>
        /// Gets the IsActive property. This dependency property
        /// indicates ....
        /// </summary>
        /// 

        public static bool GetIsActive(DependencyObject d)
        {
            return (bool)d.GetValue(IsActiveProperty);
        }

        /// <summary>
        /// Sets the IsActive property. This dependency property
        /// indicates ....
        /// </summary>
        public static void SetIsActive(DependencyObject d, bool value)
        {
            d.SetValue(IsActiveProperty, value);
        }

        /// <summary>
        /// Handles changes to the IsActive property.
        /// </summary>
        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool oldIsActive = (bool)e.OldValue;
            bool newIsActive = (bool)d.GetValue(IsActiveProperty);

            if(d is ListBox listView)
            {
                if (newIsActive)
                {
                    listView.MouseDown += ListView_MouseDown;
                }
                else
                {
                    listView.MouseDown -= ListView_MouseDown;
                }
            }
        }

        private static void ListView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(sender is ListBox listView)
            {
                HitTestResult r = VisualTreeHelper.HitTest(listView, e.GetPosition(listView));
                if (r.VisualHit.GetType() != typeof(ListBoxItem))
                {
                    listView.UnselectAll();
                }
            }
        }
        #endregion
    }
}
