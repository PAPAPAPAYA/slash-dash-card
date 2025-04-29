public abstract class State
{
	public StateController sc;
	public virtual void OnEnter(StateController stateController)
	{
		sc = stateController;
	}
	public virtual void OnUpdate()
	{
	
	}
	public virtual void OnExit()
	{
		
	}
}
