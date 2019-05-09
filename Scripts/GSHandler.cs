using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace rude
{
    public class GSHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        public static GameObject gamer;
        //private System.Collections.Generic.List<string> inActionIDs = new System.Collections.Generic.List<string>();
        void Start()
        {

        }
        public static void UpdateGS()
        {
            //updates each active player's x/y to what it is in the gamestate
            //Globals.activePlayers[Globals.playerId].GetComponent<Player>().moveVelocity.x = 2;

        }
        //Summary:
        //This creates the player upon pressing play.
        //It loads the prefab from the Resources folder, then creates a new instance of it.
        //Unlike the other method, this one is not a clone of the main player class.
        //However, it seems they share some properties, primarily speed and bullet properties. 
        public static void getGoing()
        {
            //PrefabUtility.InstantiatePrefab(gamer);
        }
    }
}
