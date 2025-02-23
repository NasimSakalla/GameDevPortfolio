using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChaseEngine : MonoBehaviour
{
    //audio

    [SerializeField] AudioSource audioSourceHeartbeat;
    [SerializeField] AudioClip heartBeatSound;

    //JS

    [SerializeField] RawImage JSImage;

    //Chase

    bool isReturningToResetPoint;
    private Coroutine chaseDelayCoroutine;          // To keep track of the running coroutine it stores inside the coro allowing us to control it with ease
    [SerializeField] Vector3[] PP = new Vector3[6];


    // getting the pos of our player

    [SerializeField] Transform moveToPos;
    [SerializeField] NavMeshAgent purserAgent;

    void Start()
    {
        //chase


        isReturningToResetPoint = true;

        //Js

        JSImage.gameObject.SetActive(false);

    }

    void Update()
    {
        //chase

        ChasePlayer();


        //audio

        Audio();
    }

    void ChasePlayer()
    {
        // If the player is in range, chase the player.

        if (PlayerDetection.isPlayerInRange)
        {
            purserAgent.destination = moveToPos.position;
            // Reset the state when chasing the player to ensure correct behavior when losing sight of the player.
            isReturningToResetPoint = true;

            // Stop any running delay coroutine if the player comes back in range
            if (chaseDelayCoroutine != null)
            {
                //Stops the coro if it wasnt null aka is running a coro
                StopCoroutine(chaseDelayCoroutine);

                //emtpy him just in case when he gets filled again we wont have issues
                chaseDelayCoroutine = null;
            }



        }
        else if (chaseDelayCoroutine == null)   // Checks if no delay coroutine is currently running.
        {
            chaseDelayCoroutine = StartCoroutine(ChasePlayerAfterDelay());
        }
    }
    
    
    void ChasePlayerAfterOutOfRange()
    {
        if (isReturningToResetPoint)
        {
            // Set the destination to the reset point (PP[0]) if returning.
            purserAgent.destination = PP[0];

            // Check if the agent has arrived at the reset point.
            //!playerAgent.pathPending = is the path done cacluting
            //playerAgent.remainingDistance <= playerAgent.stoppingDistance: Checks if he is close to the point

            if (!purserAgent.pathPending && purserAgent.remainingDistance <= purserAgent.stoppingDistance)
            {
                // Agent has reached PP[0], switch to roaming between random points.

                isReturningToResetPoint = false;

                // Choose a random point from PP[1] to PP[5].
                purserAgent.destination = PP[Random.Range(1, 6)];
            }
        }
        else //this says basically - isReturningToResetPoint = false
        {
            // The agent is currently roaming; checking if it has arrived at the current random destination just like before
            if (!purserAgent.pathPending && purserAgent.remainingDistance <= purserAgent.stoppingDistance)
            {
                // Agent has reached its current random target; select a new random point.
                purserAgent.destination = PP[Random.Range(1, 6)];
            }
        }

        // Reset the coroutine variable after completion
        chaseDelayCoroutine = null;
    }
    void Audio()
    {
        //magnitude check if the vector any size or lenght not regarding its direction
        if (PlayerDetection.isPlayerInRange && !audioSourceHeartbeat.isPlaying)
        {
            // Play heartbeat sound 
            audioSourceHeartbeat.PlayOneShot(heartBeatSound);
        }
        //stop heartbeat sound
        if (!PlayerDetection.isPlayerInRange && audioSourceHeartbeat.isPlaying)
        {
            audioSourceHeartbeat.Stop();
        }
    }
    void CaughtPlayer()
    {
        //show jumpscare png
        JSImage.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        //JS check

        if (other.gameObject.CompareTag("Player"))
        {
            CaughtPlayer();
        }
    }

    IEnumerator ChasePlayerAfterDelay()
    {
        // Wait for a few seconds after the player goes out of range
        yield return new WaitForSeconds(2.5f);

        // Call the function to handle behavior after the delay
        ChasePlayerAfterOutOfRange();
    }
}
