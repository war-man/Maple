﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using Humanizer;

using Microsoft.Win32;

namespace Maple.Core
{
    /// <summary>
    /// A helper class for generating a report containing details related to
    /// <c>System</c>, <c>Process</c>, <c>Assemblies</c>, <c>Network</c> and <c>Environment</c>
    /// on which the application executes.
    /// </summary>
    public static class DiagnosticReport
    {
        private const char Pipe = '|';
        private const char Dot = '.';
        private const char Dash = '-';
        private const char Space = ' ';
        private const char Colon = ':';
        private static readonly string NewLine = Environment.NewLine;
        private static readonly string LinePrefix = Pipe + "\t";

        private static readonly Action<StringBuilder>[] Actions =
        {
            AddSystem,
            AddProcess,
            AddDrives,
            AddAssemblies,
            AddNetworking
        };

        private static readonly string[] SystemHeaders =
        {
            "OS",
            "64Bit OS",
            "CLR Runtime",
            "FQDN",
            "Machine Name",
            "Installed RAM",
            "CPU",
            "CPU Core Count",
            "User",
            "System Directory",
            "Current Directory"
        };

        private static readonly string[] ProcessHeaders =
        {
            "Id",
            "Name",
            "Started",
            "Loaded In",
            "Optimized",
            "64Bit Process",
            "Large Address Aware",
            "Module Name",
            "Module File Name",
            "Product Name",
            "Original File Name",
            "File Name",
            "File Version",
            "Product Version",
            "Language",
            "Copyright",
            "WorkingSet",
            "Interactive",
            "CommandLine"
        };

        private static readonly string[] DrivesHeaders =
        {
            "Name", "Type", "Format", "Label", "Capacity(GB)", "Free(GB)", "Available(GB)"
        };

        private static readonly string[] AssemblyHeaders =
        {
            "Name", "GAC", "64Bit", "Optimized", "Framework", "Location", "CodeBase"
        };

        /// <summary>
        /// Returns the details related to <c>System</c>, <c>Process</c>, <c>Assemblies</c>
        /// and <c>Environment</c> on which the application executes.
        /// </summary>
        public static string Generate(DiagnosticReportType type = DiagnosticReportType.Full)
        {
            try
            {
                return GenerateImpl(type);
            }
            catch (Exception e)
            {
                return $"Unable to generate the Diagnostic Report. Error:{NewLine}\t{e}";
            }
        }

        private static string GenerateImpl(DiagnosticReportType type)
        {
            var sw = Stopwatch.StartNew();

            var builder = new StringBuilder();
            for (var i = 0; i < Actions.Length; i++)
            {
                var enabled = ((int)type & (1 << i)) != 0;
                if (enabled) { Actions[i](builder); }
            }

            sw.Stop();

            builder.Insert(0, $"/{NewLine}{Pipe}Diagnostic Report generated at: {DateTime.Now:dd-MM-yyyy HH:mm:ss.fff} in: {sw.Elapsed.TotalMilliseconds} milliseconds.{NewLine}");
            builder.Append('\\');
            return builder.ToString();
        }

        private static void AddSystem(StringBuilder builder)
        {
            var maxHeaderLength = SystemHeaders.Max(h => h.Length);
            var formatter = "{0,-" + (maxHeaderLength + 1) + "}";

            var sectionIndex = builder.Length;
            Format(SystemHeaders[0], Environment.OSVersion);
            Format(SystemHeaders[1], Environment.Is64BitOperatingSystem);
            Format(SystemHeaders[2], Environment.Version);
            Format(SystemHeaders[4], Environment.MachineName);
            Format(SystemHeaders[3], NetworkHelper.GetFQDN());
            Format(SystemHeaders[8], Environment.UserDomainName + "\\" + Environment.UserName);
            Format(SystemHeaders[6], GetProcessorName());
            Format(SystemHeaders[7], Environment.ProcessorCount);
            Format(SystemHeaders[5], GetInstalledMemoryInGigaBytes());
            Format(SystemHeaders[9], Environment.SystemDirectory);
            Format(SystemHeaders[10], Environment.CurrentDirectory);

            var maxLineLength = GetMaximumLineLength(builder, sectionIndex);
            builder.Insert(sectionIndex, GetSeperator("System", maxLineLength));

            void Format(string key, object value)
            {
                builder
                    .Append(LinePrefix)
                    .Append(Dot)
                    .Append(Space)
                    .AppendFormat(formatter, key)
                    .Append(Colon)
                    .Append(Space)
                    .AppendLine(value.ToString());
            }
        }

