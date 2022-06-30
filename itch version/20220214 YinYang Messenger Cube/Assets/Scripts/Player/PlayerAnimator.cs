using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles interactions with the animator component of the player
/// It reads the player's state from the controller and animates accordingly
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The player controller script to read state information from")]
    public PlayerController playerController;
    [Tooltip("The animator component that controls the player's animations")]
    public Animator animator;

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first update
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Start()
    {
        ReadPlayerStateAndAnimate();
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called every frame
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Update()
    {
        ReadPlayerStateAndAnimate();
    }

    /// <summary>
    /// Description:
    /// Reads the player's state and then sets and unsets booleans in the animator accordingly
    /// Input:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    void ReadPlayerStateAndAnimate()
    {
        // public animations
        if (animator == null)
        {
            return;
        }
        if (playerController.state == PlayerController.PlayerState.Idle)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }

        // special animation of Yang
        if (gameObject.name == "Yang")
        {
            if (playerController.state == PlayerController.PlayerState.Walk)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }


        // special animation of Yin
        if (gameObject.name == "Yin")
        {
            if (playerController.state == PlayerController.PlayerState.Disappear)
            {
                animator.SetBool("isDisappear", true);
            }
            else
            {
                animator.SetBool("isDisappear", false);
            }
        }


        //if (playerController.state == PlayerController.PlayerState.Fall)
        //{
        //    animator.SetBool("isFalling", true);
        //}
        //else
        //{
        //    animator.SetBool("isFalling", false);
        //}

        //if (playerController.state == PlayerController.PlayerState.Walk)
        //{
        //    animator.SetBool("isWalking", true);
        //}
        //else
        //{
        //    animator.SetBool("isWalking", false);
        //}

        //if (playerController.state == PlayerController.PlayerState.Dead)
        //{
        //    animator.SetBool("isDead", true);
        //}
        //else
        //{
        //    animator.SetBool("isDead", false);
        //}
    }
}
