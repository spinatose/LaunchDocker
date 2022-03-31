using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LaunchDocker
{
    class Program
    {
        static void Main(string[] args)
        {
            string cmd = "-p 6379:6379 --name some-redis -d redis";

            if (args.Length > 0)
            {
                List<string> cmds = new(args);

                cmd = string.Join(" ", cmds);
                Console.WriteLine("command line args passed in");
            } else
                Console.WriteLine("no command line args passed in- using default");

            Console.WriteLine("commmand line args: {0}", cmd);

            ProcessStartInfo processInfo = new("docker", $"run {cmd}")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            int exitCode;
            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                process.OutputDataReceived += Process_OutputDataReceived;
                process.ErrorDataReceived += Process_ErrorDataReceived;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit(1200000);
                if (!process.HasExited)
                {
                    process.Kill();
                }

                exitCode = process.ExitCode;
                process.Close();
            }

            Console.WriteLine("Press {ENTER} to exit...");
            _ = Console.ReadLine();
        }

        private static void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine($"{e.Data}");
        }

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine($"{e.Data}");
        }
    }
}
