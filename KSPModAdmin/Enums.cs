namespace KSPModAdmin
{
    /// <summary>
    /// Enum of possible node types.
    /// </summary>
    public enum NodeType
    {
        ZipRoot,
        KSPFolder,
        KSPFolderInstalled,
        UnknownFolder,
        UnknownFolderInstalled,
        UnknownFile,
        UnknownFileInstalled
    }

    /// <summary>
    /// Enum of possible ksp paths.
    /// </summary>
    public enum KSP_Paths
    {
        KSPRoot,
        KSPExe,
        KSP64Exe,
        AppConfig,
        KSPConfig,
        Saves,
        Parts,
        Plugins,
        PluginData,
        Resources,
        Ships,
        VAB,
        SPH,
        Internals,
        KSPData,
        GameData
    }

    /// <summary>
    /// Enum of possible sort types.
    /// </summary>
    public enum SortType
    {
        // Sort by name
        ByName = 0,

        // Sort by added date
        ByAddDate,

        // Sort by state
        ByState,

        // Sort by Version
        ByVersion
    }

    /// <summary>
    /// Possible actions after a download of KSPModAdmin.
    /// </summary>
    public enum PostDownloadAction
    {
        Ignore,
        Ask,
        AutoUpdate
    }

    /// <summary>
    /// The possible intervals of mod updating.
    /// </summary>
    public enum ModUpdateInterval
    {
        Manualy = 0,
        OnStartup = 1,
        OnceADay = 2,
        EveryTwoDays = 3,
        OnceAWeek = 4
    }

    public enum ModUpdateBehavior
    {
        RemoveAndAdd = 0,
        CopyDestination = 1,
        CopyCheckedState = 2,
        Manualy = 3
    }
}
