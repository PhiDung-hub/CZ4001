using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MoveEventListener : MonoBehaviour
{
    [SerializeField] private AudioClip[] FootstepSounds;
    [SerializeField] private AudioClip JumpSound;
    [SerializeField] private AudioClip LandingSound;
    public AudioSource AudioSource;
    [SerializeField] private InputActionReference JumpButton;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private InputActionReference MoveButton;
    [SerializeField] private float _stepInterval;
    [SerializeField] private float _stepSampleRate;

    private CharacterController _characterController;
    private Vector3 _velocity;

    [SerializeField] private float _stepCycle;
    [SerializeField] private float _nextStep;
    [SerializeField] private bool _isJumping;


    // TODO: Add sound when player moves forward/backward 
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _jumpHeight = 1;
        _stepSampleRate = 2.0f;
        _stepInterval = 1.0f;
        JumpButton.action.performed += Jumping;
        MoveButton.action.performed += ProgressStepCycle;
    }


    private void FixedUpdate()
    {
        if (_characterController.isGrounded && _isJumping)
        {
            _isJumping = false;
            PlayLandingSound();
        }

        float delta = Time.deltaTime;
        _velocity += Physics.gravity * delta;
        _characterController.Move(_velocity * delta);
    }

    // Collide simulation with other objects
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (_characterController.collisionFlags == CollisionFlags.Below) return;

        if (body == null || body.isKinematic) return;

        body.AddForceAtPosition(_characterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }

    private void Jumping(InputAction.CallbackContext obj)
    {
        if (!_characterController.isGrounded || _isJumping) return;
        _isJumping = true;
        PlayJumpSound();
        _velocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * Physics.gravity.y); // v = sqrt(2gh)
    }

    private void PlayJumpSound()
    {
        AudioSource.clip = JumpSound;
        AudioSource.Play();
    }

    private void PlayLandingSound()
    {
        AudioSource.clip = LandingSound;
        AudioSource.Play();
    }

    void ProgressStepCycle(InputAction.CallbackContext obj)
    {
        _stepCycle += _stepSampleRate * Time.fixedDeltaTime;
        if (!(_nextStep < _stepCycle)) return;

        _nextStep = _stepCycle + _stepInterval;
        PlayFootStepSound();
    }
    private void PlayFootStepSound()
    {
        if (!_characterController.isGrounded) return;

        int i = Random.Range(1, FootstepSounds.Length);
        AudioSource.clip = FootstepSounds[i];
        AudioSource.PlayOneShot(AudioSource.clip);
        FootstepSounds[i] = FootstepSounds[0];
        FootstepSounds[0] = AudioSource.clip;
    }

}
