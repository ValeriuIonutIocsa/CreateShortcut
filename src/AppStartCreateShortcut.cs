namespace CreateShortcut
{
    using System;
    using System.IO;

    class AppStartCreateShortcut
    {
        static void Main(string[] args)
        {
            if (args.Length >= 1 && "-help".Equals(args[0]))
            {
                string helpMessage = CreateHelpMessage();
                Console.WriteLine(helpMessage);
                Environment.Exit(0);
            }

            if (args.Length < 2)
            {
                string helpMessage = CreateHelpMessage();
                Console.Error.WriteLine("ERROR - insufficient arguments" +
                    Environment.NewLine + helpMessage);
                Environment.Exit(1);
            }

            string targetPathString = Path.GetFullPath(args[0]);
            string shortcutName = args[1];

            string shortcutDirPathString;
            if (args.Length < 3)
            {
                shortcutDirPathString = @"D:\IVI_MISC\Shortcuts";
            }
            else
            {
                shortcutDirPathString = Path.GetFullPath(args[2]);
            }

            string workingDirPathString;
            if (args.Length < 4)
            {
                string? tmpWorkingDirPathString = Path.GetDirectoryName(targetPathString);
                if (tmpWorkingDirPathString == null)
                {
                    workingDirPathString = targetPathString;
                }
                else
                {
                    workingDirPathString = tmpWorkingDirPathString;
                }
            }
            else
            {
                workingDirPathString = Path.GetFullPath(args[3]);
            }

            string description;
            if (args.Length < 5)
            {
                description = shortcutName;
            }
            else
            {
                description = args[4];
            }

            string shortcutPath = Path.Combine(shortcutDirPathString, shortcutName + ".lnk");

            Console.WriteLine("shortcut path: " + shortcutPath);
            Console.WriteLine("target path: " + targetPathString);
            Console.WriteLine("working dir path: " + workingDirPathString);
            Console.WriteLine("description: " + description);

            ShellLink.CreateShortcut(
                shortcutPath,
                targetPathString,
                description,
                workingDirPathString
            );
        }

        private static string CreateHelpMessage()
        {
            return "usage: create_shortcut <target_path> <shortcut_name>"
                + " (<shortcut_dir>) (<working_dir>) (<description>)";
        }
    }
}
