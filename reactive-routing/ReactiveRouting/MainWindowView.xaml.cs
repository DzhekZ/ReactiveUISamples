﻿using System.Windows;
using ReactiveUI;

namespace ReactiveRouting
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : IViewFor<MainWindowViewModel>
    {
        public MainWindowView()
        {
            InitializeComponent();

            this.Bind(ViewModel, vm => vm.Router, v => v.ViewHost.Router);

            this.BindCommand(ViewModel, vm => vm.NavigateToACommand, v => v.NavigateToA);
            this.BindCommand(ViewModel, vm => vm.NavigateToBCommand, v => v.NavigateToB);
            this.BindCommand(ViewModel, vm => vm.BackCommand, v => v.BackButton);
        }

        static MainWindowView()
        {
            ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(MainWindowViewModel), typeof(MainWindowView));
        }

        public static readonly DependencyProperty ViewModelProperty;

        public MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainWindowViewModel)value; }
        }
    }
}
