﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using TcNo_Acc_Switcher_Globals;

namespace TcNo_Acc_Switcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Crash handler
            AppDomain.CurrentDomain.UnhandledException += Globals.CurrentDomain_UnhandledException;

            Directory.SetCurrentDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)); // Set working directory to the same as the actual .exe
            var mainWindow = new MainWindow {Topmost = true};

            for (var i = 0; i != e.Args.Length; ++i)
            {
                Console.WriteLine(e.Args[i]);
                if (e.Args[i]?[0] == '+')
                {
                    var arg = e.Args[i].Substring(1); // Get command

                    switch (arg)
                    {
                        case "steam":
                            Process.Start("Steam\\TcNo Account Switcher Steam.exe");
                            mainWindow.Process_OptionalUpdate(true);
                            Environment.Exit(1);
                            break;
                        //case "origin":
                    }
                }

                if (e.Args[i]?[0] == '-')
                {
                    var arg = e.Args[i].Substring(1); // Get command
                    switch (arg)
                    {
                        case "update":
                        case "updatecheck":
                            mainWindow.Process_Update(true);
                            Environment.Exit(1);
                            break;
                        //case "update_steam":
                    }
                }
            }

            //Bypass this platform picker launcher for now.
            //This keeps shortcuts working, and can be replaced with an update.
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = Path.GetFullPath("Steam\\TcNo Account Switcher Steam.exe"),
                    WorkingDirectory =
                        Path.GetFullPath(Path.GetDirectoryName("Steam\\TcNo Account Switcher Steam.exe")),
                    CreateNoWindow = false,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception win32Exception2)
            {
                if (win32Exception2.HResult != -2147467259) throw; // Throw is error is not: cancelled by user
            }
            //Process.Start("Steam\\TcNo Account Switcher Steam.exe");

            mainWindow.Process_OptionalUpdate();
            Environment.Exit(1);

            //mainWindow.Show();
        }
    }
}
