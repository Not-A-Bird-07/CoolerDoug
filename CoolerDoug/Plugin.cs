using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace CoolerDoug
{
    [BepInPlugin(Plugin_Info.GUID,  Plugin_Info.NAME, Plugin_Info.VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;

        public void Awake()
        {
            Instance = this;
            new Harmony(Plugin_Info.GUID).PatchAll(Assembly.GetExecutingAssembly());

            GorillaTagger.OnPlayerSpawned(Init);
        }
        //should run when the player spawns, but this is inconsistent
        public void Init()
        {

        }
    }
}
