using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using jsmhToolChest.Libraries;
using FastWin32.Diagnostics;
using System.Threading;
using System.Diagnostics;

namespace jsmhToolChest.Netease
{
    internal class CL8
    {
        public static void WriteCL8()
        {
            Program.mainWindow.LogTime();
            try
            {
                System.IO.File.WriteAllBytes(Program.mainWindow.regedit.GetGamePatch() + "\\Game\\.minecraft\\mods\\CL8🤔😋😅🤣😂😡.jar", Resource1.CL8);
                Program.mainWindow.SuccessLogs("CL8写出成功");
            } catch (Exception e)
            {
                Program.mainWindow.ErrorLogs("CL8写出错误，错误信息: " + e.Message);
            }
            
        }

    }
    internal class CL24
    {
        public static void WriteCL24(Process GameProcessPID)
        {
            Program.mainWindow.LogTime();
            try
            {
                //System.IO.File.WriteAllBytes(Config.Config_folder + "\\ClientLauncher24.dll", Resource1.CL24);
                DLLInjector.InjectDLL("CL24.dll", Resource1.CL24, GameProcessPID);
                Program.mainWindow.SuccessLogs("CL24写出成功");
            } catch (Exception e)
            {
                Program.mainWindow.ErrorLogs("CL24写出错误，错误信息: " + e.Message);
            }
        }
    }
}
