using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace Platformer.Character {
    [RequireComponent(
        typeof(PlatformerCharacter),
        typeof(PlatformerAttaque)
        )]
    public class PlatformerUserControl : MonoBehaviour {
        [SerializeField]
        private PlatformerCharacter _character;
        [SerializeField]
        private PlatformerAttaque _attaque;
        private bool jump;

        private void Update() {
            if (!jump)
                jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        private void FixedUpdate() {
            bool crouch = CrossPlatformInputManager.GetButton("Crouch");
            float h = 0f;
            if (!crouch) {
                if (CrossPlatformInputManager.GetButton("PunshLeft"))
                    _attaque.PunshLeft();
                else if (CrossPlatformInputManager.GetButton("PunshRight"))
                    _attaque.PunshRight();
                else
                    h = CrossPlatformInputManager.GetAxis("Horizontal");
            }
            _character.Move(h, crouch, jump);
            jump = false;
        }
    }
}