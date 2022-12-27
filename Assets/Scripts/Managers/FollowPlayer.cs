using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerPos;

    private void Start()
    {
        playerPos = FindObjectOfType<PlayerActions>().transform;
    }

    private void Update()
    {
        transform.position = playerPos.position;
    }
}
