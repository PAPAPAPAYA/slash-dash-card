using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInteractionManager : MonoBehaviour
{
	#region SINGLETON
	public static UpgradeInteractionManager me;
	private void Awake()
	{
		me = this;
	}
	#endregion
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public List<GameObject> availableUpgrades;
	private AbilityManagerScript ams;

	void Start()
	{
		ams = AbilityManagerScript.me;
	}

	public void ShowButtons()
	{
		Time.timeScale = 0;
		button1.SetActive(true);
		button2.SetActive(true);
		button3.SetActive(true);
		ProcessButton();
	}
	private void ProcessButton()
	{
		List<GameObject> shuffledList = UtilityFuncManagerScript.me.ShuffleList(availableUpgrades); // shuffle the ability list
		// randomly assign ability to button
		DetectUpgrade(shuffledList[0].GetComponent<AbilityContainerScript>(), button1.GetComponent<Button>());
		DetectUpgrade(shuffledList[1].GetComponent<AbilityContainerScript>(), button2.GetComponent<Button>());
		DetectUpgrade(shuffledList[2].GetComponent<AbilityContainerScript>(), button3.GetComponent<Button>());
	}
	private void DetectUpgrade(AbilityContainerScript acs, Button button) // assign event to button based on the ability container assigned to the button
	{
		button.onClick.AddListener(() => AbilityManagerScript.me.GainUpgrade(acs.myAbility));
		button.onClick.AddListener(() => HideButtons());
		button.GetComponentInChildren<TextMeshProUGUI>().text = acs.abilityName;
	}
	private void HideButtons()
	{
		button1.GetComponent<Button>().onClick.RemoveAllListeners();
		button2.GetComponent<Button>().onClick.RemoveAllListeners();
		button3.GetComponent<Button>().onClick.RemoveAllListeners();
		button1.SetActive(false);
		button2.SetActive(false);
		button3.SetActive(false);
		Time.timeScale = 1f;
	}
}


