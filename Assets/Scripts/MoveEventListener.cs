using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
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
    private Vector3 _fallVelocity;

    [SerializeField] private float _stepCycle;
    [SerializeField] private float _nextStep;
    [SerializeField] private bool _isJumping;


    // TODO: Add sound when player moves forward/backward 
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _jumpHeight = 1;
        _stepSampleRate = 2.0f;
        _stepInterval = 0.0008f;
        JumpButton.action.performed += Jumping;
        MoveButton.action.performed += ProgressStepCycle;
    }


    public Vector3 previousPosition = new Vector3();
    public Vector3 currentPosition = new Vector3();
    public Vector3 velocity;
    private void Update()
    {
        previousPosition = currentPosition;
        currentPosition = transform.position;
        velocity = currentPosition - previousPosition;

    }
    private void FixedUpdate()
    {
        if (_characterController.isGrounded && _isJumping)
        {
            _isJumping = false;
            PlayLandingSound();
        }

        float delta = Time.deltaTime;
        _fallVelocity += Physics.gravity * delta;
        _characterController.Move(_fallVelocity * delta);
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
        if (_isJumping) return;
        _isJumping = true;
        PlayJumpSound();
        _fallVelocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * Physics.gravity.y); // v = sqrt(2gh)
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
        float groundVelocityMagnitude = velocity.x * velocity.x + velocity.y * velocity.y;
        if (_characterController.isGrounded)
        {
            _stepCycle += _stepSampleRate * Time.deltaTime * groundVelocityMagnitude;
        }
        if (!(_nextStep < _stepCycle)) return;

        _nextStep = _stepCycle + _stepInterval;
        PlayFootStepSound();
    }

    private void PlayFootStepSound(float volume = 0.6f)
    {
        if (!_characterController.isGrounded) return;

        int i = Random.Range(1, FootstepSounds.Length);
        AudioSource.clip = FootstepSounds[i];
        AudioSource.PlayOneShot(AudioSource.clip, volume);
        FootstepSounds[i] = FootstepSounds[0];
        FootstepSounds[0] = AudioSource.clip;
    }

}
