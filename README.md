Wiimote Presenter PowerPoint Addin
==================================

Work in progress

Key mapping not yet implemented

Next slide and previous slide are the only actions implemented so far

Control slideshows wirelessly from a Wii Remote on a Bluetooth-enabled Windows PC. PowerPoint 2007+ and .NET 4.0


![Screenshot](http://i.imgur.com/Vr6mxfS.jpg)

## Save your powerpoint slideshow before operating this Addin ##
Please save your precious work before running! Many things can go wrong.
e.g the PowerPoint COM API throws an error (OK so far), handling if the Wiimote suddenly disconnects (todo, will crash atm!), bluetooth switched off, etc.

Only the first Wiimote is utilised if multiple are connected.

## Attributions ##
- Brian Peek's **WiimoteLib** http://wiimotelib.codeplex.com/
- In The Hand **32feet.NET** http://32feet.codeplex.com/ for Bluetooth pairing
- http://www.malteahrens.com/#/blog/howto-onenote-dev/ - pointers on using WiX to create the installer
