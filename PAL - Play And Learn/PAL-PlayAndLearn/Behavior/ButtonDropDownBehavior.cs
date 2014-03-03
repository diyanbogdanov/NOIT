using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace PAL_PlayAndLearn.Behavior
{
    public class DropDownButtonBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(Button.ClickEvent, new RoutedEventHandler(AssociatedObject_Click), true);
        }

        void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button source = sender as Button;
            if ((source != null && source.ContextMenu != null))
            {
                // If there is a drop-down assigned to this button, then position and display it 
                source.ContextMenu.PlacementTarget = source;
                source.ContextMenu.Placement = PlacementMode.Left;
                if (!source.ContextMenu.IsVisible)
                {
                    source.ContextMenu.IsOpen = true;                  
                }
                else
                {
                    source.ContextMenu.IsOpen = false;
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(AssociatedObject_Click));
        }
    }
}
