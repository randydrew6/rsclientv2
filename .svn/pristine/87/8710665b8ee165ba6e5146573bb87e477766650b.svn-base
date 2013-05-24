using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace RS_Client
{
    public  class Processor
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetSystemTimes(out System.Runtime.InteropServices.ComTypes.FILETIME lpIdleTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpKernelTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpUserTime);

        private static TimeSpan _sysIdleOldTs;
        private static TimeSpan _sysKernelOldTs;
        private static TimeSpan _sysUserOldTs;

        static Processor()
        {
        }

        public static Double GetUsage()
        {
            System.Runtime.InteropServices.ComTypes.FILETIME sysIdle, sysKernel, sysUser;

            if (GetSystemTimes(out sysIdle, out sysKernel, out sysUser))
            {
                TimeSpan sysIdleTs = GetTimeSpanFromFileTime(sysIdle);
                TimeSpan sysKernelTs = GetTimeSpanFromFileTime(sysKernel);
                TimeSpan sysUserTs = GetTimeSpanFromFileTime(sysUser);

                TimeSpan sysIdleDiffenceTs = sysIdleTs.Subtract(_sysIdleOldTs);
                TimeSpan sysKernelDiffenceTs = sysKernelTs.Subtract(_sysKernelOldTs);
                TimeSpan sysUserDiffenceTs = sysUserTs.Subtract(_sysUserOldTs);

                _sysIdleOldTs = sysIdleTs;
                _sysKernelOldTs = sysKernelTs;
                _sysUserOldTs = sysUserTs;

                TimeSpan system = sysKernelDiffenceTs.Add(sysUserDiffenceTs);

                Double cpuUsage = (((system.Subtract(sysIdleDiffenceTs).TotalMilliseconds) * 100) / system.TotalMilliseconds);

                return cpuUsage;
            }
            else
            {
                return 0;
            }
        }

        private static TimeSpan GetTimeSpanFromFileTime(System.Runtime.InteropServices.ComTypes.FILETIME time)
        {
            return TimeSpan.FromMilliseconds((((ulong)time.dwHighDateTime << 32) + (uint)time.dwLowDateTime) * 0.000001);
        }
    }
}
