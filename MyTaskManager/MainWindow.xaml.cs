using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace MyTaskManager;

public partial class MainWindow : Window
{
   public ObservableCollection<Process> Processes { get; set; }
   public List<string> BlackList { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        DataContext= this;
        Processes = new(Process.GetProcesses());
    }

    private void Btn_AddBlist(object sender, RoutedEventArgs e)
    {

    }

    private void Btn_RunProcess(object sender, RoutedEventArgs e)
    {

    }
}
