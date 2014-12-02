KSP Mod Admin v1.4.0 by Bastian Heinrich - A tool to manage the installation and removal of mods.

OK, no rocket science but it speed up the installation or removal of mods and so you will have more time to do rocket science.
It also let you choose which parts of a mod should be installed and it detects mods that are outdated (when downloaded with the internal ModBrowser from KSP Forum or CurseForge). 
A backup handling for easy save game backup and recovery, Part and Craft listing/validation and a Flag manager/importer is included too.
For easy ModList exchange with your friends or the community i have added the Import / Export of ModPacks.


Features:
  - KSP Version independent! (Until they changes mod loading again.)
  - Support of ZIP-, RAR- and 7ZIP- mod archives.
  - Supports multiple installations of KSP. Change between different KSP installations with two clicks.
  - Integrated WebBrowser for direct downloads of mods from Kerbal Spaceport, CurseForge, KSP Forum or other locations with immediately add to the ModSelection.
    (Supports *.zip, *.rar, *.7z and *.craft file downloads. (Craft files will be zipped and added to the ModSelection)
  - Detects outdated Spaceport, CurseForge or KSP Forum mods (displayed in blue) and updates them if you download the new version of the mod.
    (There are several auto update options available like "Remove old and add new mod" or "Copy destination and checked state from old to new mod" ...)
  - Collision detection for mod files (if two or more mods have the same destination for a mod file the user will be asked which he wants to keep).
  - ModPack Import/Export for sharing your mods with others.
  - Flag import and management.
  - Craft validation (Check your installed crafts for missing parts), craft renaming and vehicle building swapping.
  - Easy save game backup and recovery.
  - Auto-backup functionality.
  - AutoUpdate function for the KSP Mod Admin.
  - Add mods or crafts with drag & drop of mod archives, the add dialog (past filepath or url) or with the mod download detection of the integrated ModBrowser.
  - Choose what to include, just by hook or unhook parts of a mod.
  - Detects install folder for the mod (the mod will be installed to the GameData folder).
  - Install or remove mods just with one click.
  - Easy install destination control (for those cases the destination can't be auto detected).
  - Easy source folder control for the mod archive files (if you move the mods elsewhere).
  - Scan KSP folder for installed mods (they will be added to the ModSelection if they aren't listed already).
  - Zip function for mods where the archive file is missing (the zip will be saved to the ModBrowser download folder).
  - Category change for parts and part renaming.
  - Read text files of a mod by double click the files.
  - Search for parts or anything else.
  - Editable Note column on ModSelection, Backup- and Options-Tab.
  - Project code included! (VS2013 Solution - Language: C#)


License:
  - This project (KSP Mod Admin) is published under the CC BY-NC-SA 3.0 DE license (see http://de.creativecommons.org/was-ist-cc/).
    (With this license you are allowed to copy, change and redistribute this work, when 
     - you name me (write a mail to mackerbal@mactee.de to discuss details)
     - the redistribution is non commercial!
     - and your redistribution have the same license)
  - The SharpCompress.dll (see https://sharpcompress.codeplex.com/) is published under the Ms-PL license (see https://sharpcompress.codeplex.com/license).
  - The TreeListView control and all classes in the NameSpace KSPMODAdmin.Utils.CommonTools
    are from the open source project "ListView with Columns" on Code Project. (see http://www.codeproject.com/Articles/23746/TreeView-with-Columns).
    This project is published under The Code Project Open License (CPOL - http://www.codeproject.com/info/cpol10.aspx).
    I've added cell editing and CheckBox support and removed all code i don't need (for a upcoming Mono (http://mono-project.com/Main_Page) support).
    The code i've added is marked by "added by BH" and is under the same license (CPOL).
  - The CheckBoxComboBox control with all its classes are from the open source project "CheckBox ComboBox ..." on Code Project. (see http://www.codeproject.com/Articles/21085/CheckBox-ComboBox-Extending-the-ComboBox-Class-and).
    This project is published under The Code Project Open License (CPOL - http://www.codeproject.com/info/cpol10.aspx).
  - The FolderSelectDialog is from the following Blog entry http://www.lyquidity.com/devblog/?p=136.
    There where no license only the statement that this piece of code is for free use.


Requirements:
  - Windows
  - .Net 4.5 (Web installer included in download or see http://www.microsoft.com/en-US/download/details.aspx?id=30653)


Installation:
  - Install the .Net4.5 framework from Microsoft.
  - Just extract/copy the KSP Mod Admin folder from the zip to any location you want.
  - Start KSP Mod Admin ...


How to use:

General Information:
  - Almost all controls in KSP MA have a tool tip that describes their functionality.
    Please hover (with you mouse) over the control if its use is unclear.
  - You can contact me anytime via mackerbal@mactee.de


Mod Admin general usage:
  - Start KSP Mod Admin 
  - Select a KSP install folder.
  - Press the "Add" button or drag and drop a mod (zip, rar or 7z archive) to add a mod to your selection.
  	  Black node -> Zip-File found and destination folder set - mod is ready for installation.
  	  Grey node -> No destination folder detected to install the mod into (choose a destination manually).  
  	  Yellow node -> Destination of the Mod/ModFile collides with another mods destination.
  	  Red node -> Zip-File not found - mod can only be removed.
  	  Green node -> The folder/file or a sub folder/file is installed ready for remove.
  	  Blue node -> Outdated Mod (Update available).
  - Check destination paths by hovering mouse over the TreeNodes of mod.
    If you find a suspicious path right click the node and select a destination manually (see "Choose a new destination for a mod")
  - Remove the hooks from parts of the mod you may not want (like ReadMe's or source folders).
  - Press the "All Mods" button to let KSP MA install the hooked items and uninstalls the unhooked ones from the KSP install dir (KSP/GameData).
  - Icons with the green "+" symbol indicating that this file/folder of the mod is installed.
  - To remove a mod from the ModSelection just select it and
  	  a) press the "Remove" button (Uninstalls the mod from KSP install dir (KSP/GameData) and removes it from the ModSelection).
  	  b) unhook (unchecked) the Mod and press the "Selected Mod" button (Uninstalls the mod).
  	  c) unhook (unchecked) the Mod and press the "All Mods" button (Uninstalls the mod).


Handle gray colored mods or 
Choose a new destination for a mod:
If you have added a mod where the KSP Mod Admin can't auto detect a folder where the mod should be installed to,
the mod will be displayed in gray. You have to select a source and a destination folder manually.
  - Right click the mod and select Destination -> Select new destination
  - A dialog will pop up to choose a source and a destination folder.
  - Select a source folder from the drop down menu.
    The selected folder/file will be copied to the destination.
  - Select a destination folder from the drop down menu.
    The folder where the content of the source folder or the source folder itself should be installed to.
    (The destination will be set for all sub folders and files of the source folder)
  - Click the OK button and the mod is ready for installation (The node should be displayed black now).
	
Handle blue colored (outdated) mods:
Right click the mod and select "Visit Spaceport" to download the mod manually.
After the download The auto update will perform on of the following 4 options (depending of your settings on the Options Tab).
  - RemoveAndAdd    -> Removes the outdated mod and installs the new one.
  - CopyDestination -> Tries to find matching files and copies their destination and checked state.
  - CopyCheckstate  -> Tries to find matching files and copies their checked state only.
  - Manually        -> Updated mod will just be downloaded to the downloads folder. (No auto update!)
Its possible that you have to select the destinations for the new (updated) mod manually, KSP MA will show a pupup to notify you about this.

Flags general usage:
  - Start KSP Mod Admin and select the "Flags" tab.
  - After a refresh (search of all KSP folders) a list of flags will be displayed.
  - Press "Import new Flag" to select a *.gif/*.png/*.jpg image to import to KSP as a flag.
    The image will be resized to a width of 256px and a height of 160px. (no aspect ratio keeping, for now)
    Then it will be copied to the folder "KSP Install/GameData/MyFlags".
  - Press Delete Flag to remove a flag from the disk.


Backup Helper general usage:
  - Start KSP Mod Admin and select the "Backup" tab.
  - Select a KSP install folder, if not already done.
  - Press the "Browse dir" button (...) and select a backup folder (Folder will be saved for this KSP folder).
  - Press "New" to select a directory to backup or press "Backup saves folder" to backup the saves folder of KSP.
  - To recover a backup select the backup and press "Recover selected Backup".


ModBrowser:
  - Browse normal to the Mod you want.
  - Just click the Download button and wait till download is done.
    After the Download has finished the mod will be added to the Mods Selection.
    (If the download comes from the KSP Spaceport the ModInfos will be added too).
  - Switch to the ModSelection to change the part selection and/or for installation.


Parts Tab:
  - Press refresh to clear the list and scan the GameData folder for parts.
  - Select a part 
  - and press "Rename" to rename a part.
  - or press "Change category" to change the category of the part.
  - The "Remove" button removes the selected part(s).
    This deletes the part from the HD and unchecks it in the ModSelection
    but it can be reinstalled be checking the part again and a click on Selected Mods.


Crafts Tab:
  - Press refresh to clear the list and scan the GameData/Ships folder for parts and crafts and validate the crafts.
  - Select a craft 
  - and press "Rename" to rename a craft.
  - or press "Swap building" to change the start building of the part.
  - The "Validate" button starts the validation check of all crafts (Red crafts have missing parts).
  - The "Remove" button removes the selected craft(s).


Options:
  - Self-explanatory. I hope =)


Mod Update Check:
- How to prepare already installed mods for the update feature:
  If you already have installed mods, you can edit the mod infos with a right click and the "Edit mod infos" button.
  The mod infos edit dialog will open where you can change the mod infos.
  Enter the URL to the Kerbal Spaceport, CurseForge or KSP Forum site of the mod click the corresponding RadioButton 
  and press the button next to the TextBox. KSP MA will get the mod infos for you.
  Now the mod is prepared for the "Mod update feature" :)
  If the mod is blue even if you have the newest mod change the AddDate of the mod to the creation date of the mod.
  (You can enter a URL to another site then KSP Spaceport but then KSP MA won't get the mod infos, 
  but you can visit the site with a right click on the mod and the "Visit website -> Spaceport" option.)

Known issues:
- If you have installed KSP into the Program folder of Windows, KSP MA needs admin rights to manipulate (save/change KSP MA config file).
  You can move your install dir of KSP to another spot or start KSP MA in admin mode with:
    - a right click on the KSPModAdmin.exe and choose "Run as Admin"
  or for a permanent change:
    - right click the KSPModAdmin.exe
    - choose properties
    - choose compatibility
    - check the "Run as Admin" CheckBox at the bottom.
    - press OK.
- Auto path detection fails on some mods.
  Workaround -> Choose the destination path manually.
- There is a problem with the guest account of KSP Spaceport. If you don't login with your created user account the infos of the mod won't be actual!
  They (KSP Spaceport) updates the mod infos you can see with a guest account in a very long interval!
  So it may take a while that KSP MA notices a mod update. (Any ideas how to solve this problem?)
- Responding time of the WebBrowser is horrible =( damn IExplorer control! )
- Folders of mods which create files during usage (like settings- or config-files) wont be deleted.
  (This is not a bug -> this is a feature =)
- Sometimes the Anti-Virus software avast! puts the KSPMA into a Sandbox.
  I think thats because of the search function for known KSP install paths.
  While KSPMA is in the sandbox a new mod install seems to work but its only performed in the sandbox (The mod don't show up in KSP).
  Workaround -> Put KSPMA in the white list of avast.


Special thanks, for bug reports and suggestions, to:
cy-one, Stone Blue, Benzschwagel, TheCardinal, MrHanMan, Meatplow, MorisatoK, Reisu79, Jivaii, hiegova, craigmt1, LeadMagnet, Bergion, 
shadow651, Horman, cpottinger, diomedea, Roger25000, zapman987, Pondafarr, Xetalim, eddyjay, Sangrias, marauder13, TheAlmightyOS, gurgle528,
thisischrys, DianonForce and many more =)


Change Log:

KSP Mod Admin v1.4.0 PR15:
  - FolderConflictDetection on / off option added.
  - Bug "Default naming for ModPacks" fixed -> Thanks to thisischrys!
  - Bug in filename parsing from ResponseHeaders fixed (For direct downloads with ModBrowser).
  - Creation Date and Add Date of added mods (via filepath) are now the same. -> Thanks to DianonForce!

KSP Mod Admin v1.4.0 PR14:
  - FileName parsing errors for CurseForge (caused by changes of the files site of a mod) fixed.
  - Bug in set destination for ModNodes (root) fixed.

KSP Mod Admin v1.4.0 PR13:
  - CurseForge CreationDate parsing bug fixed. -> Thanks to TheAlmightyOS!
  - FolderBrowserDialog changed to FolderSelectDialog -> Thanks to gurgle528!
  - ModSelection column SpaceportURL replaced by CurseForgeURL.

KSP Mod Admin v1.4.0 PR12:
  - KSP 64 bit bug fixed. -> Thanks to marauder13!

KSP Mod Admin v1.4.0 PR11:
  - BUG "CorseForge category change" fixed.
  - ModBrowser Spaceport buttons removed.
  - Start site is now http://kerbal.curseforge.com/projects
  - BUG "ModBrowser window don't show up completely" fixed.

KSP Mod Admin v1.4.0 PR10:
  - URL TextBox added to ModBrowser.
  - BUG "KSP forum site parsing and RegEx matching" fixed.
    When KSP MA don't finds a download link on a KSP Forum site, a new browser will open, where you can browse to the download link, 
	and start the download. KSP MA will notice the file download and copies the URL.
	There are still some problems with mediafire links =(.
  - BUG "KSP Forum download filename parsing problems" fixed.
    Now downloads from DropBox and KSP Spaceport or other sources is possible (from the link selection browser dialog).
	There are still some problems with mediafire links =(.
  - BUG "Outdated marked mods won't get unmarked on AddDate/CreationDate change" fixed.
    When you change the AddDate or Changed date the outdated state of the mod will now be updated.
  - BUG "Mismatching default node colors" fixed.
    Defaults are now "Zip archive missing" = red and "No destination detected" = gray again.
  - BUG "GameData conflicts after sorting" fixed.
    The GameData should no longer be a conflicting folder.
  - BUG "Missing 'Backup started' message on 'Backup saves' button click" fixed.
    Backup is now asynchrony.

KSP Mod Admin v1.4.0 PR9:
  - Export/Import of ModPacks added (to make bug hunting more easy to me ;)) -> inspired by KSPMM.
  - Option Tab:
    - Auto backup options moved to Options tab.
    - Conflict detection On/Off option added.
    - Show conflict solver dialog option added.
    - Node colorization can now be customized.
  - BUG "Version control not set properly when download via ModBrowser" fixed.
  - BUG "ModSelection and ModRegister don't clear properly when a mod was removed" fixed.
  - BUG "LinkSelection Window closes immediately" fixed.

KSP Mod Admin v1.4.0 PR8:
  - Behavior of add button on ModSelection changed:
    Button will now open the add mod dialog. Here you can enter (or paste) a path, a Spaceport URL, CurseForge URL or a KSP Forum URL to a mod.
    KSP MA will then download the mod (if necessary) and adds it to the ModSelection.
  - ModBrowser "Add mod" button added. (Gets ModInfos, downloads and adds the mod from current website).
  - Kerbal CurseForge support:
    - Version checking added.
    - Direct download via add dialog & ModBrowser added.
  - "Show conflicts" button added.
  - "Solve conflicts" button added to the ModSelection context menu.
  - BUG "ModSelection don't clear when last mod was removed" fixed.
  - BUG "Part parsing - Multiple Parts in one part.cfg" fixed. -> Thanks to Pondafarr
  - New Icons

KSP Mod Admin v1.4.0 PR7:
  - Collision detection added.
    Check if two or more mods have the same destination for a file. If a collision is detected the "Collision solving" dialog will pop up,
    where you can chose if the installed files should be kept or if another mod should install the files.
  - Update check for KSP Forum mods added (KSP MA checks the Edit date of the first post of the thread to determine if a mod was updated or not).
    NOTE: Auto Update is NOT available for KSP Forum mods yet -> update those mods manually by removing the old mod and downloading the new one.
  - Saving of column size of ModSelection TreeListView added. -> Thanks to TheCardinal
  - SharpCompress.dll update to v0.10.3.
  - Bug "Part parsing - Missing whitespace between part and {" fixed. -> Thanks to Pondafarr
  - Refactoring

KSP Mod Admin v1.4.0 PR6:
  - Bug "Loading Download path from cfg" fixed. -> Thanks to diomedea
  - Bug "Loading of older KSP MA cfg files" fixed. No more version folder for the KSP MA cfg file! (c:\ProgramData\KSPModAdmin\KSPModAdmin\KSPModAdmin.cfg)
  - Bug "Auto update failed on folders where no matching parent folder was found in outdated mod" fixed. -> Thanks to diomedea
  - Bug "No error logging" fixed. KSP MA will now write all messages to a log file next to the KSPModAdmin.exe (file will be deleted if size is bigger then 2mb).
  - KSP MA will only open one Window. If you double click the exe again, the running KSP MA will become the top most Window.
  - Simplified internal handling of Download and Backup path changes.
  - More messages for the message window.

KSP Mod Admin v1.4.0 PR5:
  - AutoUpdate functionality. There are 4 options to choose from:
    - RemoveAndAdd: Removes the outdated mod and installs the new one.
    - CopyDestination: Try to find matching files and copy their destination and checked state.
    - CopyCheckedState: Try to find matching files and copy their checked state only.
    - Manually: Updated mod will just be downloaded to the downloads folder. (No auto update!)
  - Update ModInfo Button added to ModBrowser ToolBar -> Trys to find a Mod in the ModSelection with the same ProductID as parsed from the current shown WebSite.
  - Second URL added to ModInfo (to save the e.g. Forum URL of a mod).
  - ModSelection context menu option "Visit Forum" added -> Opens the ModBrowser with the URL from the ModInfo field "Forum URL".
  - Internal KSP MA version handling changed to (x.x.x.x) -> now PR versions will recognize official releases with same version number (x.x.x).
  - More message for the info box (bottom area of KSP MA window) added.
  - Bug in part file parsing fixed. -> Thanks to Roger25000
  - Notes will be copied on "ModInfo copy" too. -> Thanks to diomedea
  - NasaMission folder will be ignored on GameData folder scan. -> Thanks to zapman987
  - Some ToolTips and messages updated.

KSP Mod Admin v1.4.0 PR4:
  - Copy ModInfos button added to ModSelection ContextMenu.
    This Button opens a Copy ModInfo dialog to copy the ModInfos from the selected mod to the chosen destination mod.
  - Bug with non Spaceport URLs during the mod update check fixed.
  - Adding of Mods where no RootNode was found bug fixed.

KSP Mod Admin v1.4.0 PR3:
  - Adding of mods with SpaceportID bug fixed.

KSP Mod Admin v1.4.0 PR2:
  - Version compare (KSP MA Update) bug fixed.
  - ModInfo URL validation bug fixed.
  - Mod Update Date compare bug fixed.

KSP Mod Admin v1.4.0 PR:
  - KSP MA auto update (KSP MA can now updates itself, nothing left to do for your ;)).
  - Mod update check implemented.
    KSP MA reads the Spaceport site of each mod (that was downloaded with the ModBrowser or where the Spaceport URL is available) 
    and compares the creation date of the mod with the AddDate of the installed mod. 
    If the AddDate is older then the creation date the mod will show up in blue.
    You can now right click the mod and choose "Visit Spaceport" to download the updated mod.
  - Editor for ModInfos (here you can enter ModInfos manually) added.
  - ModSelection ContectMenu enhancements:
    - Visit Spaceport site of Mod added
    - Edit ModInfo added
    - Zip Creation -> Create Zip with chosen name added

KSP Mod Admin v1.3.11:
  - GUI scaling bug now fixed.
  - Open Folder Button in Context-Menu of ModSelection -> opens the selected installed folder if possible.
  - Adding of *.craft files via add dialog or drag & drop implemented.

KSP Mod Admin v1.3.10:
  - GUI scaling bug "ArgumentOutOfRangeException: Value of '-1' is not valid for 'LargeChange'" fixed -> Thanks to "shadow651", "Bergion" and all who helped with the Bug hunt.
  - Some design time Bugs fixed.

KSP Mod Admin v1.3.9:
  - Bugs during rendering of TreeListView without Visual Styles fixed.
  - Backup-/Download-Path, Crafts-/Parts-Tab clear on KSP Path change.

KSP Mod Admin v1.3.8:
  - "Options & Part Tab: Convert TreeNode to Node exception" bug fixed.
  - "Null Exception on save KSP config, when KSP install path is invalid" bug fixed.
  - Loading of app config changed:
    Skip invalid KSP install path on load of known KSP paths.

KSP Mod Admin v1.3.7:
  - Part parsing redone (support of multiple part declaration in one *.cfg file) -> Thanks to "Jivaii"
  - Launch KSP option to start KSP with a border less window (-popupwindow). -> Thanks to "cy-one"
  - ModSelection:
    - TreeView changed to TreeListView
    - Editable version and note column.
    - Sorting by Name, AddDate, Version
    - Warning on reset/select a destination when node/childes is/are installed.
    - "Expand/collapse all nodes" Bug fixed.
    - "Mod checked state on KSP MA start or KPS path change" bug fixed. -> Thanks to "cy-one"
  - Backup & Options Tab:
    - TreeView changed to TreeListView.
    - Editable note column added.
  - Crafts & Parts Tab:
    - TreeView changed to TreeListView.
  - TreeListView:
    - CheckBox support added
    - Cell editing added
    - Auto column resize added
    - ActionKey handling added
    - Sort on column header click added

KSP Mod Admin v1.3.6:
  - Broken search function on Mod selection repaired
  - Parts Tab: 
    - Multi select and remove now works.
    - Parts will now be sorted by title not name.
    - Category filter works again.
    - Category numbers will be converted in category name.
    - Related crafts are now shown.
    - Warning message when removing a part and it is used by a craft
    - All part files (*.cfg) are scanned now not only the part files with default name (part.cfg).
  - Craft Tab: 
    - Multi select and remove now works.
    - Craft parts now show title not name.
    - Craft parts are sorted now.
    - Required Mods are shown in the Info part of a craft.
    - Parts are listed just once with count of this part.
    - Remove of a craft removes relation on part tab (Bug fixed).
    - Refresh causes a refresh of parts too.

KSP Mod Admin v1.3.5:
  - Show ALL the archive types (.zip, .7z and .rar) in "add mod" folder dialog. -> Thanks to "Stone Blue"
  - "Multiple text dialogs on double click on a part.cfg" bug fixed. -> Thanks to "Stone Blue"
  - "Shows wrong part.cfg on double click" bug fixed.
  - VAB and SPH add to destination drop down in "Destination Selection" dialog.
  - Threading Bug in Parts Tab fixed.
  - Title of Parts are shown now not the name (Title & Name added as sub nodes to the part node)
  - Rearrange of sub nodes on part tab.

KSP Mod Admin v1.3.4:
  - KSP Forum is browsable now. -> Thanks to "cy-one" for the ass kick =)
  - Downloads from other sides than Spaceport is now supported.
  - Open Downloads folder button added to Options & ModBrowser. -> Thanks to "Benzschwagel"
  - Parts & Crafts tab added (Support of renaming, part category changing, craft validating and vehicle building changing).
  - Craft installation improved (now all crafts should be placed in the KSP_InstallPath/Ships/ VAB or SPH folders).
  - Handling of known KSP paths moved to Options tab, to simplify the Welcome view.
  - Mouse thumb buttons mapped to for/backward browsing.

KSP Mod Admin v1.3.3:
  - Prerelease of v1.3.4

KSP Mod Admin v1.3.2:
  - Zip creation for all installed mods.
  - "Auto path detection" Bug fixed (no more additional root folders).
  - "Not deleted mod folder(s)" Bug fixed.

KSP Mod Admin v1.3.1:
  - 7zip & rar support implemented
  - Sort for the mod selection implemented (see ContextMenu)
  - Ships will be installed to "KSP_Install\Ships", only there ships will be loaded from KSP.
    The type of the ship will be auto detected.
  - Automated deletion of older KSPMA config files ("c:\ProgramData\KSPModAdmin\KSPModAdmin\vx.x.x.x\KSPModAdmin.cfg").
  - Mod Selection performance increased
  - "Read Zip Structure" Bug fixed -> Thanks to "MorisatoK" & "cy-one"
  - Flag import works again -> Thanks to "Reisu79"
  - Renew checked state bug fixed (GameData folder got checked even if no child was installed).
  - Flag list don't clears on KSP Path change - fixed

KSP Mod Admin v1.3.0:
  - Scan function for installed mods in the GameData folder.
  - Zip function for installed mods where the zip file is missing.
  - Display of KSP install folder search dialog on startup if KSP install path is empty.
  - Auto backup functions (see cogwheel on backup tab).
  - Better support of multiple installations of KSP.
  - Update message.
  - Key support (del / backspace -> removes mods, backups or KSP paths.)
  - ProgressBar MaxValue Bug (during navigation) fixed.
  - "Position problem when KSPMA was closed when minimized" Bug fixed. -> Thanks to "Meatplow" & "cy-one"
  - "Processing of multiple selected mods" bug fixed. -> Thanks to "cy-one"
  - Recycling bin excluded from KSP install path search -> Thanks to "cy-one"
  - "Drag & Drop of KSP Install folders" bug fixed -> Thanks to "cy-one"

KSP Mod Admin v1.2.6:
  - "Mod selection doesn't clear after KSP install path change" bug fixed. -> Thanks to "cy-one"

KSP Mod Admin v1.2.5:
 Sorry for the last 2 buggy versions. The impact of code refactoring was quiet heavy =(
 But the good news is -> refactoring is done and this version is stabil again. 
  - Another "loading of older configs" bug fixed.
  - Crash during destination selection fixed. -> Thanks to "MrHanMan" & "TheCardinal"!
  - Select destination dialog - filter settings changed when selected node is a file.
  - AutoCheck of nodes that don't have a destination - fixed.
  - Direct download now works again.
  - Backup path selection works again.
  - Recover of free chosen paths to backup works again.

KSP Mod Admin v1.2.4:
  - Loading of older configs bug (v1.2.3 only) fixed.

KSP Mod Admin v1.2.3:
  - Resize bug (crash) fixed. -> Thanks to Dave!

KSP Mod Admin v1.2.2:
  - Support of direct download of *.craft files.
    Choose where to install the craft to (VAB/SPH). 
    A ziped version of the craft will be added to the download dir and to the mod selection tree.
  - Delete a mod flag will remove the file from install path and unchecks the appropriate TreeNode in the mod selection tree.
  - Multi select in mod TreeView implemented.
  - ToolTips added.
  - Website loading progress display added (as long as no download is running otherwise the download progress will be shown).
  - Free folder selection in destination selection dialog implemented (Some mod zip file are not packed in the recommended structure).
  - Saving of Window position should work fine now.
  - Maximized on startup bug fixed.
  - TreeView CheckBox double click bug fixed.
  - No deleting of empty folder bug fixed.
  - "Deleting flag" bug fixed.
  - Refactoring / removing kerbalish code.

KSP Mod Admin v1.2.1:
  - Saving of download path bug fixed.
  - Download of non *.Zip file will show a MessageBox. (*.craft support comes with next version).

KSP Mod Admin v1.2.0:
  - ModBrowser with direct download and adding implemented.
  - Launch button text changed =) -> Special thanks to "TheCardinal" =).
  - Rename problem of installed nodes fixed (simple fix).
  - "Flag import" bug fixed
  - "Remove Mod" bug fixed
  - "Check for Updates on startup" flag bug fixed.

KSP Mod Admin v1.1.1:
  - Delete flags bug fixed

KSP Mod Admin v1.1.0:
  - Flag import and management.
  - Reading and interpreting of zip files improved.
  - Default install folder changed to GameData.
  - Backup creation improved.
  - Destination selection for every folder/file of a mod.
  - Source folder selection, if mod Zip-File folder changes.
  - Threading added for some time consuming tasks.
  - Progress bar added.
  - Node search added.
  - Renaming of mod Zip-Nodes.
  - Mod nodes colorization for better recognizability of the install state.
  - Update functionality for the KSP Mod Admin.
  - Searching for older config files when no current version found.
  - Crash safety increased ;)
