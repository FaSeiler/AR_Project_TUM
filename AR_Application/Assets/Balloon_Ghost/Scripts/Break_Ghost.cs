using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Ghost : MonoBehaviour
{
    public bool Is_Breaked = false;
    public GameObject ghost_normal;
    public GameObject ghost_Parts;
    public Animator ghost;
    int counter;

    public Vector3 nextTracker;

    Vector3 distanceLeft;
    const float DISTANCE_INCREMENT = 0.01f;
    int isPositiveDirection = 1;

    // private void Awake()
    // {
    //     Debug.Log("AymaneShadow: Balloon Ghost is awake.");
    // }

    // Start is called before the first frame update
    void Start()
    {
        ghost_normal.SetActive(true);
        ghost_Parts.SetActive(false);

        // Make Balloon face Camera
        transform.Rotate(270, 0, 180, Space.Self);                
    }

    // Update is called once per frame
    void Update()
    {
        if(Is_Breaked == true)
        {
            ghost_Parts.SetActive(true);
            ghost_normal.SetActive(false);
        }       
    }
    public void break_Ghost()
    {
        Is_Breaked = true;     
        Debug.LogError("AymaneShadow: " + name + " has been broken.");     
    }

    public void play_anim()
    {
        counter += 1;
        if(counter == 2)
        {
            counter = 0;
            ghost.Play("idle");
        }
        else
        {
            ghost.Play("attack");
        }
    }
}
