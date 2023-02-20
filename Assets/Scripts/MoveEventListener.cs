using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class MoveEventListener : MonoBehaviour
{
    [SerializeField] private AudioClip[] FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip JumpSound;           // the sound played when character leaves the ground.
    [SerializeField] private AudioClip LandingSound;           // the sound played when character touches back on ground.
    private AudioSource AudioSource;
    [SerializeField] private InputActionReference JumpButton;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private InputActionReference MoveButton;
    [SerializeField] private float _stepInterval = 1f;
    [SerializeField] private float _moveSpeed = 2f;

    private CharacterController _characterController;
    private Vector3 _velocity;

    [SerializeField] private float _stepCycle;
    [SerializeField] private float _nextStep;


    private void OnEnable()
    {
        JumpButton.action.performed += Jumping;
        MoveButton.action.performed += ProgressStepCycle;
    }

    // TODO: Add sound when player moves forward/backward 
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();
        _stepCycle = 0f;
        _nextStep = _stepCycle / 2;
    }


    private void FixedUpdate()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            PlayLandingSound();
            _velocity.y = 0;
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
        if (!_characterController.isGrounded) return;
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
        if (_characterController.velocity.sqrMagnitude > 0.0f)
        {
            _stepCycle += (_characterController.velocity.magnitude + _moveSpeed) * Time.fixedDeltaTime;
        }
        if (!(_nextStep < _stepCycle)) return;

        _nextStep = _stepCycle + _stepInterval;
        // PlayFootStepSound();
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
