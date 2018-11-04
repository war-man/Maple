﻿using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Maple
{
    public class ListBox : System.Windows.Controls.ListBox
    {
        public static readonly DependencyProperty SelectedItemsListProperty = DependencyProperty.Register(
            nameof(SelectedEntriesList),
            typeof(IList),
            typeof(ListBox),
            new FrameworkPropertyMetadata(default(IList), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IList SelectedEntriesList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public ListBox()
        {
            SelectionChanged += ListBoxCustom_SelectionChanged;
        }

        private void ListBoxCustom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedEntriesList = SelectedItems;
        }
    }
}
