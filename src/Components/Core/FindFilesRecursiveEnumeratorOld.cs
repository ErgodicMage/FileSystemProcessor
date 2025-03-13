namespace ErgodicMage.FileSystemProcessor;

public class FindFilesRecursiveEnumerableOld : IEnumerable<FileSystemInfo>
{
    #region Constructor
    public FindFilesRecursiveEnumerableOld(FindFilesOptions fileOptions, FindFilesOptions folderOptions)
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

public class FindFilesRecursiveEnumeratorOld : IEnumerator<FileSystemInfo>
{
    #region Constructor
    public FindFilesRecursiveEnumeratorOld(FindFilesOptions fileOptions, FindFilesOptions folderOptions)
    {
        FileOptions = fileOptions;
        FolderOptions = folderOptions;
        FolderStack = new Stack<IEnumerator<FileSystemInfo>>();
    }
    #endregion

    #region Properties
    public FindFilesOptions FileOptions { get; set; }
    public FindFilesOptions FolderOptions { get; set; }

    protected IEnumerator<FileSystemInfo> CurrentFileEnumerator { get; private set; }
    protected IEnumerator<FileSystemInfo> CurrentFolderEnumerator { get; private set; }

    protected Stack<IEnumerator<FileSystemInfo>> FolderStack { get; set; }
    #endregion

    #region IEnumerator<FileSystemInfo>
    public FileSystemInfo Current => CurrentFileEnumerator != null ? CurrentFileEnumerator.Current : null;

    object IEnumerator.Current => CurrentFileEnumerator != null ? CurrentFileEnumerator.Current : null;

    protected FileSystemInfo lastFileInfo;
    protected FileSystemInfo lastFolderInfo;

    public void Dispose()
    {
        Reset();
    }

    public bool MoveNext()
    {
        bool keepGoing = false;

        if (CurrentFolderEnumerator == null)
            StartSubFolder(null);

        keepGoing = FileMoveNext();

        if (!keepGoing && CurrentFolderEnumerator != null)
        {
            FileSystemInfo fsi = CurrentFolderEnumerator.Current;

            if (FolderMoveNext())
            {
                do
                {
                    fsi = CurrentFolderEnumerator.Current;
                    StartSubFolder(CurrentFolderEnumerator.Current);

                    keepGoing = FileMoveNext();

                    if (!keepGoing && !FolderMoveNext())
                    {
                        EndSubFolder(fsi);
                        FolderMoveNext();
                    }
                } while (!keepGoing && CurrentFolderEnumerator != null);
            }
            else
            {
                do
                {
                    EndSubFolder(fsi==null ? lastFolderInfo : fsi);

                    if (CurrentFolderEnumerator != null)
                    {
                        fsi = CurrentFolderEnumerator.Current;
                        if (FolderMoveNext())
                        {
                            StartSubFolder(CurrentFolderEnumerator.Current);
                            keepGoing = FileMoveNext();
                        }
                    }
                } while (!keepGoing && FolderStack.Count > 0);

                if (FolderStack.Count == 0)
                    keepGoing = false;
            }
        }

        return keepGoing;
    }

    protected bool FileMoveNext()
    {
        if (CurrentFileEnumerator == null)
            return false;

        if (CurrentFileEnumerator.Current != null)
            lastFileInfo = CurrentFileEnumerator.Current;
        return CurrentFileEnumerator.MoveNext();
    }

    protected bool FolderMoveNext()
    {
        if (CurrentFolderEnumerator == null)
            return false;

        if (CurrentFolderEnumerator.Current != null)
            lastFileInfo = CurrentFolderEnumerator.Current;
        return CurrentFolderEnumerator.MoveNext();
    }

    public void Reset()
    {
        if (CurrentFileEnumerator != null)
        {
            CurrentFileEnumerator.Dispose();
            CurrentFileEnumerator = null;
        }

        if (CurrentFolderEnumerator != null)
        {
            CurrentFolderEnumerator.Dispose();
            CurrentFolderEnumerator = null;
        }

        while (FolderStack.Count > 0)
        {
            IEnumerator<FileSystemInfo> fsi = FolderStack.Pop();
            if (fsi != null)
                fsi.Dispose();
        }
    }
    #endregion

    #region Folder Stack Functions
    private void StartSubFolder(FileSystemInfo folderInfo)
    {
        if (folderInfo != null)
            EnterSubFolder(folderInfo);

        if (CurrentFolderEnumerator != null)
        {
            lastFolderInfo = CurrentFolderEnumerator.Current;
            FolderStack.Push(CurrentFolderEnumerator);
            if (folderInfo != null)
                FolderOptions.Path = folderInfo.FullName;
        }

        if (CurrentFileEnumerator != null)
        {
            CurrentFileEnumerator.Dispose();
            FileOptions.Path = FolderOptions.Path;
        }

        FindFolders findFolder = new FindFolders(FolderOptions);
        CurrentFolderEnumerator = findFolder.Enumerate().GetEnumerator();

        FindFiles findFiles = new FindFiles(FileOptions);
        CurrentFileEnumerator = findFiles.Enumerate().GetEnumerator();

    }

    protected virtual void EnterSubFolder(FileSystemInfo folderInfo)
    {
    }

    private void EndSubFolder(FileSystemInfo fsi)
    {
        if (CurrentFileEnumerator != null)
        {
            CurrentFileEnumerator.Dispose();
            CurrentFileEnumerator = null;
        }

        lastFolderInfo = CurrentFolderEnumerator.Current;
        if (CurrentFolderEnumerator != null)
        {
            CurrentFolderEnumerator.Dispose();
            CurrentFolderEnumerator = null;
        }

        if (FolderStack.Count > 0)
            CurrentFolderEnumerator = FolderStack.Pop();

        ExitSubFolder(fsi);
    }

    protected virtual void ExitSubFolder(FileSystemInfo fsi)
    {
    }
    #endregion
}
