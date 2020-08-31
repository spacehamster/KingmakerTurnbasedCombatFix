using HarmonyLib;
using ModMaker;
using System;
using System.Reflection;
using UnityModManagerNet;

namespace TurnbasedCombatFix
{
#if DEBUG
    [EnableReloading]
#endif
    public class Main
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void Log(string msg)
        {
            modEntry?.Logger?.Log(msg);
        }
        public static void Error(Exception ex)
        {
            modEntry?.Logger?.Error(ex.ToString());
        }
        public static void Error(string err)
        {
            modEntry?.Logger?.Error(err);
        }
        public static bool enabled;
        public static UnityModManager.ModEntry modEntry;
        public static Harmony harmony;
        public static ModManager<Core, Settings> Mod;
        public static MenuManager Menu;
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            try {
                Main.modEntry = modEntry;
                Mod = new ModManager<Core, Settings>();
                Menu = new MenuManager();
                modEntry.OnToggle = OnToggle;
#if DEBUG
                modEntry.OnUnload = Unload;
#endif
            }
            catch (Exception ex)
            {
                Error(ex);
                throw;
            }
            return true;
        }
        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            //harmony.UnpatchAll(modEntry.Info.Id);
            return true;
        }
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            if (value)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Mod.Enable(modEntry, assembly);
                Menu.Enable(modEntry, assembly);
            }
            else
            {
                Menu.Disable(modEntry);
                Mod.Disable(modEntry, false);
            }
            return true;
        }
    }
}
