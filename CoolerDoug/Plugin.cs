using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace CoolerDoug;
[BepInPlugin(Plugin_Info.GUID, Plugin_Info.NAME, Plugin_Info.VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static Plugin Instance;
    public Animator animator;

    private void Awake()
    {
        Instance = this;
        GorillaTagger.OnPlayerSpawned(Init);
        new Harmony(Plugin_Info.GUID).PatchAll(Assembly.GetExecutingAssembly());
    }

    private void Init()
    {   //this is the model of new doug
        GameObject betterDoug = GameObject.Find("Environment Objects/LocalObjects_Prefab/City_WorkingPrefab/CosmeticsRoomAnchor/nicegorillastore_prefab/nicegorillastore_Layout_2_prefab/PromotionsPrefab/MakeshipDigicoolDougPromoStand/Animated Open and Flying");
        GameObject oldDoug = GameObject.Find("Floating Bug Holdable");//this works due to sheer luck the bug i want to find is first in the hierarchy
        GameObject newDoug = Instantiate(betterDoug, oldDoug.transform, false);
        
        animator = newDoug.GetComponent<Animator>();
        oldDoug.GetComponent<ThrowableBug>().animator = animator;
        oldDoug.transform.GetChild(0).gameObject.SetActive(false);//this is the model of old doug
        
        newDoug.transform.localPosition = new Vector3(0f, -0.25f, 0f);
        newDoug.transform.localRotation = Quaternion.identity;
        newDoug.transform.localScale = Vector3.one * 1.7f;
    }
}

[HarmonyPatch(typeof(ThrowableBug))]
public class Patch
{
    [HarmonyPatch("LateUpdateShared"), HarmonyPostfix]
    public static void LateUpdateSharedPatch(ThrowableBug __instance)
    {
        if (__instance.animator != Plugin.Instance.animator) return;
        
        bool held = __instance.currentState is TransferrableObject.PositionState.InLeftHand or TransferrableObject.PositionState.InRightHand;
            
        __instance.animator.CrossFade(held ? "DigiCoolDoug_InHand" : "DigiCoolDoug_Hover", 0.15f);
    }
}