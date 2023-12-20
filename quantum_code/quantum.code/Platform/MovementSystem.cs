using Photon.Deterministic;

namespace Quantum.Platform
{
	public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>
	{
		public struct Filter
		{
			public EntityRef Entity;
			public Transform3D* Transform;
			public CharacterController3D* CharacterController;
		}

		public override void Update(Frame f, ref Filter filter)
		{
			Input input = default;
			if (f.Unsafe.TryGetPointer(filter.Entity, out PlayerLink* playerLink))
			{
				input = *f.GetPlayerInput(playerLink->Player);
			}

			if (input.Jump.WasPressed)
			{
				f.Events.PlayerJump(filter.Entity);
				filter.CharacterController->Jump(f);
			}

			filter.CharacterController->Move(f, filter.Entity, input.Direction.XOY);
			var velocity = filter.CharacterController->Velocity;
			velocity.Y = 0;
			if (velocity.Magnitude > FP.FromFloat_UNSAFE(0.01f))
			{
				Transform3D* pointer = ((FrameThreadSafe)f).GetPointer<Transform3D>(filter.Entity);
				// pointer->Rotation = FPQuaternion.LookRotation(velocity);
			}
			
		}
	}
}