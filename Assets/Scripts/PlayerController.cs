using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody playerRB;
    private AudioSource playerAudio;
    public float jumpForce = 10;
    public float gravityModifier = 1;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool isJump = false;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || isJump) && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1f);
            isJump = !isJump;
        }
        if (Input.GetKey(KeyCode.LeftShift) && isOnGround && !gameOver)
        {
            Time.timeScale = 2f;
            score++;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Time.timeScale = 1f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            isJump = false;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1f);
            Debug.Log($"Score = {score}");
        }
    }
}
