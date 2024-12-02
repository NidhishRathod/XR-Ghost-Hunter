using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public float eatDistance = 0.3f;  // Distance to destroy orb
    public Animator animator;
    public NavMeshAgent agent;
    public float speed = 1;

    public UIManager uiManager; // Reference to UIManager

    void Start()
    {
        // Initialization if needed
    }

    public GameObject GetClosestOrb()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        GameObject[] orbs = GameObject.FindGameObjectsWithTag("Orbs");

        foreach (var orb in orbs)
        {
            Vector3 ghostPosition = transform.position;
            ghostPosition.y = 0; // Ignore y-axis for distance check

            Vector3 orbPosition = orb.transform.position;
            orbPosition.y = 0; // Ignore y-axis for distance check

            float d = Vector3.Distance(ghostPosition, orbPosition);

            if (d < minDistance)
            {
                minDistance = d;
                closest = orb;
            }
        }

        if (minDistance < eatDistance && closest != null)
        {
            OrbSpawner.instance.DestroyOrb(closest);  // Destroy the closest orb
            if (uiManager != null)  // Ensure uiManager is assigned
            {
                uiManager.DecreaseHeart();  // Decrease heart when an orb is eaten
            }
        }

        return closest;
    }

    void Update()
    {
        if (!agent.enabled)
            return;

        GameObject closest = GetClosestOrb();

        if (closest)
        {
            Vector3 targetPosition = closest.transform.position;
            agent.SetDestination(targetPosition);
            agent.speed = speed;
        }
    }

    public void Kill()
    {
        agent.enabled = false;
        animator.SetTrigger("Ded");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
