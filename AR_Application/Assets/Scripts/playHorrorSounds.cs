using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playHorrorSounds : MonoBehaviour
{
    public AudioClip[] horrorSounds;
    public float playEvery = 20;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("playSound", 10, playEvery);
    }


    void playSound()
    {
        if (Random.Range(0, 10) % 2 == 0) {
            int randomIndex = Random.Range(0, horrorSounds.Length);
            AudioSource.PlayClipAtPoint(horrorSounds[randomIndex], transform.position);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
