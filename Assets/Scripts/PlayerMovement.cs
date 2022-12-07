using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    [Header("Audio Stuff")]
    public AudioSource footstepAudio;
    public AudioSource effectsAudio;
    public float stepRate = 0.5f;
    float stepCooldown;


    void Update()
    {
        if (_GM.gameState != GameState.Playing)
            return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Footstep Audio stuff
        stepCooldown -= Time.deltaTime;
        if ((move.x != 0 || move.z != 0) && stepCooldown < 0 && isGrounded)
        {
            footstepAudio.clip = _AM.GetFootsteps();
            footstepAudio.pitch = Random.Range(0.9f, 1.1f);
            footstepAudio.Play();
            stepCooldown = stepRate;
        }
    }
}
