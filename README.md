Phantom Brigade Equipment Type and Rarity Change

# Description
Changes the item display from "Rarity / Item Type" to either "Item Type" or "Item Type / Rarity".

The goal is to make it easier to find sections of the same type of item quickly.

## Original:
![](Media/Original.png)

## Item Type Only:

![](Media/TypeOnly.png)

## Item Type and Rarity:

![](Media/TypeRarity.png)

# Mod Compatibility
This mod is safe to add and remove from existing saves.


# Settings
In the metadata.yaml file, the following settings are available in the modSettings section:


|Setting|Default|Description|
|--|--|--|
|showRarity|false|If true, will show item "type / rarity".  Otherwise only the type is shown.|


# Installation

Warning - Phantom Brigade is in Early Access and my change at any time, which may break this mod.

Additionally any bug reports for modded games will be ignored by the developers. 

## Install the Mod

Download the mod's zip file and extract to the following directory:

``%HOMEDRIVE%%HOMEPATH%\Documents\My Games\Phantom Brigade\Mods\``

## Enable The Mod in Phantom Brigade

If the Mods option does not show up in the game, complete the steps in the [enable mods](#configure-phantom-brigade-to-enable-mods) section.

On the main screen, click the mods icon:

![](Media/ModsMenu.png)

Click the Arrow on the mod to move the mod to the Config section

![](Media/EnableMod.png)

Click the Save Config button

![](Media/SaveConfig.png)

Click the Exit to Desktop button

![](Media/ExitToDesktop.png)

Relaunch the game.

The mod will now be enabled.

## Configure Phantom Brigade to Enable Mods

This section is only required if Phantom Brigade has not already been configured to enable mods.

Create a new mods.yaml file in the folder 
``%HOMEDRIVE%%HOMEPATH%\Documents\My Games\Phantom Brigade\Settings``

in the mod.yaml file, copy the following text and save:

```
enabled: true
loadFromApplicationPath: false
list: []
```

