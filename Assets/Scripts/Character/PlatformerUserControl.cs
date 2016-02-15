using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace Platformer.Character {
    [RequireComponent(typeof (PlatformerCharacter))]
    public class PlatformerUserControl : MonoBehaviour {
        [SerializeField] private PlatformerCharacter character;
        private bool jump;

        private void Update() {
            if(!jump)
                jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        private void FixedUpdate() {
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = 0f;
            if (!crouch)
                h = CrossPlatformInputManager.GetAxis("Horizontal");
            character.Move(h, crouch, jump);
            jump = false;
        }
    }
}