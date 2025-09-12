﻿using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace Recycli
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ViewModel vm)
            {
                var listView = (ListView)sender;
                vm.SelectedFiles = listView.SelectedItems.Cast<Model.RecycleFile>().ToList();
            }
        }
    }
}