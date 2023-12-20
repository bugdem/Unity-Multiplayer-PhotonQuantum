using Quantum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Core
{
    public class CharacterAnimation : QuantumCallbacks
    {
		private EntityView _entityView;
		private Animator _animator;
		private EntityPrototype _entityPrototype;
		private bool _waitingToGrounded;

		private void Awake()
		{
			_entityView = GetComponent<EntityView>();
			_entityPrototype = GetComponent<EntityPrototype>();
			_animator = GetComponentInChildren<Animator>();

			QuantumEvent.Subscribe<EventPlayerJump>(this, OnPlayerJump);	
		}

		public override void OnGameStart(QuantumGame game)
		{
			var frame = game.Frames.Predicted;
			var kcc = frame.Get<CharacterController3D>(_entityView.EntityRef);
			_animator.SetBool("Grounded", kcc.Grounded);
			if (!kcc.Grounded)
			{
				_animator.SetTrigger("Jump");
				_waitingToGrounded = true;
			}
		}

		public override void OnUpdateView(QuantumGame game)
		{
			var frame = game.Frames.Predicted;
			var kcc = frame.Get<CharacterController3D>(_entityView.EntityRef);
			Vector3 velocity = kcc.Velocity.ToUnityVector3();
			velocity.y = 0f;
			_animator.SetFloat("Speed", velocity.magnitude);

			if (_waitingToGrounded && kcc.Grounded)
			{
				_animator.SetTrigger("Grounded");
				_waitingToGrounded = false;
			}

			_animator.SetBool("Grounded", kcc.Grounded);
		}

		private void OnPlayerJump(EventPlayerJump e)
		{
			if (e.Entity == _entityView.EntityRef)
			{
				_animator.SetTrigger("Jump");
				_waitingToGrounded = true;
			}
		}
	}
}