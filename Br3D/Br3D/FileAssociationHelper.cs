using Microsoft.Win32;
using System;

namespace Br3D
{
    // 파일 연결 관리
    public class FileAssociationHelper
    {
        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

        // user choice key : 이 키는 수정 불가(삭제 / 읽기만 가능), 즉 수정이 필요하면 삭제하고 다시 써야함
        static RegistryKey GetUserChoiceKey(string ext)
        {
            // UserChoice가 있으면 UserChoice의 값을 검사
            var path = $"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.{ext}\\UserChoice";
            return Registry.CurrentUser.OpenSubKey(path, false);
        }

        static RegistryKey GetOpenWithListKey(string ext)
        {
            var path = $"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.{ext}\\OpenWithList";
            return Registry.CurrentUser.OpenSubKey(path, true);
        }


        // 확장자별 연결된 프로그램 이름 리턴
        // 컴퓨터\HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.ifc\OpenWithList
        static public string GetChoicedProgramNameByExt(string ext)
        {
            // UserChoice가 있으면 UserChoice의 값을 검사
            try
            {
                var choicedKey = GetUserChoiceKey(ext);
                if (choicedKey != null)
                {
                    var val = choicedKey.GetValue("ProgId") as string;
                    if (string.IsNullOrEmpty(val))
                        return null;
                    val = val.Replace("Applications\\", "");
                    return val;
                }

            }
            catch
            {
                //DeleteUserChoiceKey(ext);
            }


            // userchoice가 없으면 a의 값을 리턴
            var key = GetOpenWithListKey(ext);
            if (key == null)
                return null;
            return key.GetValue("a") as string;
        }




        public static void SetAssociation_User(string Extension, string OpenWith, string ExecutableName)
        {
            if (Extension.StartsWith("."))
                Extension.Remove(0, 1);

            try
            {
                using (RegistryKey User_Classes = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\", true))
                using (RegistryKey User_Ext = User_Classes.CreateSubKey("." + Extension))
                using (RegistryKey User_AutoFile = User_Classes.CreateSubKey(Extension + "_auto_file"))
                using (RegistryKey User_AutoFile_Command = User_AutoFile.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command"))
                using (RegistryKey ApplicationAssociationToasts = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\ApplicationAssociationToasts\\", true))
                using (RegistryKey User_Classes_Applications = User_Classes.CreateSubKey("Applications"))
                using (RegistryKey User_Classes_Applications_Exe = User_Classes_Applications.CreateSubKey(ExecutableName))
                using (RegistryKey User_Application_Command = User_Classes_Applications_Exe.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command"))
                using (RegistryKey User_Explorer = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\." + Extension))
                using (RegistryKey User_Choice = User_Explorer.OpenSubKey("UserChoice"))
                {
                    User_Ext.SetValue("", Extension + "_auto_file", RegistryValueKind.String);
                    User_Classes.SetValue("", Extension + "_auto_file", RegistryValueKind.String);
                    User_Classes.CreateSubKey(Extension + "_auto_file");
                    User_AutoFile_Command.SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
                    ApplicationAssociationToasts.SetValue(Extension + "_auto_file_." + Extension, 0);
                    ApplicationAssociationToasts.SetValue(@"Applications\" + ExecutableName + "_." + Extension, 0);
                    User_Application_Command.SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
                    User_Explorer.CreateSubKey("OpenWithList").SetValue("a", ExecutableName);
                    User_Explorer.CreateSubKey("OpenWithProgids").SetValue(Extension + "_auto_file", "0");

                    var mruList = User_Explorer.CreateSubKey("OpenWithList").GetValue("MRUList").ToString();
                    if (!mruList.StartsWith("a"))
                    {
                        mruList = "a" + mruList;
                        User_Explorer.CreateSubKey("OpenWithList").SetValue("MRUList", mruList);
                    }

                    if (User_Choice != null) User_Explorer.DeleteSubKey("UserChoice");
                    User_Explorer.CreateSubKey("UserChoice").SetValue("ProgId", @"Applications\" + ExecutableName);
                }
                SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
            }
            catch/* (Exception ext)*/
            {
                //Your code here
                //MessageBox.Show(ext.Message);
            }
        }
    }
}
