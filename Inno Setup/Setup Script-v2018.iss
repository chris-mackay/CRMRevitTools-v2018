#define MyAppName "CRMRevitTools-v2018"
#define MyAppVersion "1.0.0"
#define MyVersionInfoVersion "1.0.0"
#define MyAppPublisher "Christopher Ryan Mackay"

[Setup]
AppId={{F25C3FCA-7E73-49ED-96BB-7C5A67820FA6}
AppName={#MyAppName}
AppCopyright=Copyright © 2018 Christopher Ryan Mackay
AppVersion={#MyAppVersion}
VersionInfoVersion={#MyVersionInfoVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={userdocs}\{#MyAppName}
DisableDirPage=yes
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=.
OutputBaseFilename=CRMRevitTools-v2018-v{#MyAppVersion} Setup
Compression=lzma
SolidCompression=yes
LicenseFile=LICENSE.txt
PrivilegesRequired=lowest

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "CRMRevitTools\Commands\*"; DestDir: "{userdocs}\CRMRevitTools\v2018\Commands"; Flags: ignoreversion
Source: "CRMRevitTools\MenuCreator\*"; DestDir: "{userdocs}\CRMRevitTools\v2018\MenuCreator"; Flags: ignoreversion
Source: "CRMRevitTools\RevitIcons\16x16\*"; DestDir: "{userdocs}\CRMRevitTools\v2018\RevitIcons\16x16\"; Flags: ignoreversion
Source: "CRMRevitTools\RevitIcons\32x32\*"; DestDir: "{userdocs}\CRMRevitTools\v2018\RevitIcons\32x32\"; Flags: ignoreversion
Source: "CRMRevitTools\Addin File\*"; DestDir: "C:\ProgramData\Autodesk\Revit\Addins\2018"; Flags: ignoreversion
Source: "CRMRevitToolsInit-v2018.exe"; DestDir: "{userdocs}\CRMRevitTools\v2018"; Flags: ignoreversion
Source: "LICENSE.txt"; DestDir: "{userdocs}\CRMRevitTools\v2018"; Flags: ignoreversion

;CRMRevitTools_Help
Source: "..\CRMRevitTools_Help\*"; DestDir: "{userdocs}\CRMRevitTools\v2018\CRMRevitTools_Help"; Flags: ignoreversion
Source: "..\CRMRevitTools_Help\css\*"; DestDir: "{userdocs}\CRMRevitTools\v2018\CRMRevitTools_Help\css"; Flags: ignoreversion

;Create Revit Sheets
Source: "..\CRMRevitTools_Help\images\create_revit_sheets\*"; DestDir: "{userdocs}\CRMRevitTools\v2018\CRMRevitTools_Help\images\create_revit_sheets"; Flags: ignoreversion

;Shared Parameter Creator
Source: "..\Parameter_Template-v2018.xlsx"; DestDir: "{userdocs}\CRMRevitTools\v2018\"; Flags: ignoreversion
Source: "..\CRMRevitTools_Help\images\shared_parameter_creator\*"; DestDir: "{userdocs}\CRMRevitTools\v2018\CRMRevitTools_Help\images\shared_parameter_creator"; Flags: ignoreversion

;Sheet Renamer
Source: "..\CRMRevitTools_Help\images\sheet_renamer\*"; DestDir: "{userdocs}\CRMRevitTools\v2018\CRMRevitTools_Help\images\sheet_renamer"; Flags: ignoreversion

[UninstallDelete] 
Type: dirifempty; Name: {userdocs}\CRMRevitTools;

[Run]
Filename: {userdocs}\CRMRevitTools\v2018\CRMRevitToolsInit-v2018.exe;

