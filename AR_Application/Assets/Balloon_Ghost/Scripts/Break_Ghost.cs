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

    private void Awake()
    {
        Debug.Log("AymaneShadow: Balloon Ghost is awake.");
        //transform.rotation = Quaternion.identity;
        //Debug.Log("AymaneShadow: Balloon Ghost Rotation set to identity.");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("AymaneShadow: Balloon Ghost Started.");
        Debug.Log("AymaneShadow: Balloon Ghost rotation = " + transform.rotation);
        ghost_normal.SetActive(true);
        ghost_Parts.SetActive(false);

        // transform.rotation = Quaternion.identity;
        transform.Rotate(270, 0, 180, Space.Self);        

        // if (name.StartsWith("Balloon_Ghost(Mixed)_red"))
        // {        
        //     distanceLeft = new Vector3(1.5f, distanceLeft.y, distanceLeft.z);   
        // }
        // else if (name.StartsWith("Balloon_Ghost(Mixed)_pink"))
        // {        
        //     isPositiveDirection = -1;
        //     distanceLeft = new Vector3(distanceLeft.x, 0.4f, 0.4f);   
        // }  
        // else if (name.StartsWith("Balloon_Ghost(Mixed)_yellow"))
        // {        
        //     distanceLeft = new Vector3(1.0f, distanceLeft.y, distanceLeft.z);   
        // } 
        // else if (name.StartsWith("Balloon_Ghost(Mixed)_brown"))
        // {        
        //     distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y, 1.5f);   
        // }                 
    }

    // Update is called once per frame
    void Update()
    {
        if(Is_Breaked == true)
        {
            ghost_Parts.SetActive(true);
            ghost_normal.SetActive(false);
        }
                   
        // if(distanceLeft.x > 0)
        // {
        //     Debug.Log("AymaneShadow: Should move in X.");
        //     // Debug.Log("AymaneShadow: firstTime = " + firstTime);
        //     Vector3 oldPosition = transform.position;
        //     Debug.Log("AymaneShadow: Old Position = " + oldPosition);
        //     Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
        //     transform.position = new Vector3(transform.position.x + DISTANCE_INCREMENT * isPositiveDirection, transform.position.y, transform.position.z);                            
        //     distanceLeft = new Vector3(distanceLeft.x - DISTANCE_INCREMENT, distanceLeft.y, distanceLeft.z);
        //     Vector3 newPosition = transform.position;
        //     Debug.Log("AymaneShadow: New Position = " + newPosition);
        //     Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);  
        // }
        // else if(distanceLeft.y > 0)
        // {
        //     Debug.Log("AymaneShadow: Should move in Y.");
        //     // Debug.Log("AymaneShadow: firstTime = " + firstTime);
        //     Vector3 oldPosition = transform.position;
        //     Debug.Log("AymaneShadow: Old Position = " + oldPosition);
        //     Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
        //     transform.position = new Vector3(transform.position.x, transform.position.y + DISTANCE_INCREMENT * isPositiveDirection, transform.position.z);                            
        //     distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y - DISTANCE_INCREMENT, distanceLeft.z);
        //     Vector3 newPosition = transform.position;
        //     Debug.Log("AymaneShadow: New Position = " + newPosition);
        //     Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);  
        // }
        // else if(distanceLeft.z > 0)
        // {
        //     Debug.Log("AymaneShadow: Should move in Z.");
        //     // Debug.Log("AymaneShadow: firstTime = " + firstTime);
        //     Vector3 oldPosition = transform.position;
        //     Debug.Log("AymaneShadow: Old Position = " + oldPosition);
        //     Debug.Log("AymaneShadow: Old distanceLeft = " + distanceLeft);
        //     transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + DISTANCE_INCREMENT * isPositiveDirection);                            
        //     distanceLeft = new Vector3(distanceLeft.x, distanceLeft.y, distanceLeft.z - DISTANCE_INCREMENT);
        //     Vector3 newPosition = transform.position;
        //     Debug.Log("AymaneShadow: New Position = " + newPosition);
        //     Debug.Log("AymaneShadow: New distanceLeft = " + distanceLeft);  
        // }        
    }
    public void break_Ghost()
    {
        Is_Breaked = true;     
        Debug.Log("AymaneShadow: " + name + " has been broken.");     
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
