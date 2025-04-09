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

        // Add logic for checking if book is satisfied by being in correct snapped place 
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
        }
    }
}
