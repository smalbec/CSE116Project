using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TheWig : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PrefabUtility.InstantiatePrefab(Resources.Load("Player") as GameObject);
    }
    //Summary:
    //This creates the player upon pressing play.
    //It loads the prefab from the Resources folder, then creates a new instance of it.
    //Unlike the other method, this one is not a clone of the main player class.
    //However, it seems they share some properties, primarily speed and bullet properties. 
}
