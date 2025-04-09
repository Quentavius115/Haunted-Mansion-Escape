using System.Collections;
using System.Collections.Generic;
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
    GameObject presser;
    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Debug.Log("accepted");
            if (!isPressed)
            {
                button.transform.localPosition = new Vector3(0, 0.003f, 0);
                presser = collision.gameObject;
                onPress.Invoke();
                audioSrc.PlayOneShot(clip, 1.0f);
                isPressed = true;
            }
        }
    }

    private void OnCollisionExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (collision == presser)
            {
                button.transform.localPosition = new Vector3(0, 0.015f, 0);
                onRelease.Invoke();
                isPressed = false;
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

    public void printHi()
    {
        Debug.Log("Hello");
        }
    }
