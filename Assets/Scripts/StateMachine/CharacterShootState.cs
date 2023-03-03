using HeroSquad.Characters;
using HeroSquad.Characters.Player;

namespace HeroSquad.StateMachine
{
	public class CharacterShootState: CharacterState
	{
		private CharacterPresenter _character;
		
		public CharacterShootState(CharacterPresenter character)
		{
			_character = character;
		}

		public override void Enter()
		{
			base.Enter();
			_character.SetNewTarget();
			_character.SetShootingActive(true);
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();
			_character.SetRotationToShootingTarget();
		}

		public override void Exit()
		{
			_character.SetShootingActive(false);
		}
	}
}