using RimWorld;

namespace CaravanLettersHome
{
	[DefOf]
    public static class ThoughtDefOf
    {
		static ThoughtDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ThoughtDefOf));
		}

		public static ThoughtDef ReceivedCaravanLetterNice;

		public static ThoughtDef ReceivedCaravanLetterMean;
	}
}
