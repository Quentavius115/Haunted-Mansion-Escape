using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    float timeLimit = 15f * 60;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    private System.Collections.IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(timeLimit);

        // Do something after time runs out
        Debug.Log("Timer finished!");
        GameOver();
    }

    void GameOver()
    {
        SceneManager.LoadScene("Actual Scene");
    }
}