        private static void AddDrives(StringBuilder builder)
        {
            var values = new List<string[]>();

            foreach (var d in DriveInfo.GetDrives())
            {
                var row = new string[7];
                row[0] = d.Name;
                row[1] = d.DriveType.ToString();

                var dashString = Dash.ToString();
                string driveFormat = dashString, volumeLabel = dashString;
                double capacity = 0, free = 0, available = 0;

                if (d.IsReady)
                {
                    driveFormat = d.DriveFormat;
                    volumeLabel = d.VolumeLabel;
                    capacity = d.TotalSize.Bytes().Gigabytes;
                    free = d.TotalFreeSpace.Bytes().Gigabytes;
                    available = d.AvailableFreeSpace.Bytes().Gigabytes;
                }

                row[2] = driveFormat;
                row[3] = volumeLabel;
                row[4] = capacity.ToString("N0");
                row[5] = free.ToString("N0");
                row[6] = available.ToString("N0");

                values.Add(row);
            }

            WrapInTable(builder, DrivesHeaders, values);
        }

        private static void AddNetworking(StringBuilder builder)
        {
            var result = ProcessHelper.ExecuteAsync("ipconfig", "/all").Result;

            var sectionIndex = builder.Length;
            foreach (var entry in result.StandardOutput)
            {
                var entryLength = entry.Length;
                if (entryLength == 0) { continue; }

                builder.Append(LinePrefix);

                var firstChar = entry[0];
                if (firstChar != Space)
                {
                    // Add extra line between the new title and the previous
                    // lines only if it's not the first title
                    if (!entry.Equals("Windows IP Configuration", StringComparison.Ordinal))
                    {
                        builder.AppendLine().Append(LinePrefix);
                    }

                    if (entry[entryLength - 1] == Colon) { entryLength = entryLength - 1; }

                    builder.Append(Dot).Append(Space);
                    for (var k = 0; k < entryLength; k++) { builder.Append(entry[k]); }

                    builder.AppendLine().Append(LinePrefix);

                    builder.Append(Space).Append(Space);
                    for (var j = 0; j < entryLength; j++) { builder.Append(Dash); }
                }
                else
                {
                    builder.Append(Space).Append(Space);

                    if (entryLength > 4)
                    {
                        if (entry[3] == Space)
                        {
                            builder.Append(Space).Append(Space);
                        }
                        else
                        {
                            builder.Append(Dot).Append(Space);
                        }

                        for (var i = 3; i < entryLength; i++)
                        {
                            builder.Append(entry[i]);
                        }
                    }
                    else
                    {
                        builder.Append("[Invalid Data]");
                    }
                }

                builder.AppendLine();
            }

            var maxLineLength = GetMaximumLineLength(builder, sectionIndex);
            builder.Insert(sectionIndex, GetSeperator("Networking", maxLineLength));
        }

