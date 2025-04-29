using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerScript : MonoBehaviour
{
	public bool mouseDown = false;
	public Vector2 mousePos = Vector2.zero;
	public Vector2 mouseDownPos = Vector2.zero;
	public Vector2 mouseDragDir = Vector2.zero;
	public float mouseDragDuration = 0; // how long mouse held down
	private PlayerControlScript pcs;
	private CardManager handMan;
	#region SINGLETON
	public static InputManagerScript me;
	private void Awake()
	{
		me = this;
	}
	#endregion
	void Start()
	{
		pcs = PlayerControlScript.me;
		handMan = CardManager.me;
	}
	
	void Update()
	{
		// mouse status tracking
		mouseDown = Input.GetMouseButton(0); // updates if dragging
		mousePos = Input.mousePosition; // updates mouse pos
		if (Input.GetMouseButtonDown(0)) // mouse down frame
		{
			mouseDownPos = mousePos;
			mouseDragDuration = 0;
		}
		if (mouseDown) // mouse hold
		{
			mouseDragDuration += Time.deltaTime;
		}
		if (Input.GetMouseButtonUp(0)) // mouse up frame
		{
			mouseDragDuration = 0;
			if (pcs.chargeCompleted 
			//&& !pcs.moving
			)
			{
				if (handMan.reloaded)
				{
					pcs.Slash(mouseDragDir);
				}
				else
				{
					pcs.Dash(mouseDragDir);
				}
			}
		}
		mouseDragDir = (mouseDownPos - mousePos).normalized;
	}
}
