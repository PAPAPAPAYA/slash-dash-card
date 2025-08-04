using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadnessEffect : MonoBehaviour
{
	public IntVaribaleSO madnessCounterRef;
	public void AddMadness()
	{
		madnessCounterRef.value++;
	}
	// todo: need testing
	public void AddMadnessAccordingToCurseInGrave()
	{
		var tagAmount = 0;
		foreach (var card in CardManagerNew.me.grave)
		{
			if (card.GetComponent<CardScript>().myTags.Contains(EnumStorage.Tag.curse))
			{
				tagAmount++;
			}
		}
		madnessCounterRef.value += tagAmount;
	}
	public void ApplyMadnessToDmg()
	{
		GetComponent<CardScript>().dmg += madnessCounterRef.value;
	}
	public void ResetMadness()
	{
		madnessCounterRef.value = 0;
	}
	public void LoadResetMadness()
	{
		LingerEffectManager.me.onLastHand.RemoveListener(ResetMadness);
		LingerEffectManager.me.onLastHand.AddListener(ResetMadness);
	}
}
