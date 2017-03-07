﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snake : Photon.MonoBehaviour 
{
   

    public bool start = false;
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    public GameObject tailPrefab;
    public Text scoreText;
    public Text loseText;

    private int score = 0;
    Vector2 direction = Vector2.right;
    Vector2 lastDirection = Vector2.right;

    List<Transform> tail = new List<Transform>();


    bool ate = false;

    void Awake()
    {
        photonView.RPC("ChangeMyName", PhotonTargets.AllBuffered, PhotonNetwork.playerList.Length.ToString());
    }

    void Start () 
	{


        InvokeRepeating("Move", 0.05f, 0.05f);	
	}
	
	void Update () 
	{
        if(this.isActiveAndEnabled)
        {
            start = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && lastDirection != Vector2.left)
        {
            direction = Vector2.right;
        }
        else if(Input.GetKey(KeyCode.LeftArrow) && lastDirection != Vector2.right)
        {
            direction = -Vector2.right;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && lastDirection != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && lastDirection!= Vector2.up)
        {
            direction = -Vector2.up;
        }
        else if (Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 1;
            loseText.text = "";
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }


        scoreText.text = "Wynik: " + score.ToString();
    }

    void Move()
    {
        if(start)
        {
            Vector2 currentHeadPosition = transform.position;
            transform.Translate(direction);
            lastDirection = direction;

            if (ate)
            {
                GameObject prefab = (GameObject)Instantiate(tailPrefab, currentHeadPosition, Quaternion.identity);

                tail.Insert(0, prefab.transform);

                ate = false;
            }

            else if (tail.Count > 0)
            {
                tail.Last().position = currentHeadPosition;
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.StartsWith("FoodPrefab"))
        {
            ate = true;
            score++;
            Spawn(collision.gameObject);

        }
        else
        {
            loseText.text = "You lost, press r to restart";
            Time.timeScale = 0;
        }
    }

    void Spawn(GameObject food)
    {


        int x = (int)Random.Range(borderLeft.position.x+1, borderRight.position.x-1);

        int y = (int)Random.Range(borderBottom.position.y+1, borderTop.position.y-1);
        food.transform.position = new Vector3(x, y);
    }
}
