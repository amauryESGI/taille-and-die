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
            float h = 0f;
                if (CrossPlatformInputManager.GetButton("Punsh"))
                    _attaque.Punsh();
                else
                    h = CrossPlatformInputManager.GetAxis("Horizontal");
            _character.Move(h, jump);
            jump = false;
        }
    }
}