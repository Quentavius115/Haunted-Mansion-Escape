using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireChange : MonoBehaviour
{
    public bool burned = false;
    public AudioSource kingYell;
    public GameObject King;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("King"))
        	{
				StartCoroutine(DelayedSignal());
            	
        	}
		
	}

    private IEnumerator DelayedSignal()
	{
		//Wait for the delay
        kingYell.Play();
		yield return new WaitForSeconds(2);
        burned = true;
        Destroy(King);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
