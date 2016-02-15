using System.Collections;
using UnityEngine;

namespace Platformer.Character {

    public class PlatformerCharacter : MonoBehaviour {
        private bool facingRight = true;                    // For determining which way the player is currently facing.

        [SerializeField] private float maxSpeed = 10f;      // The fastest the player can travel in the x axis.
        [SerializeField] private float jumpForce = 400f;    // Amount of force added when the player jumps.	

        [Range(0, 1)][SerializeField] private float crouchSpeed = .36f;
                                                            // Amount of maxSpeed applied to crouching movement. 1 = 100%

        [SerializeField] private bool airControl = false;   // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask whatIsGround;    // A mask determining what is ground to the character

        [SerializeField] private Transform groundCheck;     // A position marking where to check if the player is grounded.
        private float groundedRadius = .2f;                 // Radius of the overlap circle to determine if grounded
        private bool grounded = false;                      // Whether or not the player is grounded.
        [SerializeField] private Transform ceilingCheck;    // A position marking where to check for ceilings
        private float ceilingRadius = .01f;                 // Radius of the overlap circle to determine if the player can stand up
        [SerializeField] private Animator anim;             // Reference to the player's animator component.
        [SerializeField] private Rigidbody2D rigidbody;

        private bool  _isKnockback = false;

        private void FixedUpdate() {
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
            
            anim.SetBool("Ground", grounded);
            anim.SetBool("isKnockback", _isKnockback);
            anim.SetFloat("vSpeed", rigidbody.velocity.y);
        }

        public IEnumerator Knockback(float knockbackPwd, float knockbackTime, bool knockFromRight) {
            _isKnockback = true;

            rigidbody.velocity = knockFromRight
                ? new Vector2(-knockbackPwd, knockbackPwd)
                : new Vector2(knockbackPwd, knockbackPwd);

            yield return new WaitForSeconds(knockbackTime);

            rigidbody.velocity = Vector2.zero;
            _isKnockback = false;
        }

        public void Move(float move, bool crouch, bool jump) {
            // If crouching, check to see if the character can stand up
            if (!crouch && anim.GetBool("Crouch")) {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
                    crouch = true;
            }

            // Set whether or not the character is crouching in the animator
            anim.SetBool("Crouch", crouch);

            // only control the player if grounded or airControl is turned on
            if (grounded || airControl) {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                //move = (crouch ? move*crouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                if (!_isKnockback)
                    rigidbody.velocity = new Vector2(move * maxSpeed, rigidbody.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                    // ... flip the player.
                    Flip();
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                    // ... flip the player.
                    Flip();
            }
            // If the player should jump...
            if (grounded && jump && anim.GetBool("Ground")) {
                // Add a vertical force to the player.
                grounded = false;
                anim.SetBool("Ground", false);
                rigidbody.AddForce(new Vector2(0f, jumpForce));
            }
        }


        private void Flip() {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}