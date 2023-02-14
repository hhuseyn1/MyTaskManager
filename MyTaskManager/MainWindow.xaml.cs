using Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace MyTaskManager;

public partial class MainWindow : Window
{
   public ObservableCollection<Process> Processes { get; set; }
   public List<string>? BlackList { get; set; }

    public ICommand BLCommand { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        DataContext= this;
        Processes = new(Process.GetProcesses());
        BLCommand = new RelayCommand(BLcommand);
        Timer timer = new System.Timers.Timer();
        timer.Interval = 3000;

        timer.Elapsed += TimerCloseBLProcesses;
        timer.Elapsed += TimerLoadProcess;
        timer.Start();
    }

    private void TimerCloseBLProcesses(object? sender, ElapsedEventArgs e)
    {
        BlackList.Clear();
        Dispatcher.Invoke(() =>
        {
            foreach (var process in Processes)
            {
                if (BlackList.Any(p => p == process.ProcessName))
                {
                    try
                    {
                        process.Kill();
                        Processes.Remove(process);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        );
    }

    private void TimerLoadProcess(object? sender, ElapsedEventArgs e)
    {
        Dispatcher.Invoke(() =>
        {
            Processes.Clear();
            foreach (var process in Process.GetProcesses())
            {
                Processes.Add(process);
            }
        });
    }

    private void BLcommand(object? obj)
    {
        if (Tasks.SelectedItem is null)
            return;

        if (Tasks.SelectedItem is Process process)
        {
            if (!BlackList.Contains(process.ProcessName))
                BlackList.Add(process.ProcessName);
        }
    }

    private void Btn_AddBlist(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(BlackList_txtbox.Text) || string.IsNullOrEmpty(BlackList_txtbox.Text))
            return;
        BlackList.Add(BlackList_txtbox.Text);
        BlackList_txtbox.Text = string.Empty;
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

    private void EndProcess_DClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
            //prosesin adinin uzerine 2 defe basanda isleyecek
        if (Tasks.SelectedItem is Process process){
            try
            {
                process.Kill();
                Processes.Remove(process);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
