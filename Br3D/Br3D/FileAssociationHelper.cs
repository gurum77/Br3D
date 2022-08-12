using Microsoft.Win32;
using System.Collections.Generic;

namespace Br3D
{
    // 파일 연결 관리
    public class FileAssociationHelper
    {

        static void DeleteUserChoiceKey(string ext)
        {
            var path = $"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.{ext}\\UserChoice";
            Registry.CurrentUser.DeleteSubKey(path);
        }

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

            if (!programNames.Contains(programName))
            {
                var alphaStr = ((char)(programNames.Count + 'a')).ToString();
                key.SetValue(alphaStr, programName);
                programNames.Add(programName);
            }

            // 2개 이상이면 UserChoice에 등록
            if (programNames.Count > 1)
            {
                try
                {
                    key = GetUserChoiceKey(ext);
                    if (key == null)
                        return;

                    key.SetValue("ProgId", $"Applications\\{programName}");
                }
                catch
                {
                    // user choice에 접근 불가시 a에 등록함
                    var alphaStr = ((char)('a')).ToString();
                    key.SetValue(alphaStr, programName);
                }
            }
        }
    }
}
