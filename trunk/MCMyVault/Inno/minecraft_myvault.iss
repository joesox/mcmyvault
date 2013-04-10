; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "MCMyVault"
#define MyAppVersion "1.5.1.5"
#define MyAppPublisher "JPSIII"
#define MyAppExeName "MCMyVault.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{71164BBE-128A-46E4-9A77-89766875C4C6}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
InfoBeforeFile=C:\Users\Joe\Documents\Visual Studio 2012\Projects\MCMyVault\MCMyVault\ReadMe.txt
OutputBaseFilename=MCMyVaultsetup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; 
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\Users\Joe\Documents\Visual Studio 2012\Projects\MCMyVault\MCMyVault\bin\Release\MCMyVault.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Joe\Documents\Visual Studio 2012\Projects\MCMyVault\MCMyVault\bin\Release\Ionic.Zip.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Joe\Documents\Visual Studio 2012\Projects\MCMyVault\MCMyVault\ReadMe.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Joe\Documents\MCMyVault\*"; DestDir: "{userdocs}\{#MyAppName}"; Flags: ignoreversion recursesubdirs createallsubdirs
;Source: "C:\Users\Joe\Documents\MCMyVault\eihort-0.3.11-win32"; DestDir: "{userdocs}\{#MyAppName}"; Flags: ignoreversion recursesubdirs createallsubdirs 
;Source: "C:\Users\Joe\Documents\MCMyVault\eihort-0.3.11-win64"; DestDir: "{userdocs}\{#MyAppName}"; Flags: ignoreversion recursesubdirs createallsubdirs


[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: files; Name: "{app}\config.ini"
Type: files; Name: "{app}\Log.txt"


