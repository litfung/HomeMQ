using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PythonCmdTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ready to sort one or more text lines...");
            var cancelKeyPressed = false;
            // Start the Sort.exe process with redirected input.
            // Use the sort command to sort the input text.
            using (Process myProcess = new Process())
            {

                myProcess.StartInfo.FileName = "py";
                myProcess.StartInfo.Arguments = @"C:\Users\devin\Workspaces\pi_scripts\hello_world_shell.py";
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.RedirectStandardInput = true;

                myProcess.Start();

                StreamWriter myStreamWriter = myProcess.StandardInput;

                // Prompt the user for input text lines to sort.
                // Write each line to the StandardInput stream of
                // the sort command.
                String inputText;
                int numLines = 0;
                //while(!cancelKeyPressed)
                do
                {
                    //Console.WriteLine("Enter a line of text (or press the Enter key to stop):");

                    inputText = Console.ReadLine();
                    if (inputText.Length > 0)
                    {
                        numLines++;
                        myStreamWriter.WriteLine(inputText);
                    }
                } while (!inputText.Trim().Equals("bye"));//while (inputText.Length > 0);

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
                myProcess.WaitForExit();
            }

        }

        private static void PyProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private static void Test2()
        {
            Process pyProcess = new Process();
            pyProcess.StartInfo.FileName = "py";
            pyProcess.StartInfo.Arguments = @"C:\Users\devin\Workspaces\pi_scripts\turtle_shell.py";
            pyProcess.StartInfo.UseShellExecute = false;

            pyProcess.StartInfo.RedirectStandardOutput = true;

            pyProcess.OutputDataReceived += PyProcess_OutputDataReceived;

            pyProcess.StartInfo.RedirectStandardInput = true;
            pyProcess.Start();

            var sw = pyProcess.StandardInput;
            pyProcess.BeginOutputReadLine();
        }

        private static void Test1()
        {
            ProcessStartInfo start = new ProcessStartInfo(); ;
            start.FileName = "py";
            start.Arguments = @"C:\Users\devin\Workspaces\pi_scripts\turtle_shell.py";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using var process = Process.Start(start);
            process.OutputDataReceived += Process_OutputDataReceived;
            process.BeginOutputReadLine();
            //start.out

            while (!process.StandardOutput.EndOfStream)
            {

                process.Refresh();
                string line = process.StandardOutput.ReadLine();
                // do something with line
                Console.WriteLine(line);
                process.Refresh();



                Thread.Sleep(2000);

                process.Refresh();
            }
        }


        //Kinda works
        //    ProcessStartInfo start = new ProcessStartInfo(); ;
        //        start.FileName = "py";
        //        start.Arguments = @"C:\Users\devin\Workspaces\pi_scripts\turtle_shell.py";
        //        start.UseShellExecute = false;
        //        start.RedirectStandardOutput = true;
        //        using var process = Process.Start(start);

        //        while (!process.StandardOutput.EndOfStream)
        //        {
        //            string line = process.StandardOutput.ReadLine();
        //    // do something with line
        //    Console.WriteLine(line);
        //            process.Refresh();



        //            Thread.Sleep(2000);
        //        }

        ////using var reader = process.StandardOutput;

        ////var result = reader.ReadToEnd();
        ////Console.Write(result);
        ///

        //Process p = new Process();
        //p.StartInfo.RedirectStandardError = true;
        //    p.StartInfo.RedirectStandardOutput = true;
        //    p.StartInfo.RedirectStandardInput = true;
        //    p.StartInfo.UseShellExecute = false;
        //    p.StartInfo.CreateNoWindow = true;
        //    p.StartInfo.FileName = @"py";
        //    p.StartInfo.Arguments = @"C:\Users\devin\Workspaces\pi_scripts\turtle_shell.py";

        //    p.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
        //    {
        //    Console.WriteLine(e.Data);
        //});
        //    p.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
        //    {
        //    Console.WriteLine(e.Data);
        //});



        //    p.Start();
        //    StreamWriter myStreamWriter = p.StandardInput;
        //p.BeginOutputReadLine();
        //    p.BeginErrorReadLine();
        //    //p.WaitForExit();
        //    //Console.ReadLine();
    }
}
