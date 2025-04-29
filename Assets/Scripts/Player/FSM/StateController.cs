using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
	State currentState;
	
	#region STATES
	public IdleState idleState = new();
	public DashingState dashingState= new();
	public SlashingState slashingState= new();
	#endregion
	
	#region REFS
	public CardManager cm;
	public PlayerControlScript pcs;
	public AbilityManagerScript ams;
	#endregion
	
	#region SINGLETON
	public static StateController me;
	void Awake()
	{
		me = this;
	}
	#endregion

	void Start()
	{
		currentState = idleState;
		cm = CardManager.me;
		pcs = PlayerControlScript.me;
		ams = AbilityManagerScript.me;
	}
	void Update()
	{
		currentState?.OnUpdate();
		//print("current state: "+currentState.ToString()+"");
	}
	public void ChangeState(State newState)
	{
		currentState?.OnExit();
		currentState = newState;
		currentState.OnEnter(this);
		//print("change state to: "+newState.ToString()+"");
	}
	
}
