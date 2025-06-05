using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager me;

	[Header("SCORE")]
	public GameObject scorePrefab;
	public int score = 0;
	public float score_spawnForce;
	public int upgradeAmount; // score needed to upgrade
	public float upgradeAmount_scaler; // each time player upgrades, upgrade amount is scaled

	[Header("UIs")]
	public GameObject hpIndicator;
	public GameObject scoreIndicator;
	public string hpText;
	public string scoreText;
	[Header("DEBUG")]
	public bool showKnifeReport;
	public bool CardSystemActivated;
	private void Awake()
	{
		me = this;
	}
	private void Update()
	{
		hpIndicator.GetComponent<TextMeshProUGUI>().text = hpText + PlayerControlScript.me.hp;
		scoreIndicator.GetComponent<TextMeshProUGUI>().text = scoreText + score + "/" + upgradeAmount;
		CheckUpgrade();
	}
	
	private void CheckUpgrade()
	{
		if (score >= upgradeAmount)
		{
			score  -= upgradeAmount;
			upgradeAmount  = (int)(upgradeAmount_scaler * upgradeAmount);
			CardObtainManager.me.ShowCardOptions();
			//UpgradeInteractionManager.me.ShowButtons();
		}
	}
}