using System;
using System.Linq;
using CaravanLettersHome;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace RimWorld
{
	public class IncidentWorker_CaravanLetterHome : IncidentWorker
	{
		protected override bool CanFireNowSub(IncidentParms parms)
		{
			bool anyPawnInCaravanRelatedtoAnyHomeMapPawn = (parms.target is Caravan && PawnsFinder.HomeMaps_FreeColonistsSpawned.Any(
				pawn => pawn.needs.mood.thoughts.memories.Memories.Any(memory => (parms.target as Caravan).PawnsListForReading.Contains(memory.otherPawn))
			));
			return anyPawnInCaravanRelatedtoAnyHomeMapPawn;
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Caravan caravan = parms.target as Caravan;
			if (caravan == null)
            {
				CaravanLettersHome.Debug.Log("caravan not found");
				return false;
            }

			Pawn recipient = PawnsFinder.HomeMaps_FreeColonistsSpawned.Where(
				pawn => pawn.needs.mood.thoughts.memories.Memories.Any(m => caravan.PawnsListForReading.Contains(m.otherPawn))
			).RandomElementWithFallback(null);
			if (recipient == null)
            {
				CaravanLettersHome.Debug.Log("recipient not found");
				return false;
            }

			Thought_Memory memory = recipient.needs.mood.thoughts.memories.Memories.Where(
				m => caravan.PawnsListForReading.Contains(m.otherPawn)
			).RandomElementByWeightWithFallback(m => Mathf.Abs(m.otherPawn.relations.OpinionOf(recipient)), null);
			if (memory == null)
            {
				CaravanLettersHome.Debug.Log("sender not found");
				return false;
            }
			Pawn sender = memory.otherPawn;

			string letterText = GetLetterText(recipient, sender);
			base.SendStandardLetter(this.def.letterLabel, letterText, GetLetterDef(recipient, sender), parms, recipient, Array.Empty<NamedArgument>());

			if (recipient.needs.mood != null)
			{
				recipient.needs.mood.thoughts.memories.TryGainMemory(GetThought(recipient, sender));
			}

			return true;
		}

		private LetterDef GetLetterDef(Pawn recipient, Pawn sender)
        {
			return sender.relations.OpinionOf(recipient) < 0 ? LetterDefOf.NegativeEvent : LetterDefOf.PositiveEvent;
        }

		private string GetLetterText(Pawn recipient, Pawn sender)
        {
			int opinion = sender.relations.OpinionOf(recipient);
			return string.Format(this.def.letterText, recipient.Name.ToStringShort, sender.Name.ToStringShort, 
				LetterWriter.WriteLetter(recipient.Name.ToStringShort, sender.Name.ToStringShort, opinion), 
				opinion < 0? "CaravanLettersHome_IsAngry".Translate(): "CaravanLettersHome_AppreciatesTheThought".Translate(), 
				opinion < 0? "CaravanLettersHome_Suffer".Translate() : "CaravanLettersHome_Enjoy".Translate(),
				opinion < 0 ? "CaravanLettersHome_Penalty".Translate() : "CaravanLettersHome_Boost".Translate());
        }

		private Thought_Memory GetThought(Pawn recipient, Pawn sender)
        {
			if (sender.relations.OpinionOf(recipient) < 0)
            {
				return ThoughtMaker.MakeThought(CaravanLettersHome.ThoughtDefOf.ReceivedCaravanLetterMean, null);
            }
			else
            {
				return ThoughtMaker.MakeThought(CaravanLettersHome.ThoughtDefOf.ReceivedCaravanLetterNice, null);
			}
        }
	}
}
