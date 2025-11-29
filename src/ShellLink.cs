namespace CreateShortcut.src
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    [CoClass(typeof(CShellLinkW))]
    interface IShellLinkW
    {
        void GetPath(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
            int cchMaxPath,
            out IntPtr pfd,
            uint fFlags
        );
        IntPtr GetIDList();
        void SetIDList(IntPtr pidl);
        void GetDescription(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
            int cchMaxName
        );
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        void GetWorkingDirectory(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir,
            int cchMaxPath
        );
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
        void GetArguments(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs,
            int cchMaxPath
        );
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
        ushort GetHotKey();
        void SetHotKey(ushort wHotKey);
        uint GetShowCmd();
        void SetShowCmd(uint iShowCmd);
        void GetIconLocation(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
            int cchIconPath,
            out int piIcon
        );
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
        void SetRelativePath(
            [MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
            [Optional] uint dwReserved
        );
        void Resolve(IntPtr hwnd, uint fFlags);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }

    [ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    [ClassInterface(ClassInterfaceType.None)]
    class CShellLinkW { }

    public static class ShellLink
    {
        public static void CreateShortcut(
            string lnkPath,
            string targetPath,
            string description,
            string workingDirectory
        )
        {
            if (string.IsNullOrWhiteSpace(lnkPath))
                throw new ArgumentNullException("lnkPath");

            if (string.IsNullOrWhiteSpace(targetPath))
                throw new ArgumentNullException("targetPath");

            IShellLinkW link = new IShellLinkW();

            link.SetPath(targetPath);

            if (!string.IsNullOrWhiteSpace(description))
            {
                link.SetDescription(description);
            }

            if (!string.IsNullOrWhiteSpace(workingDirectory))
            {
                link.SetWorkingDirectory(workingDirectory);
            }

            string? lnkParentDirectory = Path.GetDirectoryName(lnkPath);
            if (lnkParentDirectory == null)
            {
                Console.Error.WriteLine("ERROR - failed to compute the shortcut folder path");
            }
            else
            {
                if (lnkParentDirectory != null && !Directory.Exists(lnkParentDirectory))
                {
                    Directory.CreateDirectory(lnkParentDirectory);
                }

                IPersistFile file = (IPersistFile)link;
                file.Save(lnkPath, true);
            }
            Marshal.FinalReleaseComObject(link);
        }
    }
}
