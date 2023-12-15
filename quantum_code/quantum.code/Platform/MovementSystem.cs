namespace Quantum.Platform
{
	public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>
	{
		public struct Filter
		{
			public EntityRef Entity;
			public CharacterController3D* CharacterController;
		}

		public override void Update(Frame f, ref Filter filter)
		{
			// gets the input for player 0
			var input = *f.GetPlayerInput(0);

			if (input.Jump.WasPressed)
			{
				filter.CharacterController->Jump(f);
			}

			filter.CharacterController->Move(f, filter.Entity, input.Direction.XOY);
		}
	}
}