        private static void AddProcess(StringBuilder builder)
        {
            var maxHeaderLength = ProcessHeaders.Max(h => h.Length);
            var formatter = "{0,-" + (maxHeaderLength + 1) + "}";

            var sectionIndex = builder.Length;
            using (var p = Process.GetCurrentProcess())
            {
                var pVerInfo = p.MainModule.FileVersionInfo;
                Format(ProcessHeaders[0], p.Id);
                Format(ProcessHeaders[1], p.ProcessName);
                Format(ProcessHeaders[2], p.StartTime.ToString("dd-MM-yyyy HH:mm:ss.fff"));
                Format(ProcessHeaders[3], ApplicationHelper.GetProcessStartupDuration());
                Format(ProcessHeaders[17], Environment.UserInteractive);
                Format(ProcessHeaders[4], IsOptimized());
                Format(ProcessHeaders[5], Environment.Is64BitProcess);
                Format(ProcessHeaders[6], ApplicationHelper.IsProcessLargeAddressAware());
                Format(ProcessHeaders[16], Environment.WorkingSet.Bytes().Humanize());
                Format(ProcessHeaders[12], pVerInfo.FileVersion);
                Format(ProcessHeaders[13], pVerInfo.ProductVersion);
                Format(ProcessHeaders[14], pVerInfo.Language);
                Format(ProcessHeaders[15], pVerInfo.LegalCopyright);
                Format(ProcessHeaders[10], pVerInfo.OriginalFilename);
                Format(ProcessHeaders[11], pVerInfo.FileName);
                Format(ProcessHeaders[7], p.MainModule.ModuleName);
                Format(ProcessHeaders[8], p.MainModule.FileName);
                Format(ProcessHeaders[9], pVerInfo.ProductName);

                var cmdArgs = Environment.GetCommandLineArgs();
                Format(ProcessHeaders[18], cmdArgs[0]);

                for (var i = 1; i < cmdArgs.Length; i++)
                {
                    builder
                        .Append(LinePrefix)
                        .Append(Space).Append(Space)
                        .AppendFormat(formatter, string.Empty)
                        .Append(Space).Append(Space)
                        .AppendLine(cmdArgs[i]);
                }

                var maxLineLength = GetMaximumLineLength(builder, sectionIndex);
                builder.Insert(sectionIndex, GetSeperator("Process", maxLineLength));

                void Format(string key, object value)
                {
                    builder
                        .Append(LinePrefix)
                        .Append(Dot)
                        .Append(Space)
                        .AppendFormat(formatter, key)
                        .Append(Colon)
                        .Append(Space)
                        .AppendLine(value.ToString());
                }

                string IsOptimized()
                {
                    var executingAssembly = Assembly.GetEntryAssembly();
                    return executingAssembly == null
                        ? "N/A - Assembly was called from Unmanaged code."
                        : executingAssembly.IsOptimized().ToString();
                }
            }
        }

        private static void AddAssemblies(StringBuilder builder)
        {
            var sectionIndex = builder.Length;

            var maxHeaderLength = AssemblyHeaders.Max(h => h.Length);

            var nameFormatter = "{0}{1:D3}{2} {3,-" + (maxHeaderLength + 1) + "}: {4}{5}";
            var formatter = "{0,-" + (maxHeaderLength + 1) + "}";

            var assCounter = 1;
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(ass => !ass.IsDynamic)
                .OrderByDescending(o => o.GlobalAssemblyCache)
                .ForEach(x =>
                {
                    builder.AppendFormat(nameFormatter, LinePrefix, assCounter, Pipe, AssemblyHeaders[0], x.FullName, NewLine);

                    Format(AssemblyHeaders[1], x.GlobalAssemblyCache);
                    Format(AssemblyHeaders[2], !x.Is32Bit());
                    Format(AssemblyHeaders[3], x.IsOptimized());
                    Format(AssemblyHeaders[4], x.GetFrameworkVersion());
                    Format(AssemblyHeaders[5], x.Location);
                    Format(AssemblyHeaders[6], x.CodeBase);
                    assCounter++;
                });

            var maxLineLength = GetMaximumLineLength(builder, sectionIndex);
            builder.Insert(sectionIndex, GetSeperator("Assemblies", maxLineLength));

            void Format(string key, object value)
            {
                builder
                    .Append(LinePrefix)
                    .Append(Space).Append(Space).Append(Space)
                    .Append(Dot)
                    .Append(Space)
                    .AppendFormat(formatter, key)
                    .Append(Colon)
                    .Append(Space)
                    .AppendLine(value.ToString());
            }
        }

