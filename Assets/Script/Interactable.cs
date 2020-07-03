using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactionRadius;
    GameObject player;

    public string
        textOption;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }


    public virtual void Interact()
    {
        print("Base Interact");

        /*Put whatever you want in a
         * subclass inheriting the Interactable
         * class, using "public override void Interact()"*/
    }

    private void LateUpdate()
    {
        Collider[] check = Physics.OverlapSphere(transform.position, interactionRadius);
        for (var i = 0; i < check.Length; i++)
        {
            if (check[i].gameObject == player)
            {
                print("player in range");
                if (Player.instance.targetObject == this.gameObject)
                {
                    Player.instance.targetLocation = player.transform.position;
                    Interact();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
