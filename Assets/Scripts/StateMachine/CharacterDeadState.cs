using HeroSquad.Characters;
using HeroSquad.Characters.Player;

namespace HeroSquad.StateMachine
{
	public class CharacterDeadState: CharacterState
	{
		private CharacterPresenter _character;
		
		public CharacterDeadState(CharacterPresenter character)
		{
			_character = character;
		}
		public override void Enter()
		{
			base.Enter();
			_character.Die();
		}
	}
}