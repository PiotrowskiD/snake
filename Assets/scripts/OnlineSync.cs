using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineSync : Photon.MonoBehaviour {

    public Vector2 realDirection = Vector2.zero;
    public Snake snakeHead;
    private void Start()
    {
        PhotonNetwork.sendRate = 20;
        PhotonNetwork.sendRateOnSerialize = 20;
        snakeHead = GetComponent<Snake>();
    }

    private void FixedUpdate()
    {
        SmoothMovement();
    }

    void SmoothMovement()
    {
        if(photonView.isMine)
        {

        }
        else
        {
            // transform.position = Vector2.Lerp(transform.position, realPosition, Time.deltaTime * 5);
            //transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, Time.deltaTime * 5);

            //transform.position = realPosition;
            //transform.rotation = realRotation;
            
            snakeHead.serverDirection = realDirection;
        }
    }

   
    void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        
        if(stream.isWriting)
        {
            stream.SendNext(snakeHead.direction);
           
        }
        else
        {
            realDirection = (Vector2)stream.ReceiveNext();
        }
    }
}
