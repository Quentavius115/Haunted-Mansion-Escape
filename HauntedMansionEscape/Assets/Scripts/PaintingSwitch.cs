using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSwitch : MonoBehaviour
{

    public GameObject sphere;
    FireChange fire;
    // Start is called before the first frame update
    void Start()
    {
        fire = FindObjectOfType<FireChange>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fire.burned == true){
            sphere.SetActive(true);
        }
    }
}
