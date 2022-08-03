using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D
{
    // 파일 연결 관리
    public class FileAssociationHelper
    {

        static RegistryKey GetUserChoiceKey(string ext)
        {
            // UserChoice가 있으면 UserChoice의 값을 검사
            var path = $"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.{ext}\\UserChoice";
            return Registry.CurrentUser.OpenSubKey(path, true);
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
            var key = GetUserChoiceKey(ext);
            if(key != null)
            {
                var val = key.GetValue("ProgId") as string;
                if (string.IsNullOrEmpty(val))
                    return null;
                val = val.Replace("Applications\\", "");
                return val;
            }

            // userchoice가 없으면 a의 값을 리턴
            key = GetOpenWithListKey(ext);
            if (key == null)
                return null;
            return key.GetValue("a") as string;
        }

        // 해당 확장자에 연결된 프로그램 이름 목록 리턴
        static List<string> GetProgramNamesByExt(string ext)
        {
            var key = GetOpenWithListKey(ext);
            if (key == null)
                return null;

            List<string> programNames = new List<string>();
            // OpenWithList에 이름이 있는지 검사하고, 없으면 마지막 빈칸에 이름을 쓴다.
            for (int i = 'a'; i <= 'z'; ++i)
            {
                var alphaStr = ((char)i).ToString();
                var val = key.GetValue(alphaStr) as string;
                // 값이 없으면 여기에 쓴다.
                if (string.IsNullOrEmpty(val))
                    break;
                programNames.Add(val);
            }

            return programNames;
        }

   

        // 
        internal static void SetProgramNameByExt(string ext, string programName)
        {
            var key = GetOpenWithListKey(ext);
            if (key == null)
                return;

            // OpenWithList에 이름이 있는지 검사하고, 없으면 마지막 빈칸에 이름을 쓴다.
            var programNames = GetProgramNamesByExt(ext);

            if(!programNames.Contains(programName))
            {
                var alphaStr = ((char)(programNames.Count + 'a')).ToString();
                key.SetValue(alphaStr, programName);
                programNames.Add(programName);
            }

            // 2개 이상이면 UserChoice에 등록
            if (programNames.Count > 1)
            {
                key = GetUserChoiceKey(ext);
                if (key == null)
                    return;

                key.SetValue("ProgId", $"Applications\\{programName}");
            }
        }
    }
}
