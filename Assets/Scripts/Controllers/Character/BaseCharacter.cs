using System;

using Helpers;

using UnityEngine;
using UnityEngine.Rendering;

using Random = UnityEngine.Random;

/*
 * Base Character class. A Character is defined by generally any "living" thing. Anything that moves around and
 * interacts with the world. This could include both the Player entity and various enemies.
 *
 * Characters with special requirements, such as keyboard input, should extend this class. Other functionality
 * can be tacked on with component scripts.
 */

namespace Controllers.Character {
    public class BaseCharacter : PhysicsObject {
        [Header("Health")] [SerializeField] private int health = 100;
        [SerializeField] private int maxHealth = 100;

        [Header("Movement")] [SerializeField] private float speed = 7;
        [SerializeField] private float jumpPower = 17;
        [SerializeField] private float impulseRecovery = 3;
        public float impulse;

        [Header("Audio")] [SerializeField] private bool playFootStepSound = true;
        [SerializeField] private AudioClip[] footStepClips;
        [SerializeField] private float footStepVolume = 0.1f;

        protected Animator _animator;
        private AudioHelper _audioHelper;

        private static readonly int VelocityX = Animator.StringToHash("velocityX");
        private static readonly int VelocityY = Animator.StringToHash("velocityY");
        private static readonly int MoveDirection = Animator.StringToHash("moveDirection");

        public bool isFacingRight = true;
        public bool isJumping;
        public bool isMoving;

        public Vector3 ScreenPoint => Camera.main.WorldToScreenPoint(transform.position);


        protected virtual void Start() {
            var audioSource = GetComponent<AudioSource>();
            _audioHelper = new AudioHelper(audioSource);

            _animator = GetComponent<Animator>();
        }


        protected virtual void Update() {
            if (_animator) {
                var moveDir = 0;
                if (isMoving) {
                    moveDir = isFacingRight ? 1 : -1;
                }

                _animator.SetInteger(MoveDirection, moveDir);
            }

            FlipSprite();
        }


        protected void Die() {
            Destroy(gameObject);
        }


        public void Damage(int amount) {
            health -= amount;
        }


        private void FlipSprite() {
            var currLocalScale = transform.localScale;
            var spriteFacingRight = currLocalScale.x > 0;
            if ((isFacingRight && !spriteFacingRight) || (!isFacingRight && spriteFacingRight)) {
                currLocalScale.x *= -1;
                transform.localScale = currLocalScale;
            }
        }


        public void Move(float horizontalAxis) {
            isMoving = horizontalAxis != 0;

            impulse += (0 - impulse) * Time.deltaTime * impulseRecovery;
            var xVel = horizontalAxis + impulse;
            targetVelocity = new Vector2(xVel, 0) * speed;

            if (_animator) {
                // _animator.SetFloat(VelocityX, Mathf.Abs(velocity.x) / speed);
                // _animator.SetFloat(VelocityY, velocity.y);
            }
        }


        protected void Jump() {
            if (isJumping) return;
            if (Math.Abs(velocity.y - jumpPower) < 0.001) return;

            velocity.y = jumpPower;
            isJumping = true;
        }


        public void OnLand() {
            if (!isJumping) return;

            isJumping = false;
        }


        public void OnFootStep() {
            var pitch = Random.Range(0.9f, 1.1f);
            var volume = Random.Range(footStepVolume - 0.05f, footStepVolume + 0.5f);
            _audioHelper.PlayRandomAudioOneShot(footStepClips, pitch, volume);
        }
    }
}