using UnityEngine;

namespace Interaction
{
    public abstract class Interactable : MonoBehaviour {
        // minigame is for custom types, for example connect wires to interact with doors
        public enum InteractionType {
            Click,
            Hold,
            Minigame
        }

        private float _holdTime;

        public InteractionType interactionType;

        public abstract string GetDescription();
        public abstract void Interact();

        public void IncreaseHoldTime() => _holdTime += Time.deltaTime;
        public void ResetHoldTime() => _holdTime = 0f;

        public float GetHoldTime() => _holdTime;
    }
}