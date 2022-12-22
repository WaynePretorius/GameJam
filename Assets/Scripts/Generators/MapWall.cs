using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWall : MonoBehaviour
{
    [SerializeField] private Collider2D leftWall;
    [SerializeField] private Collider2D rightWall;
    [SerializeField] private Collider2D topWall;
    [SerializeField] private Collider2D lowerWall;

    public void SetWall(int width, int height)
    {
        int Xpos = width / 2;
        int Ypos = height / 2;

        leftWall.gameObject.transform.position = new Vector3(-Xpos, 0, 0);
        rightWall.gameObject.transform.position = new Vector3(Xpos, 0, 0);
        lowerWall.gameObject.transform.position = new Vector3(0, -Ypos, 0);
        topWall.gameObject.transform.position = new Vector3(0, Ypos, 0);
    }
}
