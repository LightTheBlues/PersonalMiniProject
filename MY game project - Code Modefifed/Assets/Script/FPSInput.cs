using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    private PlayerCharacter playerCharacter;
    public float jumpSpeed = 5.8f;
    public float minFall = -0.8f;
    public float speed = 6.0f;

    public float gravity = -9.8f;
    private float _vertSpeed;
    private CharacterController _charController;
    private Animator _animator;

    public float pushForce = 3.0f;
    private ControllerColliderHit _contact;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        playerCharacter = GetComponentInParent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1 && playerCharacter.die == false)
        {
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;

            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);

            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < gravity)
                _vertSpeed = gravity;

            movement.y = _vertSpeed;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);

            _charController.Move(movement);

            bool isMoving = false;
            if (deltaX != 0f || deltaZ != 0f)
            {
                isMoving = true;
            }
            _animator.SetBool("walking", isMoving);
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}