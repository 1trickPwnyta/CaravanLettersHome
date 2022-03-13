using Verse;
using Verse.Grammar;

namespace CaravanLettersHome
{
    public static class LetterWriter
    {
        public static string WriteLetter(string recipientName, string senderName, int opinion)
        {
            GrammarRequest request = default(GrammarRequest);
            request.Includes.Add(RulePackDefOf.CaravanLettersHome);
            string space = " ";
            string comma = ",";
            string period = ".";

            string text = "";
            bool interject = false;

            if (opinion >= 0)
            {
                if (Rand.Value > 0.1f) text += "CaravanLettersHome_DearPawn".Translate(recipientName);
                else text += GrammarResolver.Resolve("CaravanLettersHome_DearPawn", request).Formatted(recipientName);
                text += "\n\n";
                if (Rand.Value > 0.5f) text += GrammarResolver.Resolve("CaravanLettersHome_NiceIntro", request) + space;
                int numSentences = Rand.RangeInclusive(1, 6);
                for (int i = 0; i < numSentences; i++)
                {
                    interject = Rand.Value > 0.9f;
                    if (interject) text += GrammarResolver.Resolve("CaravanLettersHome_Interjection", request) + comma + space;
                    if (Rand.Value > 0.8f) text += GrammarResolver.Resolve("CaravanLettersHome_LetterSentenceNice", request, null, false, null, null, null, !interject) + space;
                    else text += GrammarResolver.Resolve("CaravanLettersHome_LetterSentenceNeutral", request, null, false, null, null, null, !interject) + space;
                }
                interject = Rand.Value > 0.7f;
                bool outro = Rand.Value > 0.5f;
                if (interject) text += GrammarResolver.Resolve("CaravanLettersHome_Interjection", request) + (outro? comma + space: period);
                if (outro) text += GrammarResolver.Resolve("CaravanLettersHome_NiceOutro", request, null, false, null, null, null, !interject);
                text += "\n\n";
                if (Rand.Value > 0.7f) text += "CaravanLettersHome_LovePawn".Translate(senderName);
                else text += GrammarResolver.Resolve("CaravanLettersHome_LovePawn", request).Formatted(senderName);
            }
            else
            {
                if (Rand.Value > 0.1f) text += "CaravanLettersHome_NotDearPawn".Translate(recipientName);
                else text += GrammarResolver.Resolve("CaravanLettersHome_NotDearPawn", request).Formatted(recipientName);
                text += "\n\n";
                if (Rand.Value > 0.7f) text += GrammarResolver.Resolve("CaravanLettersHome_MeanIntro", request) + space;
                int numSentences = Rand.RangeInclusive(1, 3);
                for (int i = 0; i < numSentences; i++)
                {
                    interject = Rand.Value > 0.9f;
                    if (interject) text += GrammarResolver.Resolve("CaravanLettersHome_Interjection", request) + comma + space;
                    if (Rand.Value > 0.4f) text += GrammarResolver.Resolve("CaravanLettersHome_LetterSentenceMean", request, null, false, null, null, null, !interject) + space;
                    else text += GrammarResolver.Resolve("CaravanLettersHome_LetterSentenceNeutral", request, null, false, null, null, null, !interject) + space;
                }
                interject = Rand.Value > 0.7f;
                bool outro = Rand.Value > 0.5f;
                if (interject) text += GrammarResolver.Resolve("CaravanLettersHome_Interjection", request) + (outro ? comma + space : period);
                if (outro) text += GrammarResolver.Resolve("CaravanLettersHome_MeanOutro", request, null, false, null, null, null, !interject);
                text += "\n\n";
                if (Rand.Value > 0.2f) text += senderName;
                else text += GrammarResolver.Resolve("CaravanLettersHome_HatePawn", request).Formatted(senderName);
            }

            return text;
        }
    }
}
