using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OMI_AOE_ProposedMacroAndShortcut : MonoBehaviour
{

    public  ShortCutNameToAction[] m_default = new ShortCutNameToAction[]{
    new ShortCutNameToAction("SelectMilitaryInScreen","Select All Military in Screen","sc: Alt+Comma"),
    new ShortCutNameToAction("GoToArchery","Go to Archery Range","sc: CTRL+A"),
    new ShortCutNameToAction("GoToBarracks","Go to Barracks","sc: CTRL+B"),
    new ShortCutNameToAction("GoToForge","Go to Blacksmith","sc: CTRL+S"),
    new ShortCutNameToAction("GoToCastle","Go to Castle","sc: CTRL+V"),
    new ShortCutNameToAction("GoToDock","Go to Dock","sc: CTRL+D"),
    new ShortCutNameToAction("GoToKrepost","Go to Krepost","sc: CTRL+T"),
    new ShortCutNameToAction("GoToLumber","Go to Lumber Camp","sc: CTRL+Z"),
    new ShortCutNameToAction("GoToMarket","Go to Market","sc: CTRL+M"),
    new ShortCutNameToAction("GoToMill","Go to Mill","sc: CTRL+I"),
    new ShortCutNameToAction("GoToMiningCamp","Go to Mining Camp","sc: CTRL+G"),
    new ShortCutNameToAction("GoToMonastery","Go to Monastery","sc: CTRL+Y"),
    new ShortCutNameToAction("GoToSiege","Go to Siege Workshop","sc: CTRL+K"),
    new ShortCutNameToAction("GoToStable","Go to Stable ","sc: CTRL+L"),
    new ShortCutNameToAction("GoToUniversity","Go to University","sc: CTRL+U"),
    new ShortCutNameToAction("GoToTown","Go to Town","sc: H"),
    new ShortCutNameToAction("CreateOneAntiArcher","Go to ","sc: Shift+ CTRL+ A ⌛30 W↓ W↑"),
    new ShortCutNameToAction("CreateOneAntiHorse","Go to ","sc: Shift+CTRL+B ⌛30 W↓ W↑"),
    new ShortCutNameToAction("CreateOneFoodHorse","Go to ","sc: Shift+CTRL+L ⌛30 Q↓ Q↑"),
    new ShortCutNameToAction("CreateGroupAntiArcher","Go to ","sc: Shift+ CTRL+ A ⌛100 Shift+W "),
    new ShortCutNameToAction("CreateGroupAntiHorse","Go to ","sc: Shift+CTRL+B  ⌛100 Shift+W "),
    new ShortCutNameToAction("CreateGroupFoodHorse","Go to ","sc: Shift+CTRL+L  ⌛100 Shift+Q "),
    new ShortCutNameToAction("CheatSpeedBuild","Go to ","sc: Enter↓ ⌛30 [[rowshep]] ⌛30 Enter↑"),
    new ShortCutNameToAction("CheatResources","Go to ","sc: Enter↓ ⌛30 [[aegis]] ⌛30 Enter↑"),
    new ShortCutNameToAction("CheatMap","Go to ","sc: Enter↓ ⌛30 [[marco]] ⌛30 Enter↑ ⌛30 Enter↓ ⌛30 [[polo]] ⌛30 Enter↑ "),
    new ShortCutNameToAction("SetRallyArchery","Go to Archery Range","sc: Shift+CTRL+A RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyBarracks","Go to Barracks","sc: Shift+CTRL+B RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyForge","Go to Blacksmith","sc: Shift+CTRL+S RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyCastle","Go to Castle","sc: Shift+CTRL+V RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyDock","Go to Dock","sc: Shift+TRL+D RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyKrepost","Go to Krepost","sc: Shift+CTRL+T RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyLumber","Go to Lumber Camp","sc: Shift+CTRL+Z RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyMarket","Go to Market","sc: Shift+CTRL+M RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyMill","Go to Mill","sc: Shift+CTRL+I RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyMiningCamp","Go to Mining Camp","sc:Shift+ CTRL+G RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyMonastery","Go to Monastery","sc: Shift+CTRL+Y RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallySiege","Go to Siege Workshop","sc:Shift+ CTRL+K RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyStable","Go to Stable ","sc:Shift+ CTRL+L RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyUniversity","Go to University","sc: Shift+CTRL+U RightClick↓ RightClick↑"),
    new ShortCutNameToAction("SetRallyTown","Go to Town","sc: Shift+H RightClick↓ RightClick↑"),
    new ShortCutNameToAction("CreateGroupCastle","Go to ","sc: Shift+CTRL+V  ⌛100 Shift+Q "),


    new ShortCutNameToAction("SellFood","Market Sell food ","sc: Ctrl+Shift+M  D↓  D↑"),
    new ShortCutNameToAction("SellWood","Market Sell wood ","sc: Ctrl+Shift+M  S↓  S↑"),
    new ShortCutNameToAction("SellStone","Market Sell stone ","sc: Ctrl+Shift+M  F↓  F↑"),
    new ShortCutNameToAction("BuyFood","Market buy food ","sc: Ctrl+Shift+M  C↓  C↑"),
    new ShortCutNameToAction("BuyWood","Market buy wood ","sc: Ctrl+Shift+M  X↓  X↑"),
    new ShortCutNameToAction("BuyStone","Market buy stone ","sc: Ctrl+Shift+M  V↓  V↑"),

    new ShortCutNameToAction("SellSeveralFood","Market Sell food x5","sc: Ctrl+Shift+M  Shift+D"),
    new ShortCutNameToAction("SellSeveralWood","Market Sell wood x5","sc: Ctrl+Shift+M   Shift+S"),
    new ShortCutNameToAction("SellSeveralStone","Market Sell stone x5","sc: Ctrl+Shift+M   Shift+F"),
    new ShortCutNameToAction("BuySeveralFood","Market buy food x5","sc: Ctrl+Shift+M   Shift+C"),
    new ShortCutNameToAction("BuySeveralWood","Market buy wood x5","sc: Ctrl+Shift+M   Shift+X"),
    new ShortCutNameToAction("BuySeveralStone","Market buy stone x5","sc: Ctrl+Shift+M   Shift+V"),

    new ShortCutNameToAction("UpgradeVillagerStrengh","Research Loom","sc: Ctrl+Shift+H  A↓ A↑"),
    new ShortCutNameToAction("UpgradeVillagerCarry","Research Wheelbarrow","sc: Ctrl+Shift+H  S↓ S↑"),
    new ShortCutNameToAction("UpgradeSight","Research Town Watch","sc: Ctrl+Shift+H  D↓ D↑"),

    new ShortCutNameToAction("UpgradeRam","Research Capped ram","sc: Ctrl+Shift+K  A↓ A↑"),
    new ShortCutNameToAction("UpgradeMangonel","Research Mangonel ","sc: Ctrl+Shift+K  S↓ S↑"),
    new ShortCutNameToAction("UpgradeScorpion","Research Scropion","sc: Ctrl+Shift+K  D↓ D↑"),

    new ShortCutNameToAction("UpgradeBuildingStrenght","Research Masonry","sc: Ctrl+Shift+U  Q↓ Q↑"),
    new ShortCutNameToAction("UpgradeVillagerBuildSpeed","Research ","sc: Ctrl+Shift+U  W↓ W↑"),
    new ShortCutNameToAction("UpgradeTowerFireArrow","Research Heated Shot","sc: Ctrl+Shift+U  E↓ E↑"),
    new ShortCutNameToAction("UpgradeMissilesAccuracy","Research Ballistics","sc: Ctrl+Shift+U  R↓ R↑"),
    new ShortCutNameToAction("UpgradeChemistry","Research Chemistry ","sc: Ctrl+Shift+U  T↓ T↑"),
    new ShortCutNameToAction("UpgradeSiegeRange","Research ","sc: Ctrl+Shift+U  A↓ A↑"),
    new ShortCutNameToAction("UpgradeTowerNoMinimum","Research Murder Holes","sc: Ctrl+Shift+U  S↓ S↑"),
    new ShortCutNameToAction("UpgradeWallStenght","Research Fortified wall","sc: Ctrl+Shift+U  D↓ D↑"),
    new ShortCutNameToAction("UpgradeTower","Research Guard tower","sc: Ctrl+Shift+U  F↓ F↑"),
    new ShortCutNameToAction("UpgradeTowerAttack","Research Arrowslits","sc: Ctrl+Shift+U  G↓ G↑"),

    new ShortCutNameToAction("UpgradeMonkBuilding","Research ","sc: Ctrl+Shift+Y  W↓ W↑"),
    new ShortCutNameToAction("UpgradeMonkConverMonk","Research ","sc: Ctrl+Shift+Y  E↓ E↑"),
    new ShortCutNameToAction("UpgradeMonkSpeed","Research ","sc: Ctrl+Shift+Y  R↓ R↑"),
    new ShortCutNameToAction("UpgradeMonkHitPoint","Research ","sc: Ctrl+Shift+Y  A↓ A↑"),
    new ShortCutNameToAction("UpgradeMonkConvertResistance","Research ","sc: Ctrl+Shift+Y  S↓ S↑"),
    new ShortCutNameToAction("UpgradeMonkRegenSpeed","Research ","sc: Ctrl+Shift+Y  D↓ D↑"),
    new ShortCutNameToAction("UpgradeMonkRange","Research ","sc: Ctrl+Shift+Y  F↓ F↑"),
    new ShortCutNameToAction("UpgradeMonkKillConverted","Research ","sc: Ctrl+Shift+Y  Z↓ Z↑"),
    new ShortCutNameToAction("UpgradeMonkOneByGroup","Research ","sc: Ctrl+Shift+Y  X↓ X↑"),
    new ShortCutNameToAction("UpgradeBuildingHeal","Research ","sc: Ctrl+Shift+Y  C↓ C↑"),

    new ShortCutNameToAction("UpgradeCastleUnity","Research ","sc: Ctrl+Shift+V  A↓ A↑"),
    new ShortCutNameToAction("UpgradeCivSpecialityFeodal","Research civilization feodal ","sc: Ctrl+Shift+V  S↓ S↑"),
    new ShortCutNameToAction("UpgradeCivSpecialityImperial","Research civilisation imperial ","sc: Ctrl+Shift+V  D↓ D↑"),
    new ShortCutNameToAction("UpgradeCastleHitPoint","Research ","sc: Ctrl+Shift+V  Z↓ Z↑"),
    new ShortCutNameToAction("UpgradeVillageBuildingAttack","Research ","sc: Ctrl+Shift+V  X↓ X↑"),
    new ShortCutNameToAction("UpgradeMilitaryRespawn","Research Unity Spawn Faster","sc: Ctrl+Shift+V  C↓ C↑"),
    new ShortCutNameToAction("UpgradeSpy","Research ","sc: Ctrl+Shift+V  V↓ V↑"),

    new ShortCutNameToAction("UpgradeTraderSpeed","Research ","sc: Ctrl+Shift+M  W↓ W↑"),
    new ShortCutNameToAction("UpgradeMarketTribute","Research ","sc: Ctrl+Shift+M  E↓ E↑"),
    new ShortCutNameToAction("UpgradeMarketFee","Research ","sc: Ctrl+Shift+M  R↓ R↑"),


    new ShortCutNameToAction("UpgradeArcherAim","Research ","sc: Ctrl+Shift+A  Z↓ Z↑"),
    new ShortCutNameToAction("UpgradeArcher","Research ","sc: Ctrl+Shift+A  A↓ A↑"),
    new ShortCutNameToAction("UpgradeSkirmisher","Research ","sc: Ctrl+Shift+A  S↓ S↑"),
    new ShortCutNameToAction("UpgradeCavaleryArcher","Research ","sc: Ctrl+Shift+A  D↓ D↑"),
    new ShortCutNameToAction("UpgradeCavaleryArcherStat","Research Parthian Tactics","sc: Ctrl+Shift+A  C↓ C↑"),


    new ShortCutNameToAction("UpgradeMeleeAttack","","sc: Ctrl+Shift+S  Q↓ Q↑"),
    new ShortCutNameToAction("UpgradeInfantryArmor","","sc: Ctrl+Shift+S  W↓ W↑"),
    new ShortCutNameToAction("UpgradeCavalryArmor","","sc: Ctrl+Shift+S  E↓ E↑"),
    new ShortCutNameToAction("UpgradeArcherRange","","sc: Ctrl+Shift+S  A↓ A↑"),
    new ShortCutNameToAction("UpgradeArcherArmor","","sc: Ctrl+Shift+S  S↓ S↑"),


    new ShortCutNameToAction("UpgradeFarm","","sc: Ctrl+Shift+I  Q↓ Q↑"),
    new ShortCutNameToAction("SwitchSeeding","","sc: Ctrl+Shift+I  T↓ T↑"),
    new ShortCutNameToAction("SeedFarm","","sc: Ctrl+Shift+I  R↓ R↑"),

    new ShortCutNameToAction("UpgradeSoldier","","sc: Ctrl+Shift+B  A↓ A↑"),
    new ShortCutNameToAction("UpgradePike","","sc: Ctrl+Shift+B  S↓ S↑"),
    new ShortCutNameToAction("UpgradeInfantryFoodCost","","sc: Ctrl+Shift+B  Z↓ Z↑"),
    new ShortCutNameToAction("UpgradeInfantrySpeed","","sc: Ctrl+Shift+B  X↓ X↑"),
    new ShortCutNameToAction("UpgradeInfantryDamageBuilding","","sc: Ctrl+Shift+B  X↓ X↑"),
    new ShortCutNameToAction("UpgradeEagle","","sc: Ctrl+Shift+B  R↓ R↑"),


    new ShortCutNameToAction("UpgradeWood","","sc: Ctrl+Shift+Z  Q↓ Q↑"),
    new ShortCutNameToAction("UpgradeStone","","sc: Ctrl+Shift+G  W↓ W↑"),
    new ShortCutNameToAction("UpgradeGold","","sc: Ctrl+Shift+G  Q↓ Q↑"),
    new ShortCutNameToAction("UpgradeFood","","sc: Ctrl+Shift+I  Q↓ Q↑"),




    new ShortCutNameToAction("Flare","","sc: Alt+F"),
    new ShortCutNameToAction("MapCombat","","sc: Alt+C"),
    new ShortCutNameToAction("MapNormal","","sc: Alt+N"),
    new ShortCutNameToAction("MapResource","","sc: Alt+R"),


    new ShortCutNameToAction("AIBuildMarket","","sc: Enter↓ ⌛30 [[53]] ⌛30 Enter↑ ⌛200 Alt+F "),
    new ShortCutNameToAction("AIBuildFortification","","sc: Enter↓ ⌛30 [[50]] ⌛30 Enter↑ ⌛200 Alt+F "),
    new ShortCutNameToAction("AIForwardBase","","sc: Enter↓ ⌛30 [[49]] ⌛30 Enter↑ ⌛200 Alt+F "),
    new ShortCutNameToAction("AIPleaseHelp","","sc: Enter↓ ⌛30 [[48]] ⌛30 Enter↑  "),
    new ShortCutNameToAction("AIAttackHere","","sc: Enter↓ ⌛30 [[47]] ⌛30 Enter↑ ⌛200 Alt+F "),
    new ShortCutNameToAction("AIWhereIsArmy","","sc: Enter↓ ⌛30 [[46]] ⌛30 Enter↑  "),
    new ShortCutNameToAction("AIFightWithMe","","sc:  Enter↓ ⌛30 [[51]] ⌛30 Enter↑"),
    new ShortCutNameToAction("AIBuildWallBetween","","sc:Enter↓ ⌛30 [[53]] ⌛30 Enter↑"),
    new ShortCutNameToAction("AINeedFood","","sc:Enter↓ ⌛30 [[3]] ⌛30 Enter↑"),
    new ShortCutNameToAction("AINeedStone","","sc:Enter↓ ⌛30 [[6]] ⌛30 Enter↑"),
    new ShortCutNameToAction("AINeedWood","","sc:Enter↓ ⌛30 [[4]] ⌛30 Enter↑"),
    new ShortCutNameToAction("AINeedGold","","sc:Enter↓ ⌛30 [[5]] ⌛30 Enter↑"),
    new ShortCutNameToAction("AINeedExtraResources","","sc:Enter↓ ⌛30 [[38]] ⌛30 Enter↑"),
    new ShortCutNameToAction("AIBuildWonder","","sc:Enter↓ ⌛30 [[37]] ⌛30 Enter↑"),
    new ShortCutNameToAction("AIWhatStrategy","","sc:Enter↓ ⌛30 [[43]] ⌛30 Enter↑"),
    new ShortCutNameToAction("AIResourceCount","","sc:Enter↓ ⌛30 [[44]] ⌛30 Enter↑"),

    new ShortCutNameToAction("ChatYes","","sc:Enter↓ ⌛30 [[1]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatNo","","sc:Enter↓ ⌛30 [[2]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatAh","","sc:Enter↓ ⌛30 [[7]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatOh","","sc:Enter↓ ⌛30 [[8]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatLOL","","sc:Enter↓ ⌛30 [11]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatBeRush","","sc:Enter↓ ⌛30 [[12]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatDontPoint","","sc:Enter↓ ⌛30 [[15]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatLongTimeNoSiege","","sc:Enter↓ ⌛30 [[19]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatNiceTown","","sc:Enter↓ ⌛30 [[21]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatQuitTouching","","sc:Enter↓ ⌛30 [[22]] ⌛30 Enter↑"),
    new ShortCutNameToAction("Chat2H","","sc:Enter↓ ⌛30 [[27]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatWololo","","sc:Enter↓ ⌛30 [[30]] ⌛30 Enter↑"),
    new ShortCutNameToAction("ChatBox","","sc: F7↓ F7↑"),









    };

    public  GroupOfAOEAction[] m_groupOfActinon = new GroupOfAOEAction[] {
        new GroupOfAOEAction("AllCheatCode"){
            m_actions= new GroupOfAOEAction.TimeAction[] {
                new GroupOfAOEAction.TimeAction(0, "CheatSpeedBuild"),
                new GroupOfAOEAction.TimeAction(200, "CheatResources"),
                new GroupOfAOEAction.TimeAction(300, "CheatMap")
            }
        },
        new GroupOfAOEAction("SetMilitaryRally"){
            m_actions= new GroupOfAOEAction.TimeAction[] {
                new GroupOfAOEAction.TimeAction(0, "")
            }
        }

    };
    [System.Serializable]
    public class GroupOfAOEAction {
        public string m_actionNameId;
        public TimeAction[] m_actions;
        public GroupOfAOEAction(string nameId) {
            m_actionNameId = nameId;
        }




        public class TimeAction {
            public uint m_whenInMs;
        public string m_action;

        public TimeAction(uint whenInMs, string action)
        {
            m_whenInMs = whenInMs;
            m_action = action;
        }
    }
    }


    public  void AppendCommands(string id, ref List<string> commandsAsShortcut)
    {
        List<string> d = new List<string>();
        GetCommands(id, out d);
        commandsAsShortcut.AddRange(d);
    }

    public  void GetCommands(string id, out List<string> commandsAsShortcut)
    {
        commandsAsShortcut = new List<string>();
        string cmd;
        bool found;
        FindSingleCommand(id, out cmd, out found);

        if (found) { 
            commandsAsShortcut.Add(cmd);
        }
    }

    public  void FindSingleCommand(string id, out string cmd, out bool found)
    {
        found = false;
        cmd = "";
        id = id.ToLower();
        for (int i = 0; i < m_default.Length; i++)
        {
            if (id == m_default[i].m_name.ToLower()) {
                cmd = m_default[i].m_shortcut;
                found = true;
                return; 
            }
        }
    }

    public enum ClassicUnityType { Archer, Skirmisher, CavaleryArcher , Swordsman, Pikeman,LightCavalery, Knight , BatteringRam,Mangonel,Scorpion,Bombard,SiegeTower,Trebuchet, Petard, CastelElite, TradeCart, Monk, MissionaryMonk, Villager, Eagle, Camel, Elephant,SteppeLancer}
    public enum BuildingTYpe { Archery, Barracks, Blacksmith, Castle, Dock, Kerpost, Lumber, Market, Mill, MiningCamp, Monastery, Siege, Stable, University, Town }

   // ➤ ☗ | ↓ ↑ ♦ ⌛30

    public string SelectGroup(uint i)
    {
        if (i < 1) return "";
        if (i < 10) return string.Format("sc:{0}↓ {0}↑", i);
        return string.Format("sc:Alt↓ {0}↓ {0}↑ Alt↑", i - 10);
    }
    public string CreateGroup(uint i)
    {
        if (i < 1) return "";
        if (i < 10) return string.Format("sc: Ctrl↓ {0}↓ {0}↑ Ctrl↑", i);
        return string.Format("sc: Ctrl↓ Alt↓ {0}↓ {0}↑ Alt↑ Ctrl↑", i - 10);
    }
    public string AddGroup(uint i)
    {
        if (i < 1) return "";
        if (i < 10) return string.Format("sc: Shift↓ {0}↓ {0}↑ Shift↑", i);
        return string.Format("sc: Shift↓ Alt↓ {0}↓ {0}↑ Alt↑ Shift↑", i - 10);
    }

    [System.Serializable]
    public class ShortCutNameToAction {
        public string m_description;
        public string m_name;
        public string m_shortcut;

        public ShortCutNameToAction(string name,string description, string shortcut)
        {
            m_name = name;
            m_description = description;
            m_shortcut = shortcut;
        }
    }
}
