using Dalamud.Hooking;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace ReAction.Modules;

public unsafe class Decombos : PluginModule
{
    private enum ActionID : uint
    {
        Liturgy_of_the_Bell = 25862,

        Earthly_Star = 7439,

        Fire_in_Red = 34650,
        Blizzard_in_Cyan = 34653,
        Fire_II_in_Red = 34656,
        Blizzard_II_in_Cyan = 34659,
        
        Manafiction = 7521,
        
        Ten_Chi_Jin = 7403,
        
        Inner_Release = 7389,
        
        Soulsow = 24387,
        
        Reawaken = 34626,
        
        Astral_Flow = 25822
    }

    public override bool ShouldEnable => ReAction.Config.EnableDecomboLiturgy
                                         || ReAction.Config.EnableDecomboEarthlyStar
                                         || ReAction.Config.EnableDecomboMinorArcana
                                         || ReAction.Config.EnableDecomboFireInRed
                                         || ReAction.Config.EnableDecomboFire2InRed
                                         || ReAction.Config.EnableDecomboBlizzardInCyan
                                         || ReAction.Config.EnableDecomboManafiction
                                         || ReAction.Config.EnableDecomboTen_Chi_Jin
                                         || ReAction.Config.EnableDecomboInner_Release
                                         || ReAction.Config.EnableDecomboSoulsow
                                         || ReAction.Config.EnableDecomboReawaken
                                         || ReAction.Config.EnableDecomboBlizzard2InCyan
                                         || ReAction.Config.EnableDecomboAstralFlow;

    protected override void Enable() => GetAdjustedActionIdHook.Enable();
    protected override void Disable() => GetAdjustedActionIdHook.Disable();

    private delegate ActionID GetAdjustedActionIdDelegate(ActionManager* actionManager, ActionID actionID);
    [HypostasisClientStructsInjection(typeof(ActionManager.MemberFunctionPointers), Required = true, EnableHook = false)]
    private static Hook<GetAdjustedActionIdDelegate> GetAdjustedActionIdHook;
    private static ActionID GetAdjustedActionIdDetour(ActionManager* actionManager, ActionID actionID)
    {
        var ret = GetAdjustedActionIdHook.Original(actionManager, actionID);

        switch (actionID)
        {
            case ActionID.Liturgy_of_the_Bell when ReAction.Config.EnableDecomboLiturgy:
                return actionID;

            case ActionID.Earthly_Star when ReAction.Config.EnableDecomboEarthlyStar:
                return actionID;

            case ActionID.Fire_in_Red when ReAction.Config.EnableDecomboFireInRed:
                return actionID;
            case ActionID.Fire_II_in_Red when ReAction.Config.EnableDecomboFire2InRed:
                return actionID;
            case ActionID.Blizzard_in_Cyan when ReAction.Config.EnableDecomboBlizzardInCyan:
                return actionID;
            case ActionID.Blizzard_II_in_Cyan when ReAction.Config.EnableDecomboBlizzard2InCyan:
                return actionID;
            case ActionID.Manafiction when ReAction.Config.EnableDecomboManafiction:
                return actionID;
            case ActionID.Ten_Chi_Jin when ReAction.Config.EnableDecomboTen_Chi_Jin:
                return actionID;
            case ActionID.Inner_Release when ReAction.Config.EnableDecomboInner_Release:
                return actionID;
            case ActionID.Soulsow when ReAction.Config.EnableDecomboSoulsow:
                return actionID;
            case ActionID.Reawaken when ReAction.Config.EnableDecomboReawaken:
                return actionID;
            case ActionID.Astral_Flow when ReAction.Config.EnableDecomboAstralFlow:
                return actionID;
            
            default:
                return ret;
        }
    }
}