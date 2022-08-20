using System.Linq;
using System.Text.RegularExpressions;
using Verse;
using Verse.Grammar;

namespace CaravanLettersHome
{
    public static class LetterWriter
    {
        private const string SPACE = " ";
        private const string COMMA = ",";
        private const string PERIOD = ".";
        private const string EXCLAMATION = "!";

        private static GrammarRequest request = default(GrammarRequest);

        static LetterWriter()
        {
            request.Includes.Add(RulePackDefOf.CaravanLettersHome);
        }

        public static string WriteLetter(string recipientName, string senderName, int opinion)
        {
            string text = "";

            string openingKeyword;
            string introKeyword;
            string sentenceKeyword;
            string outroKeyword;
            string closingKeyword;
            bool interject;

            if (opinion >= 0)
            {
                openingKeyword = "CaravanLettersHome_DearPawn";
                introKeyword = "CaravanLettersHome_NiceIntro";
                sentenceKeyword = "CaravanLettersHome_LetterSentenceNice";
                outroKeyword = "CaravanLettersHome_NiceOutro";
                closingKeyword = "CaravanLettersHome_LovePawn";
            }
            else
            {
                openingKeyword = "CaravanLettersHome_NotDearPawn";
                introKeyword = "CaravanLettersHome_MeanIntro";
                sentenceKeyword = "CaravanLettersHome_LetterSentenceMean";
                outroKeyword = "CaravanLettersHome_MeanOutro";
                closingKeyword = "CaravanLettersHome_HatePawn";
            }

            if (Rand.Value > 0.1f) text += openingKeyword.Translate(recipientName);
            else text += GrammarResolver.Resolve(openingKeyword, request).Formatted(recipientName);
            text += "\n\n";
            if (Rand.Value > (opinion >= 0? 0.5f: 0.7f)) text += GrammarResolver.Resolve(introKeyword, request) + SPACE;
            int numSentences = Rand.RangeInclusive(1, opinion >= 0? 6: 3);
            for (int i = 0; i < numSentences; i++)
            {
                text = AddSentence(text, sentenceKeyword, opinion);
                if (Rand.Value > 0.7f && i < numSentences - 1 && (text.EndsWith(PERIOD + SPACE) || text.EndsWith(EXCLAMATION + SPACE)))
                {
                    text = text.TrimEnd(new[] { SPACE, PERIOD, EXCLAMATION }.Select(s => s.First()).ToArray());
                    text += COMMA + SPACE + GrammarResolver.Resolve("CaravanLettersHome_Conjunction", request, null, false, null, null, null, false) + SPACE;
                    text = AddSentence(text, sentenceKeyword, opinion, false);
                    i++;
                }
            }
            interject = Rand.Value > 0.7f;
            bool outro = Rand.Value > 0.5f;
            if (interject) text += GrammarResolver.Resolve("CaravanLettersHome_Interjection", request) + (outro? COMMA + SPACE: PERIOD);
            if (outro) text += GrammarResolver.Resolve(outroKeyword, request, null, false, null, null, null, !interject);
            text += "\n\n";
            if (Rand.Value > (opinion >= 0? 0.7f: 0.2f)) text += closingKeyword.Translate(senderName);
            else text += GrammarResolver.Resolve(closingKeyword, request).Formatted(senderName);

            text = FixArticleGrammar(text);
            text = FixAdverbSpelling(text);
            text = FixGerundSpelling(text);

            return text;
        }

        private static string AddSentence(string text, string sentenceKeyword, int opinion, bool capitalize = true)
        {
            bool interject = Rand.Value > 0.9f;
            if (interject) text += GrammarResolver.Resolve("CaravanLettersHome_Interjection", request, null, false, null, null, null, capitalize) + COMMA + SPACE;
            if (Rand.Value > (opinion >= 0 ? 0.8f : 0.4f)) text += GrammarResolver.Resolve(sentenceKeyword, request, null, false, null, null, null, !interject && capitalize) + SPACE;
            else text += GrammarResolver.Resolve("CaravanLettersHome_LetterSentenceNeutral", request, null, false, null, null, null, !interject && capitalize) + SPACE;
            return text;
        }

        private static string FixArticleGrammar(string text)
        {
            return Regex.Replace(text, "\\s(a)\\s([aeiou]|y[^aeiouy])", " $1n $2", RegexOptions.IgnoreCase);
        }

        private static string FixAdverbSpelling(string text)
        {
            return Regex.Replace(text, "yly", "ily");
        }

        private static string FixGerundSpelling(string text)
        {
            return Regex.Replace(text, "([^be])eing", "$1ing");
        }
    }
}
