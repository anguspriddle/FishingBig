using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    Dictionary<string, Vector3> gridPositions = new Dictionary<string, Vector3>();

    void Start()
    {
        // Setting up grid square distances
        gridPositions.Add("a1", new Vector3(40f, 0f, -40f));
        gridPositions.Add("a2", new Vector3(40f, 0f, -20f));
        gridPositions.Add("a3", new Vector3(40f, 0f, 0f));
        gridPositions.Add("a4", new Vector3(40f, 0f, 20f));
        gridPositions.Add("a5", new Vector3(40f, 0f, 40f));

        gridPositions.Add("b1", new Vector3(20f, 0f, -40f));
        gridPositions.Add("b2", new Vector3(20f, 0f, -20f));
        gridPositions.Add("b3", new Vector3(20f, 0f, 0f));
        gridPositions.Add("b4", new Vector3(20f, 0f, 20f));
        gridPositions.Add("b5", new Vector3(20f, 0f, 40f));

        gridPositions.Add("c1", new Vector3(0f, 0f, -40f));
        gridPositions.Add("c2", new Vector3(0f, 0f, -20f));
        gridPositions.Add("c3", new Vector3(0f, 0f, 0f));
        gridPositions.Add("c4", new Vector3(0f, 0f, 20f));
        gridPositions.Add("c5", new Vector3(0f, 0f, 40f));

        gridPositions.Add("d1", new Vector3(-20f, 0f, -40f));
        gridPositions.Add("d2", new Vector3(-20f, 0f, -20f));
        gridPositions.Add("d3", new Vector3(-20f, 0f, 0f));
        gridPositions.Add("d4", new Vector3(-20f, 0f, 20f));
        gridPositions.Add("d5", new Vector3(-20f, 0f, 40f));

        gridPositions.Add("e1", new Vector3(-40f, 0f, -40f));
        gridPositions.Add("e2", new Vector3(-40f, 0f, -20f));
        gridPositions.Add("e3", new Vector3(-40f, 0f, 0f));
        gridPositions.Add("e4", new Vector3(-40f, 0f, 20f));
        gridPositions.Add("e5", new Vector3(-40f, 0f, 40f));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
