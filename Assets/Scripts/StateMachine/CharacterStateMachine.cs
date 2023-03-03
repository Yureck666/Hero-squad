namespace HeroSquad.StateMachine
{
	public class CharacterStateMachine
	{
		public CharacterState CurrentState { get; private set; }

		public void Init(CharacterState state)
		{
			CurrentState = state;
			CurrentState.Enter();
		}

		public void ChangeState(CharacterState state)
		{
			CurrentState.Exit();
			CurrentState = state;
			CurrentState.Enter();
		}
	}
}