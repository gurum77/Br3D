namespace Br3D
{
    static public class VersionHelper
    {
        static public bool isLT { get; set; } = false;
        static public string appName
        {
            get
            {
                return isLT ? "Br3DLT" : "Br3D";
            }
        }

        // LT인지? 파일명으로 구분한다.
        static public void InitVersion()
        {
            var fileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            fileName = fileName.ToLower();
            isLT = fileName.EndsWith("lt.exe");
        }

    }
}
