using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChess : MonoBehaviour
{
    public GameObject rook1;
    public GameObject rook2;
    public GameObject blackKing;
    public GameObject whiteKing;
    Vector3 originalPosRook1;
    Vector3 originalPosRook2;
    Vector3 originalPosBlackKing;
    Vector3 originalPosWhiteKing;
    // Start is called before the first frame update
    void Start()
    {
        originalPosRook1 = rook1.transform.position;
        originalPosRook2 = rook1.transform.position;
        originalPosBlackKing = rook1.transform.position;
        originalPosWhiteKing = rook1.transform.position;
    }

    // Update is called once per frame
    public void Reset()
    {
        rook1.transform.position = originalPosRook1;
        rook2.transform.position = originalPosRook2;
        blackKing.transform.position = originalPosBlackKing;
        whiteKing.transform.position = originalPosWhiteKing;
        Debug.Log("pressed");
    }
}
