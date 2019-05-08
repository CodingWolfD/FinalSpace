using UnityEngine;

public class Animation_States : MonoBehaviour
{
    public enum states { idle, walking, running, jumping, crouching, crouchIdle, crouchStanding, punching }; // CREATES A NEW ENUM USED TO STORE THE PLAYERS DIFFERENT ANIMATION STATES
    public states animationStates; // CREATES A REFERENCE TO THE ENUM SO WE CAN CHANGE THE PLAYERS ANIMATION BASED ON THE ANIMATION STATES
    private Animator animator; // REFERENCE TO THE ANIMATOR THAT STORES THE PLAYERS DIFFERENT ANIMATIONS

    private void Start()
    {
        animator = GetComponent<Animator>(); // FINDS THE ANIMATOR RELATED TO THE PLAYER
        animationStates = states.idle; // SETS THE ANIMATOR STATE TO IDLE WHEN THE GAME IS STARTED
    }

    private void Update()
    {
        States(); // CALLS THE STATES METHOD TO HANDLE WHAT ANIMATIONS ARE PLAYED WHEN NEEDED
    }

    private void States()
    {
        switch (animationStates) // THIS SWITCH STAMEMENT HANDLES WHAT METHODS ARE CALLED BASED ON THE CURRENT PLAYER STATE
        {
            case states.idle: // IF THE CURRENT PLAYER STATE IS IDLE
                {
                    playIdleAnim(); // CALLS THE PLAY IDLE ANIMATION METHOD
                }
                break;
            case states.walking: // IF THE CURRENT PLAYER STATE IS WALKING
                {
                    playWalkingAnim(); // CALLS THE PLAY WALKING ANIMATION METHOD
                }
                break;
            case states.running: // IF THE CURRENT PLAYER STATE IS RUNNING
                {
                    playRunningAnim(); // CALLS THE PLAY RUNNING ANIMATION METHOD
                }
                break;
            case states.jumping: // IF THE CURRENT PLAYER STATE IS JUMPING
                {
                    playJumpingAnim(); // CALLS THE PLAY JUMPING ANIMATION METHOD
                }
                break;
        }
    }

    private void playIdleAnim()
    {
        animator.Play("Idle"); // GETS THE IDLE ANIMATION FROM THE ANIMATOR AND PLAYS IT
    }
    
    private void playWalkingAnim()
    {
        animator.Play("Walking"); // GETS THE WALKING ANIMATION FROM THE ANIMATOR AND PLATS IT
    }

    private void playJumpingAnim()
    {
        animator.Play("Jumping"); // GETS THE JUMPING ANIMATION FROM THE ANIMATOR AND PLAYS IT
    }

    private void playRunningAnim()
    {
        animator.Play("Running"); // FOR SOME FUN CHANGE THE RUNNING STRING TO "GoofyRun" 
    }
}