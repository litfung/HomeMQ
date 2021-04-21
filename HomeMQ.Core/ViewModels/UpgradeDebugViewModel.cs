using BaseClasses;
using BaseViewModels;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public class UpgradeDebugViewModel : BaseViewModel
    {
        #region Fields
        private Process mainProcess;
        private StreamWriter myStreamWriter;
        private StreamReader myStreamReader;
        #endregion

        #region Properties
        private string consoleInput;
        public string ConsoleInput
        {
            get { return consoleInput; }
            set
            {
                if (consoleInput != value)
                {
                    consoleInput = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<string> consoleOutput = new ObservableCollection<string>();
        public ObservableCollection<string> ConsoleOutput
        {
            get { return consoleOutput; }
            set
            {
                if (consoleOutput != value)
                {
                    consoleOutput = value;
                    RaisePropertyChanged();
                }
            }
        }


        #endregion

        #region Commands
        public IMvxCommand EnterKeyCommand { get; set; }
        public IMvxCommand StartCmdConsoleCommand { get; set; }
        public IMvxCommand StopCmdConsoleCommand { get; set; }
        #endregion

        #region Constructors
        public UpgradeDebugViewModel(IBackgroundHandler backgroundHandler) : base(backgroundHandler)
        {
            EnterKeyCommand = new MvxCommand(OnSendCmdMessage, CanSendCmdMessage);
            StartCmdConsoleCommand = new MvxCommand(OnStartCmdConsole);
            StopCmdConsoleCommand = new MvxCommand(OnStopCmdConsole);
        }
        #endregion

        #region Methods
        private void OnStartCmdConsole()
        {
            mainProcess = new Process();
            mainProcess.StartInfo.FileName = "py";
            mainProcess.StartInfo.Arguments = @"C:\Users\devin\Workspaces\pi_scripts\hello_world_shell.py";
            mainProcess.StartInfo.UseShellExecute = false;
            mainProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            mainProcess.StartInfo.CreateNoWindow = true;
            mainProcess.StartInfo.RedirectStandardInput = true;
            mainProcess.StartInfo.RedirectStandardOutput = true;

            mainProcess.Start();

            myStreamWriter = mainProcess.StandardInput;
            myStreamReader = mainProcess.StandardOutput;

            var _ = UpdateStandardOutput();

            _backgroundHandler.SendMessage(new UpdateViewMessage());

            // Prompt the user for input text lines to sort.
            // Write each line to the StandardInput stream of
            // the sort command.
            //String inputText;
            //int numLines = 0;
            ////while(!cancelKeyPressed)
            //do
            //{
            //    //Console.WriteLine("Enter a line of text (or press the Enter key to stop):");

            //    inputText = Console.ReadLine();
            //    if (inputText.Length > 0)
            //    {
            //        numLines++;
            //        myStreamWriter.WriteLine(inputText);
            //    }
            //} while (!inputText.Trim().Equals("bye"));//while (inputText.Length > 0);

            // Write a report header to the console.
            //if (numLines > 0)
            //{
            //    Console.WriteLine($" {numLines} sorted text line(s) ");
            //    Console.WriteLine("------------------------");
            //}
            //else
            //{
            //    Console.WriteLine(" No input was sorted");
            //}

            //// End the input stream to the sort command.
            //// When the stream closes, the sort command
            //// writes the sorted text lines to the
            //// console.
            //myStreamWriter.Close();

            // Wait for the sort process to write the sorted text lines.
            //myProcess.WaitForExit();

        }

        private async void OnSendCmdMessage()
        {
            myStreamWriter.WriteLine($"{ConsoleInput}");
            //ConsoleOutput.Add($"{ConsoleInput}");
            //ConsoleInput = string.Empty;
            //string line;
            //while ((line = await myStreamReader.ReadLineAsync()) != null)
            //{
            //    ConsoleOutput.Add(line);
            //}


        }

        private bool CanSendCmdMessage()
        {
            return myStreamWriter != null;
        }

        private async Task UpdateStandardOutput()
        {
            var line = await myStreamReader.ReadLineAsync();
            var checkLine = "(Cmd)";
            if (line.Contains(checkLine))
            {
                line = line.Replace(checkLine, $"{checkLine} {ConsoleInput.Trim()}\n");
                ConsoleInput = string.Empty;
            }
            if (line != null)
            {
                ConsoleOutput.Add(line);
            }

            await UpdateStandardOutput();
        }

        private void OnStopCmdConsole()
        {
            mainProcess.Close();
        }
        #endregion

    }
}
