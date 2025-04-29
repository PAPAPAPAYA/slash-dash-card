using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoIndicatorScript : MonoBehaviour
{
	void Update()
	{
		transform.position = PlayerControlScript.me.transform.position;
		GetComponent<TextMeshPro>().text = ""+PlayerControlScript.me.ammo; // get TextMeshPro when it's 3D text
	}
}
