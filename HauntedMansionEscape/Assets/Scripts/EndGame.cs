using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject player;
    public Transform playerCam;
    public Image blackScreen;
    float timeLimit = 9f * 60;
    public bool gameIsOver = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = player.transform.Find("Camera Offset").Find("Main Camera");
        StartCoroutine(TimerCoroutine());
        StartCoroutine(RandomSound());
    }

    void Update(){
        if (gameIsOver)
            GameOver();
    }
    private System.Collections.IEnumerator TimerCoroutine()
    {

        // Add button check here to skip the return loop?
        yield return new WaitForSeconds(timeLimit);

        // Do something after time runs out
        Debug.Log("Timer finished!");
        gameIsOver = true;
    }

    private IEnumerator RandomSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(30f, 60f));
            SoundManager.Instance.Play(SoundType.AMBIENCE);
        }
    }

    void GameOver()
    {
        if (!blackScreen.gameObject.activeInHierarchy) 
            blackScreen.gameObject.SetActive(true);

        if (blackScreen.gameObject.activeInHierarchy){
            blackScreen.transform.position = playerCam.position + playerCam.forward * 2f;
            blackScreen.transform.rotation = Quaternion.LookRotation(blackScreen.transform.position - playerCam.position);

            if (blackScreen.color.a < 1){
                Debug.Log("FADE OOUT");
                Color temp = blackScreen.color;
                temp.a += Time.deltaTime * 1f / 1f; // right number is second
                blackScreen.color = temp;
            }
            if (blackScreen.color.a >= 1){
                SceneManager.LoadScene("Cellar");
            }
        }
    }
}


