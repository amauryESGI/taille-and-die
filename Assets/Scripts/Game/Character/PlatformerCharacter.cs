﻿using System.Collections;
using UnityEngine;

namespace Platformer.Character {

    public class PlatformerCharacter : MonoBehaviour {
        private bool _facingRight = true;                    // For determining which way the player is currently facing.

        [SerializeField]
        private float _maxSpeed = 10f;      // The fastest the player can travel in the x axis.
        [SerializeField]
        private float _jumpForce = 400f;    // Amount of force added when the player jumps.	

        //[Range(0, 1)][SerializeField] private float _crouchSpeed = .36f;
        // Amount of maxSpeed applied to crouching movement. 1 = 100%

        [SerializeField]
        private bool _airControl = false;   // Whether or not a player can steer while jumping;
        [SerializeField]
        private LayerMask _whatIsGround;    // A mask determining what is ground to the character

        [SerializeField]
        private Transform _groundCheck;     // A position marking where to check if the player is grounded.
        private float _groundedRadius = .2f;                 // Radius of the overlap circle to determine if grounded
        private bool _grounded = false;                      // Whether or not the player is grounded.
        [SerializeField]
        private Transform _ceilingCheck;    // A position marking where to check for ceilings
        private float _ceilingRadius = .01f;                 // Radius of the overlap circle to determine if the player can stand up

        [SerializeField]
        private Animator _anim;             // Reference to the player's animator component.
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _flipOffsetX=0;

        private bool _isKnockback = false;

        public bool Grounded {
            get { return _grounded; }
            set { _grounded = value; }
        }

        private void FixedUpdate() {
            _grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundedRadius, _whatIsGround);
            _anim.SetBool("Ground", _grounded);
            _anim.SetBool("isKnockback", _isKnockback);
            _anim.SetFloat("vSpeed", _rigidbody.velocity.y);
        }

        public IEnumerator Knockback(float knockbackPwd, float knockbackTime, bool knockFromRight) {
            _isKnockback = true;

            _rigidbody.velocity = knockFromRight
                ? new Vector2(-knockbackPwd, knockbackPwd)
                : new Vector2(knockbackPwd, knockbackPwd);

            yield return new WaitForSeconds(knockbackTime);

            _rigidbody.velocity = Vector2.zero;
            _isKnockback = false;
        }

        public void Move(float move, bool jump) {

            // only control the player if grounded or airControl is turned on
            if (_grounded || _airControl) {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                //move = (crouch ? move*crouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                _anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                if (!_isKnockback)
                    _rigidbody.velocity = new Vector2(move * _maxSpeed, _rigidbody.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !_facingRight)
                    // ... flip the player.
                    Flip();
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && _facingRight)
                    // ... flip the player.
                    Flip();
            }
            // If the player should jump...
            if (_grounded && jump && _anim.GetBool("Ground")) {
                // Add a vertical force to the player.
                _grounded = false;
                _anim.SetBool("Ground", false);
                _rigidbody.AddForce(new Vector2(0f, _jumpForce));
            }
        }


        private void Flip() {
            // Switch the way the player is labelled as facing.
            _facingRight = !_facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

            float offsetX =0;
            if (transform.localScale.x > 0)
                offsetX = _flipOffsetX;
            else
                offsetX = -_flipOffsetX;

            // Allow to center sprite not center.
            transform.position = new Vector3(
                transform.position.x + offsetX,
                transform.position.y
                );
        }
    }
}