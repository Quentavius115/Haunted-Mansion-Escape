using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public PianoPuzzle puzzle;
    public Note note;
    public AudioSource audioSrc;
    public AudioClip clip;
    public AudioClip counterPoint;
    public Note lastNote;
    GameObject presser;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            presser = collision.gameObject;
            onPress.Invoke();
            audioSrc.Play();
            Debug.Log("Collision");
            if ((lastNote == Note.A5) && (note == Note.F4))
            {
                audioSrc.Stop();
                audioSrc.PlayOneShot(counterPoint, 1.0f);
            }
        }
    }

    public void Press()
    {
        if (puzzle.State != PianoState._FINAL_KEY_F)
        {
            puzzle.Press(note);
        }
        else
        {
            audioSrc.Play();
        }
    }
}
