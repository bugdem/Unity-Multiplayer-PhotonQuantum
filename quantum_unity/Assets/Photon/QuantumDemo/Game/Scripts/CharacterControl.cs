using Cinemachine;
using Quantum;
using Quantum.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Core
{
    public class CharacterControl : QuantumCallbacks
	{
		[SerializeField] private CinemachineVirtualCameraBase _cmCameraFollowerPrefab;

		private EntityView _entityView;
		private EntityPrototype _entityPrototype;
		[SerializeField, ReadOnly] private CinemachineVirtualCameraBase _cmCameraFollower;

		private void Awake()
		{
			_entityView = GetComponent<EntityView>();
			_entityPrototype = GetComponent<EntityPrototype>();
		}

		public override void OnGameStart(QuantumGame game)
		{

		}

		public override void OnUpdateView(QuantumGame game)
		{

		}

		protected virtual void OnEntityInstantiated(QuantumGame game)
		{
			var frame = game.Frames.Predicted;
			var kcc = frame.Get<CharacterController3D>(_entityView.EntityRef);

			Debug.Log("Entity Created!");

			unsafe
			{
				if (frame.Unsafe.TryGetPointer(_entityView.EntityRef, out PlayerLink* playerLink))
				{
					if (game.PlayerIsLocal(playerLink->Player))
					{
						_cmCameraFollower = Instantiate(_cmCameraFollowerPrefab);
						_cmCameraFollower.Follow = _entityView.transform;
						_cmCameraFollower.LookAt = _entityView.transform;
						_cmCameraFollower.Priority = 50;
					}
				}
			}
		}

		private void OnPlayerJump(EventPlayerJump e)
		{

		}

		protected override void OnEnable()
		{
			base.OnEnable();

			_entityView.OnEntityInstantiated.AddListener(OnEntityInstantiated);
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			_entityView.OnEntityInstantiated.RemoveListener(OnEntityInstantiated);
		}
	}
}