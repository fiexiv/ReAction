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
        
        Hammer_Stamp = 34678,
        
        Hunters_Coil = 34621,
        Swiftskin_Coil = 34622,
        Steel_Fang = 34606,
        Reaving_Fangs = 34607,
        Hunters_Sting = 34608,
        Swiftskins_Sting = 34609,
        Flanksting_Strike = 34610,
        Flanksbane_Fang = 34611,
        Hindsting_Strike = 34612,
        Hindsbane_Fang = 34613,
        Steel_Maw = 34614,
        Reaving_Maw = 34615,
        Hunters_Bite = 34616,
        Swiftskins_Bite = 34617,
        Jagged_Maw = 34618,
        Bloodied_Maw = 34619,
        Vicewinder = 34620,
        VicePit = 34623,
        Hunters_Den = 34624,
        Swiftskin_Den = 34625,
        SerpentTail = 35920,
        
        Astral_Flow = 25822,
        
        Quietus = 7391,
        Bloodspiller = 7392,
        Scarlet_Delirium = 36928,
        Comeuppance = 36929,
        
        Fang_And_Claw = 3554,
        Wheeling_Thrust = 3556,
        
        Savage = 16147,
        Gnashing = 16146,
        Bloodfest = 16164,
        ReignOfBeasts = 36937,
        
        OgiNamikiri = 25781,
        
        Jolt = 37004,
        Impact = 16526,
        Moulinet = 7513,
        Verthunder2 = 16524,
        Veraero2 = 16525
        
        
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
                                         || ReAction.Config.EnableDecomboAstralFlow
                                         || ReAction.Config.EnableDecomboHuntersCoil
                                         || ReAction.Config.EnableDecomboSwiftskinCoil
                                         || ReAction.Config.EnableDecomboReavingFang
                                         || ReAction.Config.EnableDecomboSteelFang
                                         || ReAction.Config.EnableDecomboHuntersSting
                                         || ReAction.Config.EnableDecomboSwiftskinSting
                                         || ReAction.Config.EnableDecomboFlankStingStrike
                                         || ReAction.Config.EnableDecomboFlankBaneFang
                                         || ReAction.Config.EnableDecomboHindBaneFang
                                         || ReAction.Config.EnableDecomboHindStingStrike
                                         || ReAction.Config.EnableDecomboSteelMaw
                                         || ReAction.Config.EnableDecomboReavingMaw
                                         || ReAction.Config.EnableDecomboHuntersBite
                                         || ReAction.Config.EnableDecomboSwiftskinBite
                                         || ReAction.Config.EnableDecomboJaggedMaw
                                         || ReAction.Config.EnableDecomboBloodiedMaw
                                         || ReAction.Config.EnableDecomboVicepit
                                         || ReAction.Config.EnableDecomboVicewinder
                                         || ReAction.Config.EnableDecomboHuntersDen
                                         || ReAction.Config.EnableDecomboSwiftskinDen
                                         || ReAction.Config.EnableDecomboHammerStamp
                                         || ReAction.Config.EnableDecomboSerpentTail
                                         || ReAction.Config.EnableDecomboBloodspiller
                                         || ReAction.Config.EnableDecomboScarletDelirium
                                         || ReAction.Config.EnableDecomboComeuppance
                                         || ReAction.Config.EnableDecomboQuietus
                                         || ReAction.Config.EnableDecomboFangAndClaw
                                         || ReAction.Config.EnableDecomboWheelingThrust
                                         || ReAction.Config.EnableDecomboGnashing
                                         || ReAction.Config.EnableDecomboSavage
                                         || ReAction.Config.EnableDecomboBloodfest
                                         || ReAction.Config.EnableDecomboVerAoE
                                         || ReAction.Config.EnableDecomboOgiNamikiri
                                         || ReAction.Config.EnableDecomboReignOfBeasts;

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
            case ActionID.Hunters_Coil when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Swiftskin_Coil when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Reaving_Fangs when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Steel_Fang when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Hunters_Sting when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Swiftskins_Sting when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Flanksting_Strike when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Flanksbane_Fang when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Hindsting_Strike when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Hindsbane_Fang when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            
            case ActionID.Steel_Maw when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Reaving_Maw when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Hunters_Bite when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Swiftskins_Bite when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Jagged_Maw when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Bloodied_Maw when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.VicePit when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Vicewinder when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Hunters_Den when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            case ActionID.Swiftskin_Den when ReAction.Config.EnableDecomboViperGCD:
                return actionID;
            
            
            case ActionID.Astral_Flow when ReAction.Config.EnableDecomboAstralFlow:
                return actionID;
            
            case ActionID.Hammer_Stamp when ReAction.Config.EnableDecomboHammerStamp:
                return actionID;
            
            case ActionID.SerpentTail when ReAction.Config.EnableDecomboSerpentTail:
                return actionID;
            
            case ActionID.Bloodspiller when ReAction.Config.EnableDecomboBloodspiller:
                return actionID;
            
            case ActionID.Scarlet_Delirium when ReAction.Config.EnableDecomboScarletDelirium:
                return actionID;
            
            case ActionID.Comeuppance when ReAction.Config.EnableDecomboComeuppance:
                return actionID;
            
            case ActionID.Quietus when ReAction.Config.EnableDecomboQuietus:
                return actionID;
            
            
            case ActionID.Fang_And_Claw when ReAction.Config.EnableDecomboFangAndClaw:
                return actionID;
            
            case ActionID.Wheeling_Thrust when ReAction.Config.EnableDecomboWheelingThrust:
                return actionID;

            case ActionID.Gnashing when ReAction.Config.EnableDecomboGnashing:
                return actionID;
            
            case ActionID.Savage when ReAction.Config.EnableDecomboSavage:
                return actionID;
            
            case ActionID.Bloodfest when ReAction.Config.EnableDecomboBloodfest:
                return actionID;
            
            case ActionID.ReignOfBeasts when ReAction.Config.EnableDecomboReignOfBeasts:
                return actionID;
            
            case ActionID.Verthunder2 when ReAction.Config.EnableDecomboVerAoE:
                return actionID;
            
            case ActionID.Veraero2 when ReAction.Config.EnableDecomboVerAoE:
                return actionID;
            
            case ActionID.OgiNamikiri when ReAction.Config.EnableDecomboOgiNamikiri:
                return actionID;
            
            default:
                return ret;
        }

    }
}