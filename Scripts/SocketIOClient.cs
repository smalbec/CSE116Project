using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace wig {
    public class SocketIOClient : MonoBehaviour {
        Socket socket;
        public GameObject Player;

        //dictionary made that maps server id to game objects
        private Dictionary<string, GameObject> serverObjects = new Dictionary<string, GameObject>();
        //scuffed list to allow gameobjects to spawn
        private List<string> idList = new List<string>();

        public static Dictionary<string, Vector2> velocities = new Dictionary<string, Vector2>();

        private Dictionary<string, List<Dictionary<string, string>>> gamestate = new Dictionary<string, List<Dictionary<string, string>>>();

        private List<JObject> movements = new List<JObject>();

        public static string currentPlayer = null;

        public static Dictionary<string, GameObject> eggs = new Dictionary<string, GameObject>();

        public static Dictionary<string, bool> eggCheck = new Dictionary<string, bool>();

        public static bool eggState = false;

        public static Dictionary<string, float> eggHP = new Dictionary<string, float>();

        [Header("Network Client")]
        [SerializeField]
        public Transform networkContainer;

        void Start()
        {
            gamestate.Add("players", new List<Dictionary<string, string>>());
                if (socket == null)
                {
                    socket = IO.Socket("http://127.0.0.1:5000");
                    socket.On(Socket.EVENT_CONNECT, () =>
                    {
                        Debug.Log("connection");

                        //Instantiate(body, new Vector2(10f, 10f), Quaternion.identity);

                        socket.On("register", (data) => {
                            Debug.LogFormat("Data ID is: " + (data as JObject)["id"].ToString());
                    
                        });
                        socket.On("spawn", (data) => {
                            string id = (data as JObject)["id"].ToString();

                            Debug.Log("Spawning Player: " + id);
                            //GameObject go = new GameObject("Player ID " + id); //create object -- code halts at this point for some reason 
                            idList.Add(id);
                            Debug.Log("Player Spawned: " + id);
                            //go.transform.SetParent(networkContainer); //add object to container so that the scene doesnt look fucky wucky
                            //serverObjects.Add(id,go); //add server object to dictionary
                    
                        });
                        socket.On("disconnected", (data) => { // don't know how to debug for now :shrug:
                            string id = (data as JObject)["id"].ToString();

                            GameObject go = serverObjects[id]; //retrieve object from dictionary
                            Destroy(go); //destroy gameobject from scene
                            serverObjects.Remove(id); //remove id from dictionary
                            Debug.Log("Player Disconnected: " + id);
                        });
                        socket.On("gs", (data) =>
                        {
                            //Debug.Log(data);
                            var meme = (data as JObject);
                            movements.Add(meme);
                        });
                        socket.On("update-eggs", (data) =>
                        {
                            Debug.Log("eggs checked!");
                            var meme = (data as JObject);
                            foreach (string e in eggCheck.Keys)
                            {
                                eggCheck[e] = Convert.ToBoolean(meme[e].ToString());
                            }
                        });
                    });
                
                }
            StartCoroutine(BeginLoop());
        }

        void addGS(GameObject go, Socket socket)
        {
            var boyPlayer = go.GetComponent<Player>();
            var incomingBoy = new Dictionary<string, string>();
            //Debug.Log(boyPlayer.transform.position);
            incomingBoy.Add("id", go.name);
            incomingBoy.Add("x", boyPlayer.transform.position.x.ToString());
            incomingBoy.Add("y", boyPlayer.transform.position.y.ToString());
            socket.Emit("append-player", JsonConvert.SerializeObject(incomingBoy));
        }

        void updateGS(Socket socket)
        {
            foreach (string o in serverObjects.Keys)
            {
                var boyPlayer = serverObjects[o].GetComponent<Player>();
                if (boyPlayer.name == currentPlayer)
                {
                    Debug.Log(boyPlayer.name + " is " + currentPlayer);
                    var incomingBoy = new Dictionary<string, string>();
                    incomingBoy.Add("id", o);
                    incomingBoy.Add("x", boyPlayer.transform.position.x.ToString());
                    incomingBoy.Add("y", boyPlayer.transform.position.y.ToString());
                    socket.Emit("send-gs", JsonConvert.SerializeObject(incomingBoy));
                }
            }
            foreach (string e in eggs.Keys)
            {
                if (eggCheck[e] == false && eggs.ContainsKey(e))
                {
                    Debug.Log(e + " destroyed!");
                    eggs[e].SetActive(false);
                }
            }
            socket.Emit("update-gs", JsonConvert.SerializeObject(currentPlayer));
            socket.Emit("get-eggs");
        }

        IEnumerator BeginLoop()
        {
            while (true)
            {
                if(idList.Count != 0) //if any clients registered
                {
                    var ide = idList[0]; //take the head of the list
                    //Debug.Log("BigYeet!" + ide);
                    //GameObject go = new GameObject("Player ID: " + ide); //make a gameobject using the id found at the head
                    GameObject go = Instantiate(Player);
                    go.name = ide;
                    if (serverObjects.Count == 0)
                    {
                        currentPlayer = ide;
                        //Debug.Log("the player is " + currentPlayer);
                    }
                    //go.transform.SetParent(networkContainer); //add object to container so that the scene doesnt look fucky wucky
                    serverObjects.Add(ide,go); //add server object to dictionary
                    velocities.Add(ide, new Vector2(0, 0));
                    addGS(go, socket);
                    go.transform.SetParent(networkContainer); //add object to container so that the scene doesnt look fucky wucky
                    StartCoroutine(Applyer());
                    StartCoroutine(Updater());
                    idList.Remove(idList[0]); //remove it from the scuffed list, on wig.
                }
                yield return new WaitForSeconds(0.05F); //50 millisecond delay   
            }
        }

        IEnumerator Applyer()
        {
            while (true)
            {
                if (movements.Count != 0)
                {
                    //Debug.Log("fuck me" + movements.Count);
                    if (serverObjects.ContainsKey(movements[0]["id"].ToString()))
                    {
                        //Debug.Log("moved " + movements[0]["id"].ToString() + " from " + serverObjects[movements[0]["id"].ToString()].GetComponent<Player>().transform.position.x + " to " + movements[0]["x"].ToString());
                        serverObjects[movements[0]["id"].ToString()].GetComponent<Player>().transform.SetPositionAndRotation(new Vector3(float.Parse(movements[0]["x"].ToString()), float.Parse(movements[0]["y"].ToString()), 0), Quaternion.identity);
                    }
                    movements.Remove(movements[0]);
                }
                yield return new WaitForSeconds(0.05F);
            }
        }

        IEnumerator Updater()
        {
            while (true)
            {
                updateGS(socket);
                if (eggState)
                {
                    updateEggState(socket);
                    eggState = false;
                }
                yield return new WaitForSeconds(0.5F);
            }
        }

        static void updateEggState(Socket socket)
        {
            socket.Emit("remove-egg", JsonConvert.SerializeObject(eggCheck));
            Debug.Log(eggHP);
        }
    } 
}