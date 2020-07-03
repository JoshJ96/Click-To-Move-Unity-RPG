using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    #region Singleton

    public static Player instance;
    void Awake()
    {
        if (instance != null)
        {
            print("More than one instance of Player found");
            return;
        }
        instance = this;
    }

    #endregion

    NavMeshAgent agent;
    Animator anim;

    public Vector3 targetLocation;
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        targetLocation = transform.position;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        targetLocation = ClickManager.instance.clickedLocation;
        agent.SetDestination(targetLocation);
        anim.SetFloat("Blend", agent.velocity.magnitude / agent.speed);
    }
}
