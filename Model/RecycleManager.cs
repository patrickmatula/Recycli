using Microsoft.Extensions.Logging;
using Shell32;
using System.IO;

namespace Recycli.Model
{
    internal class RecycleManager
    {
        private static readonly ILogger? logger = App.Logger;
        public static List<RecycleFile> GetRecycleBinFiles()
        {
            List<RecycleFile> recycleBinFiles = new List<RecycleFile>();
            Shell shell = new Shell();
            Folder recycleBin = shell.NameSpace(10); // 10 = Recycle Bin

            if (recycleBin == null)
            {
                return recycleBinFiles;
            }

            foreach (FolderItem2 item in recycleBin.Items())
            {
                RecycleFile recycleFile = new RecycleFile
                {
                    Name = item.Name.ToString(),
                    Size = item.Size / 1024,
                    Path = item.Path.ToString(),
                    Verbs = new List<object>()
                };

                foreach (FolderItemVerb verb in item.Verbs())
                {
                    recycleFile.Verbs.Add(verb);
                }

                logger.LogInformation("Recycli: GetRecycleBinFiles(): Adding File {value}", recycleFile.Name);
                recycleBinFiles.Add(recycleFile);
            }
            return recycleBinFiles;
        }

        public static bool RestoreFile(RecycleFile recycleFile)
        {

            foreach (FolderItemVerb verb in recycleFile.Verbs)
            {
                if (verb.Name.ToString().Replace("&", "").Trim().Equals("Restore", StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogInformation("Recycli: RestoreFile(): Restore File {value}, verb: {verb}", recycleFile.Name, verb.Name.ToString());
                    verb.DoIt();
                    return true;
                }
            }
            return false;
        }

        public static void DeleteFile(RecycleFile recycleFile)
        {
            /* We don't use the verb "Delete" directly because this results in a confirmation dialog.
             * Instead, we delete the file directly using File.Delete.
             * This is a more straightforward approach for the recycle bin management.
             */
            logger.LogInformation("Recycli: DeleteFile(): Delete File {value}", recycleFile.Name);
            File.Delete(recycleFile.Path);
        }
    }
}
