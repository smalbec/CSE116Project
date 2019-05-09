using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator respawn(GameObject prefab, Vector3 position)
    {
        Debug.Log("start timer");
        yield return new WaitForSeconds(5);
        Debug.Log("time has passed");
        Instantiate(prefab, position , Quaternion.identity);
        Debug.Log("wall created");

    }

}
