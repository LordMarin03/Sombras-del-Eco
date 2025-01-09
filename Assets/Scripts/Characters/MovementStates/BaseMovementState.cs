using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eco
{
    public abstract class BaseMovementState
    {
        protected BaseCharacter m_Character;

        public BaseMovementState(BaseCharacter character)
        {
            m_Character = character;
        }

        public abstract void OnEnter();

        public abstract void OnUpdate();

        public abstract void OnExit();
    }
}


