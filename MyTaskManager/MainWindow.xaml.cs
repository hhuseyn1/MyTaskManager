using System;
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
        if (string.IsNullOrWhiteSpace(RunProcess_txtbox.Text) || string.IsNullOrEmpty(RunProcess_txtbox.Text))
            return;

        try
        {
            Process.Start(RunProcess_txtbox.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            RunProcess_txtbox.Text = string.Empty;
        }
    }
}
