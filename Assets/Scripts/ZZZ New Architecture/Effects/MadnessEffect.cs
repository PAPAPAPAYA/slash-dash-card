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
	public void ApplyMadnessToDmg()
	{
		transform.GetComponent<CardScript>().dmg += madnessCounterRef.value;
	}
	public void ResetMadness()
	{
		madnessCounterRef.value = 0;
	}
	public void LoadResetMadness()
	{
		LingerEffectManager.me.OnLastHand.RemoveListener(ResetMadness);
		LingerEffectManager.me.OnLastHand.AddListener(ResetMadness);
	}
}
