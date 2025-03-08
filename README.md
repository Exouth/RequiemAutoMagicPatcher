# RequiemAutoMagicPatcher

**An automatic Synthesis patcher for Requiem that balances magic mods using statistically derived values.**

## Overview
RequiemAutoMagicPatcher is a tool for **Skyrim SE**, built with **Mutagen Synthesis**, that automatically balances magic-related mechanics in **Requiem**.

This patcher aggregates data from multiple manually patched **Requiem magic mods**, primarily those using **[Requiem - Magic Redone](https://www.nexusmods.com/skyrimspecialedition/mods/59302)** patches. By analyzing and averaging key values across various patches, the system uses those values to patch your selected magic mods.

While the patcher can be used **without** Requiem - Magic Redone, it is **highly recommended** to use it for the most consistent balance across all magic mods.

## Features
- **Automatically applies balance adjustments using statistical calculations**  
- **Dynamically modifies values based on aggregated data from multiple patches**  
- **Full customization – Every value can be manually adjusted in the settings**
- **CSV Report Feature – Generates before/after records of all changes**

## Customization
- **Manually select which ESP mods should be patched**
- **Adjust nearly every value**
- **Enable or disable specific patching features (books, spells, flags)**
- **CSV Report Feature**
  - All modified records are logged in a CSV file, showing their **before and after values**. - 
  - The report is saved in the **Synthesis folder**, under `RequiemAutoMagicPatcher`, with a separate file for each selected mod.

## Installation
1. Download & install Synthesis
2. Enter the URL of this Github Repository
3. Confirm and add the patcher to your Synthesis setup
4. Run the patcher to apply the changes to your selected mods.  

## Requirements
- **Skyrim SE / AE**
- **[Synthesis](https://github.com/Mutagen-Modding/Synthesis)**
- **[Requiem - The Roleplaying Overhaul](https://www.nexusmods.com/skyrimspecialedition/mods/60888)**
- **.NET 6.0 or later**

### Recommended
- **[Requiem - Magic Redone](https://www.nexusmods.com/skyrimspecialedition/mods/59302)** (for the best balance)

This patcher was originally created because I played a **modlist** where the magic system was based on **Requiem - Magic Redone**, rather than the default Requiem magic.
Since I wanted to add **many custom magic mods** while keeping them balanced within that modlist, I developed this patcher.
It **derives values from various Requiem - Magic Redone patches**, aggregating them to maintain consistency and ensure compatibility across different magic mods.
While the patcher **can be used without Requiem - Magic Redone**, having it installed ensures that everything remains **fully balanced** as originally intended.