
public class EnemyStateMachine
{
	public EnemyStateBase State { get; private set; }

	#region data 
	private readonly EnemyAgent _agent;
	#endregion


	public EnemyStateMachine(EnemyAgent agent, EnemyStateBase initialState)
	{
		_agent = agent;
		ChangeState(initialState);
	}

	#region methods
	public void Update()
		=> State?.Update();

	public void ChangeState(EnemyStateBase newState)
	{
		State?.Exit();
		State = newState;

		State?.Initialize(_agent);
		State?.Enter();
	}
	#endregion
}
