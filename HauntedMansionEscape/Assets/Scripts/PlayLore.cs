using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLore : MonoBehaviour
{
	public AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<AudioSource>() != null){
            player = GetComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(){
        player.Play();
    }
}
