using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Interaction
{
    public class PlayerInteraction : MonoBehaviour {

        public float interactionDistance;

        public TextMeshProUGUI interactionText;
        public GameObject interactionHoldGO; // the ui parent to disable when not interacting
        public Image interactionHoldProgress; // the progress bar for hold interaction type

        private Camera _cam;

        private void Start(){
            _cam = GetComponentInChildren<Camera>();
        }
        private void Update(){
            Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
            RaycastHit hit;

            bool successfulHit = false;

            if (Physics.Raycast(ray, out hit, interactionDistance)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null) {
                    HandleInteraction(interactable);
                    interactionText.text = interactable.GetDescription();
                    successfulHit = true;

                    interactionHoldGO.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);
                }
            }

            // if we miss, hide the UI
            if (!successfulHit) {
                interactionText.text = "";
                interactionHoldGO.SetActive(false);
            }
        }

        void HandleInteraction(Interactable interactable) {
            KeyCode key = KeyCode.E;
            switch (interactable.interactionType) {
                case Interactable.InteractionType.Click:
                    // interaction type is click and we clicked the button -> interact
                    if (Input.GetKeyDown(key)) {
                        interactable.Interact();
                    }
                    break;
                case Interactable.InteractionType.Hold:
                    if (Input.GetKey(key)) {
                        // we are holding the key, increase the timer until we reach 1f
                        interactable.IncreaseHoldTime();
                        if (interactable.GetHoldTime() > 1f) {
                            interactable.Interact();
                            interactable.ResetHoldTime();
                        }
                    } else {
                        interactable.ResetHoldTime();
                    }
                    interactionHoldProgress.fillAmount = interactable.GetHoldTime();
                    break;
                // here is started code for your custom interaction :)
                case Interactable.InteractionType.Minigame:
                    // here you make ur minigame appear
                    break;

                // helpful error for us in the future
                default:
                    throw new System.Exception("Unsupported type of interactable.");
            }
        }
    }
}
