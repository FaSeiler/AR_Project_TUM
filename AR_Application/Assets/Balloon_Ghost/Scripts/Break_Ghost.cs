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

    public Vector3 nextPosition;

    private void Awake()
    {
        Debug.Log("AymaneShadow: Balloon Ghost is awake.");
        //transform.rotation = Quaternion.identity;
        //Debug.Log("AymaneShadow: Balloon Ghost Rotation set to identity.");
    }

    // Start is called before the first frame update
    void Start()
    {
        ghost_normal.SetActive(true);
        ghost_Parts.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Is_Breaked == true)
        {
            ghost_Parts.SetActive(true);
            ghost_normal.SetActive(false);
        }

        //if (transform.position.z < nextPosition.z)
        //{
        //    transform.position += new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.001f);
        //}
    }
    public void break_Ghost()
    {
        Is_Breaked = true;        
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
