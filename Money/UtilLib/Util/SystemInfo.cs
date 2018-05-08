using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System;

namespace sver
{

    public class SystemInfo
    {
        static public string Info()
        {
            Process proc = Process.GetCurrentProcess();
            StringBuilder str = new StringBuilder();
            str.Append("进程影象名：" + proc.ProcessName).Append(" ");
            str.Append("进程ID：" + proc.Id).Append(" ");
            str.Append("启动线程树：" + proc.Threads.Count.ToString()).Append(" ");
            str.Append("CPU占用时间：" + proc.TotalProcessorTime.ToString()).Append(" ");
            str.Append("线程优先级：" + proc.PriorityClass.ToString()).Append(" ");
            str.Append("启动时间：" + proc.StartTime.ToLongTimeString()).Append(" ");

            str.Append("专用内存：" + (proc.PrivateMemorySize64 / 1024).ToString() + "K").Append(" ");
            str.Append("峰值虚拟内存：" + (proc.PeakVirtualMemorySize64 / 1024).ToString() + "K").Append(" ");
            str.Append("峰值分页内存：" + (proc.PeakPagedMemorySize64 / 1024).ToString() + "K").Append(" ");
            str.Append("分页系统内存：" + (proc.PagedSystemMemorySize64 / 1024).ToString() + "K").Append(" ");
            str.Append("分页内存：" + (proc.PagedMemorySize64 / 1024).ToString() + "K").Append(" ");
            str.Append("未分页系统内存：" + (proc.NonpagedSystemMemorySize64 / 1024).ToString() + "K").Append(" ");
            str.Append("物理内存：" + (proc.WorkingSet64 / 1024).ToString() + "K").Append(" ");
            str.Append("虚拟内存：" + (proc.VirtualMemorySize64 / 1024).ToString() + "K").Append(" ");
            return str.ToString();
        }
        
    }
}
