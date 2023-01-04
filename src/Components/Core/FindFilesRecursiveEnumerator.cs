namespace FileSystemProcessor;

public class FindFilesRecursiveEnumerable : IEnumerable<FileSystemInfo>
{
    #region Constructor
    public FindFilesRecursiveEnumerable(FindFilesOptions fileOptions, FindFilesOptions folderOptions)
    {
        FileOptions = fileOptions;
        FolderOptions = folderOptions;
    }
    #endregion


    #region Properties
    public FindFilesOptions FileOptions { get; set; }
    public FindFilesOptions FolderOptions { get; set; }

    public IEnumerator<FileSystemInfo> GetEnumerator() => new FindFilesRecursiveEnumerator(FileOptions, FolderOptions);

    IEnumerator IEnumerable.GetEnumerator() => new FindFilesRecursiveEnumerator(FileOptions, FolderOptions);
    #endregion
}

public class FindFilesRecursiveEnumerator : IEnumerator<FileSystemInfo>
{
    #region Constructor
    public FindFilesRecursiveEnumerator(FindFilesOptions fileOptions, FindFilesOptions folderOptions)
    {
        FileOptions = fileOptions;
        FolderOptions = folderOptions;
        FolderStack = new Stack<IEnumerator<FileSystemInfo>>();
    }
    #endregion

    #region Properties
    public FindFilesOptions FileOptions { get; set; }
    public FindFilesOptions FolderOptions { get; set; }
    public Action<DirectoryInfo> EnterFolder { get; set; }
    public Action<DirectoryInfo> ExitFolder { get; set; }

    protected IEnumerator<FileSystemInfo> CurrentFileEnumerator { get; private set; }
    protected IEnumerator<FileSystemInfo> CurrentFolderEnumerator { get; private set; }

    protected Stack<IEnumerator<FileSystemInfo>> FolderStack { get; set; }
    #endregion

    #region IEnumerator<FileSystemInfo>
    public FileSystemInfo Current => CurrentFileEnumerator?.Current ?? null;

    object IEnumerator.Current => CurrentFileEnumerator?.Current ?? null;


    public void Dispose()
    {
        Reset();
    }

    public bool MoveNext()
    {
        if (CurrentFolderEnumerator == null)
            StartSubFolder();

        if (FileMoveNext())
            return true;

        bool keepGoing = false;

        if (FolderMoveNext())
        {
            StartSubFolder();
            if (!FileMoveNext())
                keepGoing = DrillDown();
            else
                keepGoing = true;
        }
        else
            keepGoing = GoBackUp();

        if (keepGoing && CurrentFileEnumerator.Current is null)
            keepGoing = false;

        return keepGoing;
    }

    public void Reset()
    {
        CurrentFileEnumerator?.Dispose();
        CurrentFileEnumerator = null;

        CurrentFolderEnumerator?.Dispose();
        CurrentFolderEnumerator = null;


        while (FolderStack.Count > 0)
        {
            IEnumerator<FileSystemInfo> fsi = FolderStack.Pop();
            fsi?.Dispose();
        }
    }
    #endregion

    #region Folder Stack Functions
    protected bool FileMoveNext() => CurrentFileEnumerator?.MoveNext() == true;

    protected bool FolderMoveNext() => CurrentFolderEnumerator?.MoveNext() == true;

    private bool DrillDown()
    {
        bool keepGoing = false;

        do
        {
            if (FolderMoveNext())
            {
                StartSubFolder();
                keepGoing = FileMoveNext();
            }
            else if (FolderStack.Count > 0)
                keepGoing = GoBackUp();
            else
                break;
        } while (!keepGoing);

        return keepGoing;
    }

    private bool GoBackUp()
    {
        bool keepGoing = false;

        do
        {
            EndSubFolder();
            if (CurrentFolderEnumerator is not null)
            {
                if (FolderMoveNext())
                {
                    StartSubFolder();
                    keepGoing = FileMoveNext();
                    if (!keepGoing)
                        keepGoing = DrillDown();
                }

                //else
                //    keepGoing = DrillDown();
            }
        } while (!keepGoing && FolderStack.Count != 0);

        if (FolderStack.Count == 0)
            keepGoing = false;

        return keepGoing;
    }

    private void StartSubFolder()
    {
        if (CurrentFolderEnumerator != null)
        {
            if (CurrentFolderEnumerator.Current is DirectoryInfo)
                EnterFolder?.Invoke(CurrentFolderEnumerator.Current as DirectoryInfo);
            FolderOptions.Path = CurrentFolderEnumerator.Current.FullName;
            FolderStack.Push(CurrentFolderEnumerator);
        }

        CurrentFileEnumerator?.Dispose();
            
        FileOptions.Path = FolderOptions.Path;

        FindFolders findFolder = new FindFolders(FolderOptions);
        CurrentFolderEnumerator = findFolder.Enumerate().GetEnumerator();

        FindFiles findFiles = new FindFiles(FileOptions);
        CurrentFileEnumerator = findFiles.Enumerate().GetEnumerator();

    }

    private void EndSubFolder()
    {
        CurrentFileEnumerator?.Dispose();
        CurrentFileEnumerator = null;
     
        CurrentFolderEnumerator?.Dispose();
        CurrentFolderEnumerator = null;

        if (FolderStack.Count > 0)
        {
            CurrentFolderEnumerator = FolderStack.Pop();
            if (CurrentFolderEnumerator.Current is DirectoryInfo)
                ExitFolder?.Invoke(CurrentFolderEnumerator.Current as DirectoryInfo);
        }
    }
    #endregion
}
