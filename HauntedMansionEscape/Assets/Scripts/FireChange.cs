using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireChange : MonoBehaviour
{
    public bool burned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("King"))
        	{
				StartCoroutine(DelayedSignal());
            	
        	}
		
	}

    private IEnumerator DelayedSignal()
	{
		//Wait for the delay
		yield return new WaitForSeconds(2);
        burned = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
