using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDestroyable
{
    [SerializeField] CharacterController2D controller2D;
    [SerializeField] Animator animator;
    [SerializeField] Health health;
    [SerializeField] AudioSource jumpSFX;

    [SerializeField] float walkSpeed = 50f;
    [SerializeField] float runSpeed = 100f;

    PlayerInput playerInput;

    float hMovement = 0;

    bool jump = false;
    bool run = false;
    bool crouch = false;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // check if the run buttons are pressed
        run = playerInput.actions["Run"].IsPressed();

        // set the animation for running
        animator.SetFloat("speed", Mathf.Abs(hMovement));
    }


    void FixedUpdate()
    {
        controller2D.Move(hMovement * Time.fixedDeltaTime, crouch, jump);
    }

    void OnMove(InputValue inputValue)
    {
        float speed = 0f;

        if (run)
            speed = runSpeed;
        else
            speed = walkSpeed;
            
        hMovement = inputValue.Get<Vector2>().x * speed;
    }

    void OnJump()
    {
        if (!jump)
        { 
            jump = true;
            jumpSFX.Play();
            animator.SetBool("jump", true);
        }
    }

    public void OnLanding()
    {
        jump = false;
        animator.SetBool("jump", false);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
        GameManager.Instance.GameState = GameManager.eGameState.PlayerDead;
    }
}
