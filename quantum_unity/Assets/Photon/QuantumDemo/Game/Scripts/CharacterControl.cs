using Quantum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Core
{
    public class CharacterControl : QuantumCallbacks
	{
		private EntityView _entityView;
		private EntityPrototype _entityPrototype;

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

		private void OnPlayerJump(EventPlayerJump e)
		{

		}
	}
}