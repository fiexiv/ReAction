using System;
using System.Collections.Generic;
using Dalamud.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ReAction;

public class Configuration : PluginConfiguration, IPluginConfiguration
{
    public class Action
    {
        public uint ID = 0;
        public bool UseAdjustedID = false;
    }

    public class ActionStackItem
    {
        public uint ID = 0;
        public uint TargetID = 10_000;
    }

    public class ActionStack
    {
        public string Name = string.Empty;
        public List<Action> Actions = [];
        public List<ActionStackItem> Items = [];
        public uint ModifierKeys = 0u;
        public bool BlockOriginal = false;
        public bool CheckRange = false;
        public bool CheckCooldown = false;
    }

    public class StackSerializer : DefaultSerializationBinder
    {
        private static readonly Type actionStackType = typeof(ActionStack);
        private static readonly Type actionStackItemType = typeof(ActionStackItem);
        private static readonly Type actionType = typeof(Action);
        private const string actionStackShortName = "s";
        private const string actionStackItemShortName = "i";
        private const string actionShortName = "a";
        private static readonly Dictionary<string, Type> types = new()
        {
            [actionStackType.FullName!] = actionStackType,
            [actionStackShortName] = actionStackType,
            [actionStackItemType.FullName!] = actionStackItemType,
            [actionStackItemShortName] = actionStackItemType,
            [actionType.FullName!] = actionType,
            [actionShortName] = actionType
        };
        private static readonly Dictionary<Type, string> typeNames = new()
        {
            [actionStackType] = actionStackShortName,
            [actionStackItemType] = actionStackItemShortName,
            [actionType] = actionShortName
        };

        public override Type BindToType(string assemblyName, string typeName)
            => types.TryGetValue(typeName, out var t) ? t : base.BindToType(assemblyName, typeName);

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            if (typeNames.TryGetValue(serializedType, out var name))
                typeName = name;
            else
                base.BindToName(serializedType, out assemblyName, out typeName);
        }
    }

    public int Version { get; set; }

    public List<ActionStack> ActionStacks = [];
    public bool EnableEnhancedAutoFaceTarget = false;
    public bool EnableAutoDismount = false;
    public bool EnableGroundTargetQueuing = false;
    public bool EnableInstantGroundTarget = false;
    public bool EnableBlockMiscInstantGroundTargets = false;
    public bool EnableAutoCastCancel = false;
    public bool EnableAutoTarget = false;
    public bool EnableAutoChangeTarget = false;
    public bool EnableSpellAutoAttacks = false;
    public bool EnableSpellAutoAttacksOutOfCombat = false;
    public bool EnableCameraRelativeDashes = false;
    public bool EnableNormalBackwardDashes = false;
    public bool EnableQueuingMore = false;
    public bool EnableFrameAlignment = false;
    public bool EnableAutoRefocusTarget = false;
    public bool EnableMacroQueue = false;
    public bool EnableFractionality = false;
    public bool EnablePlayerNamesInCommands = false;
    public bool EnableQueueAdjustments = false;
    public bool EnableRequeuing = false;
    public bool EnableSlidecastQueuing = false;
    public bool EnableGCDAdjustedQueueThreshold = false;
    public float QueueThreshold = 0.5f;
    public float QueueLockThreshold = 0.5f;
    public float QueueActionLockout = 0f;
    public bool EnableTurboHotbars = false;
    public int TurboHotbarInterval = 400;
    public int InitialTurboHotbarInterval = 0;
    public bool EnableTurboHotbarsOutOfCombat = false;
    public HashSet<uint> TurboHotbarBlacklist = [];
    public bool EnableCameraRelativeDirectionals = false;
    public bool EnableUnassignableActions = false;
    public uint AutoFocusTargetID = 0;
    public bool EnableAutoFocusTargetOutOfCombat = false;

    public bool EnableDecomboLiturgy = false;
    public bool EnableDecomboEarthlyStar = false;
    public bool EnableDecomboMinorArcana = false;
    public bool EnableDecomboFireInRed = false;
    public bool EnableDecomboFire2InRed = false;
    public bool EnableDecomboBlizzardInCyan = false;
    public bool EnableDecomboBlizzard2InCyan = false;
    public bool EnableDecomboManafiction = false;
    public bool EnableDecomboTen_Chi_Jin = false;
    public bool EnableDecomboInner_Release = false;
    public bool EnableDecomboSoulsow = false;
    public bool EnableDecomboReawaken = false;
    public bool EnableDecomboHuntersCoil = true;
    public bool EnableDecomboSwiftskinCoil = true;
    public bool EnableDecomboReavingFang = true;
    public bool EnableDecomboHuntersSting = true;
    public bool EnableDecomboSwiftskinSting = true;
    public bool EnableDecomboFlankStingStrike = true;
    public bool EnableDecomboFlankBaneFang = true;
    public bool EnableDecomboHindStingStrike = true;
    public bool EnableDecomboHindBaneFang = true;
    public bool EnableDecomboSteelFang = true;
    public bool EnableDecomboSteelMaw = true;
    public bool EnableDecomboReavingMaw = true;
    public bool EnableDecomboHuntersBite = true;
    public bool EnableDecomboSwiftskinBite = true;
    public bool EnableDecomboJaggedMaw = true;
    public bool EnableDecomboBloodiedMaw = true;
    public bool EnableDecomboVicepit = true;
    public bool EnableDecomboVicewinder = true;
    public bool EnableDecomboHuntersDen = true;
    public bool EnableDecomboSwiftskinDen = true;
    public bool EnableDecomboAstralFlow = false;
    public bool EnableDecomboHammerStamp = false;
    public bool EnableDecomboSerpentTail = false;
    

    public override void Initialize() { }

    private static readonly StackSerializer serializer = new ();

    private const string exportPrefix = "RE_";

    public static string ExportActionStack(ActionStack stack)
        => Util.CompressString(JsonConvert.SerializeObject(stack, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            SerializationBinder = serializer
        }), exportPrefix);

    public static ActionStack ImportActionStack(string import)
        => JsonConvert.DeserializeObject<ActionStack>(Util.DecompressString(import, exportPrefix), new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            SerializationBinder = serializer
        });
}