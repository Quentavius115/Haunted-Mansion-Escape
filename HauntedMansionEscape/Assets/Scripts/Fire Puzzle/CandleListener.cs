using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleListener : MonoBehaviour
{
    public GameObject checkCube;
    public List<CandelManager> allCandles = new List<CandelManager>();

    // Start is called before the first frame update
    void Start()
    {
        allCandles.AddRange(FindObjectsOfType<CandelManager>());
        checkCube.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAllCandlesLit();
    }

    void CheckAllCandlesLit()
    {
        bool allStateSatisfied = true;

        foreach (var candle in allCandles)
        {
            if (candle.isSatisfied != true)
            {
                allStateSatisfied = false;
                break;
            }
        }

        if (allStateSatisfied)
        {
            checkCube.GetComponent<MeshRenderer>().material.color = Color.green;
            Debug.Log("All candles are lit, cube is now green!");
        }
    }
}
