public class DashingState : State
{
	public override void OnEnter(StateController stateController)
	{
		base.OnEnter(stateController);
		sc.cm.MoveCard_GraveFirstToHandFirst();
	}
	public override void OnUpdate()
	{
		base.OnUpdate();
		if (!sc.pcs.moving)
		{
			sc.ChangeState(sc.idleState);
		}
	}
	public override void OnExit()
	{
		base.OnExit();
	}
}
