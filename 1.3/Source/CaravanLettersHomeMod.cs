using Verse;

namespace CaravanLettersHome
{
    public class CaravanDetectedDontCareMod : Mod
    {
        public const string PACKAGE_ID = "caravanlettershome.1trickPwnyta";
        public const string PACKAGE_NAME = "Caravan Letters Home";

        public CaravanDetectedDontCareMod(ModContentPack content) : base(content)
        {
            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
