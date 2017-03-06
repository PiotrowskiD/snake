using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

    public GameObject foodPrefab;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

	// Use this for initialization
	void Start ()
    {
        borderTop = GameObject.FindGameObjectWithTag("top").transform;
        borderBottom = GameObject.FindGameObjectWithTag("bottom").transform;
        borderRight = GameObject.FindGameObjectWithTag("right").transform;
        borderLeft = GameObject.FindGameObjectWithTag("left").transform;
        Debug.Log("start");
    }
	void Spawn()
    {
        GameObject newFood;

        
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);

        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);
        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }

    private void OnDestroy()
    {

        Spawn();
    }
}
