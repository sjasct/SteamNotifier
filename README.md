# SteamNotifier
Lightweight background program to notify you when Steam has started downloading something since Steam cannot be arsed doing it

[**Download the latest version**](https://github.com/Avinch/SteamNotifier/releases/latest)


![An image showing a sample notification](http://i.imgur.com/lUDQ3LE.png)


### How to use
**Installation**  
Extract the contents of `SteamNotifier-vx.x.x.zip` into your favourite folder.  

**Starting**  
Run the `SteamNotifier.exe` file.  
 
**Adding to / removing from startup**  
Click on the SteamNotifier icon in the system tray or run the `SN-Utility.exe` file. Once the window is open, check/uncheck the "Run on startup" option.  

**Stopping SteamNotifier**  
Click on the SteamNotifier icon in the system tray or run the `SN-Utility.exe` file. Once the window is open click "Stop SteamNotifier". After that is done, you can close that window. Alternatively, you can stop the Steam Notifier process through Task Manager if you know how to.  

**Uninstalling**  
Delete the SteamNotifier folder.  

**Reporting bugs**  
You can report bugs via GitHub. Simply [open a new issue](https://github.com/Avinch/SteamNotifier/issues/new) and explain the problem. It would be great if you could include anything in the `debug.log` file and the version of Windows you are running.  

### Dependencies
- Newtonsoft.Json (v10.0.3)

### Stuff from other repos / projects
- RegistryMonitor
  - Thomas Freudenberg via CodeProject
  - https://www.codeproject.com/Articles/4502/RegistryMonitor-a-NET-wrapper-class-for-RegNotifyC
  
- [benjibobs/Steam-Shutdown](https://github.com/benjibobs/Steam-Shutdown/)
  - [updateCheck method](https://github.com/benjibobs/Steam-Shutdown/blob/efd55ef02012178735099e4dfe9e9497d8112a9d/Steam%20Shutdown/SShutdown.cs#L262) (modified)