        private static void WrapInTable(StringBuilder builder, string[] columnHeaders, List<string[]> values)
        {
            foreach (var row in values)
            {
                if (row.Length != columnHeaders.Length)
                {
                    throw new InvalidDataException("There should be a corresponding data for every column header");
                }
            }

            // initialize cellLengths first based on length of the headers
            var cellLengths = new int[columnHeaders.Length];
            for (var i = 0; i < columnHeaders.Length; i++)
            {
                var headerLength = columnHeaders[i].Length;
                cellLengths[i] = headerLength;
            }

            foreach (var row in values)
            {
                for (var i = 0; i < columnHeaders.Length; i++)
                {
                    var cellVal = row[i];
                    if (cellVal.Length > cellLengths[i])
                    {
                        cellLengths[i] = cellVal.Length;
                    }
                }
            }

            for (var i = 0; i < cellLengths.Length; i++)
            {
                cellLengths[i] = cellLengths[i] + 2;
            }

            var headerBuilder = new StringBuilder();

            // insert headers
            headerBuilder.Append(LinePrefix);
            for (var i = 0; i < columnHeaders.Length; i++)
            {
                var headerVal = columnHeaders[i];
                var formatter = "{0} {1,-" + (cellLengths[i] - 2) + "} ";
                headerBuilder.AppendFormat(formatter, Pipe, headerVal);
            }
            headerBuilder.Append(Pipe).AppendLine();

            // insert headers underline
            headerBuilder.Append(LinePrefix);
            for (var i = 0; i < columnHeaders.Length; i++)
            {
                headerBuilder.Append(Pipe).Append(new string(Dash, cellLengths[i]));
            }

            var maxLineLengthInHeader = GetMaximumLineLength(headerBuilder);
            var beginAndEnd = $"{LinePrefix} {new string(Dash, maxLineLengthInHeader - LinePrefix.Length - 2)}{NewLine}";
            headerBuilder.Insert(0, beginAndEnd);

            var beginPos = builder.Length;

            // insert row values
            builder.Append(Pipe).AppendLine();
            foreach (var row in values)
            {
                builder.Append(LinePrefix);
                for (var j = 0; j < row.Length; j++)
                {
                    var formatter = "{0} {1,-" + (cellLengths[j] - 2) + "} ";
                    builder.AppendFormat(formatter, Pipe, row[j]);
                }
                builder.Append(Pipe).AppendLine();
            }

            builder.Insert(beginPos, headerBuilder.ToString());
            builder.Append(beginAndEnd);

            var maxLineLength = GetMaximumLineLength(builder, beginPos);
            builder.Insert(beginPos, GetSeperator("Drives", maxLineLength));
        }

        private static int GetMaximumLineLength(StringBuilder builder, int start = 0)
        {
            if (start >= builder.Length) { throw new IndexOutOfRangeException(); }

            int maxLength = 0, tmpLength = 0;
            var prevChar = '\0';

            for (var i = start; i < builder.Length; i++)
            {
                var currChar = builder[i];

                if (currChar == '\n')
                {
                    if (prevChar == '\r') { --tmpLength; }
                    if (maxLength < tmpLength) { maxLength = tmpLength; }
                    tmpLength = 0;
                }
                else { tmpLength++; }

                prevChar = currChar;
            }
            return maxLength;
        }

        /// <summary>
        /// Returns the full CPU name using the registry.
        /// See <see href="http://stackoverflow.com/questions/2376139/get-full-cpu-name-without-wmi"/>
        /// </summary>
        /// <returns>The CPU Name</returns>
        private static string GetProcessorName()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0\");
            return key?.GetValue("ProcessorNameString").ToString() ?? "Not Found";
        }

        private static string GetInstalledMemoryInGigaBytes()
        {
            GetPhysicallyInstalledSystemMemory(out var installedMemoryKb);
            return installedMemoryKb.Bytes().Humanize();
        }

        private static string GetSeperator(string title, int count)
        {
            return $"{Pipe}{NewLine}{Pipe}{title}{Pipe}{new string(Dot, count - title.Length)}{NewLine}{Pipe}{NewLine}";
        }

        /// <summary>
        /// <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/cc300158(v=vs.85).aspx"/>
        /// </summary>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysicallyInstalledSystemMemory(out long totalMemoryInKilobytes);
    }
}
