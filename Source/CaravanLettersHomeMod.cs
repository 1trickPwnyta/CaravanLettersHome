using Verse;
using HarmonyLib;

namespace CaravanLettersHome
{
    public class CaravanDetectedDontCareMod : Mod
    {
        public const string PACKAGE_ID = "caravanlettershome.1trickPwnyta";
        public const string PACKAGE_NAME = "Caravan Letters Home";

        public CaravanDetectedDontCareMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
