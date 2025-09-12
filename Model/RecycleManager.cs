using Microsoft.Extensions.Logging;
using Shell32;
using System.IO;

namespace Recycli.Model
{
    public class RecycleManager
    {
        private readonly ILogger<RecycleManager> _logger;

        public RecycleManager(ILogger<RecycleManager> logger)
        {
            _logger = logger;
        }

        public List<RecycleFile> GetRecycleBinFiles()
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

                _logger.LogInformation("GetRecycleBinFiles(): Adding File {value}", recycleFile.Name);
                recycleBinFiles.Add(recycleFile);
            }
            return recycleBinFiles;
        }

        public bool RestoreFile(RecycleFile recycleFile)
        {
            /*
             * This works only when the first verb is "Restore".
             * It seems to work fine on an English and German Windows 11 system.
             * I don't know if it works on any other languages.
             */
            var restoreOperation = (FolderItemVerb)recycleFile.Verbs[0];
            restoreOperation.DoIt(); 
           
            return false;
        }

        public void DeleteFile(RecycleFile recycleFile)
        {
            /* 
             * We don't use the verb "Delete" directly because this results in a confirmation dialog.
             * Instead, we delete the file directly using File.Delete.
             * This is a more straightforward approach for the recycle bin management.
             */
            _logger.LogInformation("DeleteFile(): Delete File {value}", recycleFile.Name);
            File.Delete(recycleFile.Path);
        }
    }
}
