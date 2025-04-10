using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLore : MonoBehaviour
{
	public AudioSource success;
    public AudioSource Record1;
    public AudioSource Record2;
    public enum ObjectType { RecordSuccess, Record1, Record2}
    public ObjectType objectType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPlaced(){
        switch(objectType){
            case ObjectType.RecordSuccess:
            if (success != null){
                success.Play();
            }
            break;
            
            case ObjectType.Record1:
            if (Record1 != null){
                Record1.Play();
            }
                break;
            
            case ObjectType.Record2:
            if(Record2 != null){
                Record2.Play();
            }
                break;
        }
    }

    public void OnRemoved(){
        switch(objectType){
            case ObjectType.RecordSuccess:
                if (success.isPlaying){
                    success.Stop();
                }
                break;

            case ObjectType.Record1:
                if (Record1.isPlaying){
                    Record1.Stop(); 
                }
                break;

            case ObjectType.Record2:
                if (Record2.isPlaying){
                    Record2.Stop();
                }
                break;
            default:
                break;
        }
    }
}
