using HarmonyLib;
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
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            try { 
                Main.modEntry = modEntry;
#if DEBUG
                modEntry.OnUnload = Unload;
#endif
                harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
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
            harmony.UnpatchAll(modEntry.Info.Id);
            return true;
        }
    }
}
