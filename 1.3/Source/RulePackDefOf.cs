using RimWorld;
using Verse;

namespace CaravanLettersHome
{
	[DefOf]
	public static class RulePackDefOf
	{
		static RulePackDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(RulePackDefOf));
		}

		public static RulePackDef CaravanLettersHome;
	}
}
