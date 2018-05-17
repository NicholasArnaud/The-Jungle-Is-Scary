using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 1. start game pick a key
    /// 2. based on key that type of room will spawn. red green blue
    /// 3. use key at door then spawn that room. 
    /// 3a. on room load the doors will be locked
    /// 3b. on room complete the button that allows unlocking will be interactable
    /// 4. when a room is completed player will press a button to unlock room
    /// </summary>

    public Animator anim;
    public Vector3Variable position;
    public StringVariable Horizontal;
    public StringVariable Vertical;
    public StringVariable Jump;
    public FloatVariable Speed;
    public FloatVariable _currentSpeed;
    public FloatVariable _jumpForce;
    public float gravity = 20.0F;
    private Vector3 moveDirection;
    private CharacterController controller;
    public Vector3 targetDir;
    
    public bool ATTACKING { get; set; }
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (ATTACKING)
            return;
        if (controller.isGrounded)
        {

            var h = Input.GetAxis(Horizontal.Value);
            var v = Input.GetAxis(Vertical.Value);

            var forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;
            ///such copy paste but it works
            var right = new Vector3(forward.z, 0, -forward.x);
            targetDir = h * right + v * forward;

            if (targetDir.magnitude > 0)
                transform.rotation = Quaternion.LookRotation(targetDir);
            if (Input.GetButton("Jump"))
            {
                targetDir.y = _jumpForce.Value;
                anim.SetTrigger("Jump");
            }
                

            moveDirection = targetDir;
            anim.SetFloat("Speed", moveDirection.magnitude);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move((moveDirection * Speed.Value) * Time.deltaTime);

        _currentSpeed.Value = controller.velocity.magnitude;
    }
}
