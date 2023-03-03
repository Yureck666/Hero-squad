using HeroSquad.Characters;
using HeroSquad.Characters.Player;

namespace HeroSquad.StateMachine
{
	public class CharacterRunState: CharacterState
	{
		private CharacterPresenter _character;
		
		public CharacterRunState(CharacterPresenter character)
		{
			_character = character;
		}

		public override void Enter()
		{
			base.Enter();
			_character.SetNavMeshRotationActive(true);
		}

		public override void Exit()
		{
			base.Exit();
			_character.SetNavMeshRotationActive(false);
		}
	}
}