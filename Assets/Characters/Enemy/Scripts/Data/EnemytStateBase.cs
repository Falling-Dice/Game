public enum EnemyStateId
{
	FollowPlayer,
	Attacking,
}

public interface IEnemyState
{
	EnemyStateId Id { get; }
	void Enter();
	void Update();
	void Exit();
}

public abstract class EnemyStateBase : IEnemyState
{
	public virtual void Initialize(EnemyAgent agent)
	{
		Agent = agent;
	}
	protected EnemyAgent Agent { get; private set; }


	#region abstracts
	public abstract EnemyStateId Id { get; }
	public abstract void Enter();
	public abstract void Update();
	public abstract void Exit();
	#endregion
}