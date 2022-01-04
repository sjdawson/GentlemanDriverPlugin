!define PRODUCT_NAME "sjdawson.GentlemanDriverPlugin"
!define PRODUCT_VERSION "v1.0.2"
!define PRODUCT_PUBLISHER "sjdawson"
!define PRODUCT_WEB_SITE "https://sjdawson.github.io/GentlemanDriverPlugin"

Function .onInit
    ReadRegStr $INSTDIR HKCU "SOFTWARE\SimHub" InstallDirectory
FunctionEnd

SetCompressor lzma

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "bin/Release/assets/helmet.ico" ; @TODO new icon
!define MUI_WELCOMEPAGE_TITLE "${PRODUCT_NAME} Setup"
!define MUI_WELCOMEPAGE_TEXT "Welcome to the setup of ${PRODUCT_NAME} ${PRODUCT_VERSION} for SimHub."
!define MUI_FINISHPAGE_TITLE "Setup complete for ${PRODUCT_NAME}"
!define MUI_FINISHPAGE_TEXT "Setup has finished installing ${PRODUCT_NAME}, you'll need to restart SimHub if it's currently running in order to have it pick up that the plugin has been installed."
!define MUI_DIRECTORYPAGE_TEXT_DESTINATION "Choose your SimHub folder (where SimHubWPF.exe is located)."

!define MUI_FINISHPAGE_SHOWREADME "https://sjdawson.github.io/GentlemanDriverPlugin/"
!define MUI_FINISHPAGE_SHOWREADME_TEXT "Open documentation and changelog?"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!insertmacro MUI_PAGE_FINISH

; Language files
!insertmacro MUI_LANGUAGE "English"

; Reserve files
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "bin/Release/sjdawson.GentlemanDriverPluginInstall.exe"
ShowInstDetails show

Section "MainSection" SEC01
  SetOutPath $INSTDIR
  SetOverwrite ifnewer
  File "..\..\sjdawson.GentlemanDriverPlugin.dll"
  ; Add the various static information files
  SetOutPath "$INSTDIR\PluginsData\F12020\sjdawson.GentlemanDriverPlugin\"
  File "..\..\PluginsData\F12020\sjdawson.GentlemanDriverPlugin\ActualTyreCompound.json"
  File "..\..\PluginsData\F12020\sjdawson.GentlemanDriverPlugin\VisualTyreCompound.json"
SectionEnd

Section -Post
SectionEnd
