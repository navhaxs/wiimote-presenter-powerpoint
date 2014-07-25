Wiimote Presenter PowerPoint Addin
==================================

Work in progress

Control slideshows wirelessly from a Wii Remote on a Bluetooth-enabled Windows PC. PowerPoint 2007+ and .NET 4.0

## Debugging ##
- VS proj is set up to launch PowerPoint for debugging (hit F5)
- Adjust path to POWERPNT.exe for your installed version if neccessary
- To COM register dll, either (1) Tick Register for COM interop in Project Settings > Compile (must always run VS as admin)
- or (2) Build DLL then run "register <your arch>.bat" in debug-tools (just first time)

## Building installer ##
- Install WiX toolset if creating installer

## Attributions ##
- Brian Peek's **WiimoteLib** http://wiimotelib.codeplex.com/
- In The Hand **32feet.NET** http://32feet.codeplex.com/ for Bluetooth pairing
- http://www.malteahrens.com/#/blog/howto-onenote-dev/ - pointers on using WiX to create the installer