using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    //Variables Declared
    [SerializeField] private float speed = 1f;

    //Cached References
    [Tooltip("The body of the player, for rotation purposes")]
    [SerializeField] private GameObject playerBody;
    [SerializeField] private AudioClip clip;
    private Animator myAnimator;
    private Rigidbody2D myBody2D;
    private AudioSource mySFX;


    [SerializeField] private GameObject npcInteractions;

    private bool isInteractableR = false;
    private bool isInteractableF = false;

    private bool inShop = false;

    /// <summary>
    /// setters for any private variables
    /// </summary>

    public void SetIsInteractableR(bool isR)
    {
        isInteractableR = isR;
    }

    public void SetInShop(bool inShopnow)
    {
        inShop = inShopnow;
    }

    public bool GetInteractableR()
    {
        return isInteractableR;
    }

    public void SetInteractableF(bool isF)
    {
        isInteractableF = isF;
    }

    public bool GetInteractableF()
    {
        return isInteractableF;
    }

    //first function when the object is enabled
    private void Awake()
    {
        InitialReferences();
    }

    //Contains all reference initializations
    private void InitialReferences()
    {
        myAnimator = GetComponentInChildren<Animator>();
        myBody2D = GetComponent<Rigidbody2D>();
        mySFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    /// <summary>
    /// moves the player in the direction of the axis that was pressed, and using those axis, calls the animate and rotate function
    /// </summary>
    private void MovePlayer()
    {
        if (!inShop)
        {
            IdleOrWalkAnimations(Input.GetAxis(Tags.AXIS_HORIZONTAL), Input.GetAxis(Tags.AXIS_VERTICAL));
            RotateCharacter(Input.GetAxis(Tags.AXIS_HORIZONTAL));
            float moveHorizontal = Input.GetAxis(Tags.AXIS_HORIZONTAL) * speed * Time.deltaTime;
            float moveVertical = Input.GetAxis(Tags.AXIS_VERTICAL) * speed * Time.deltaTime;

            float newPosY = transform.position.y + moveVertical;
            float newPosX = transform.position.x + moveHorizontal;

            transform.position = new Vector2(newPosX, newPosY);
        }
    }

    /// <summary>
    /// moves the character according to the input given
    /// </summary>
    /// <param name="xAxis">horizontal axis</param>
    /// <param name="yAxis">vertical axis</param>
    private void IdleOrWalkAnimations(float xAxis, float yAxis)
    {
        if (xAxis > 0.1 || xAxis < -0.1 || yAxis > 0.1 || yAxis < -0.1)
        {
            myAnimator.Play(Tags.ANIM_WALK);
        }
        else
        {
            myAnimator.Play(Tags.ANIM_IDLE);
        }
    }

    public void SetplayerInteraction(GameObject triggerDetection)
    {
        npcInteractions = triggerDetection;
    }

    /// <summary>
    /// rotates the character according to the axis that is used
    /// </summary>
    /// <param name="xAxis">horizontal axis</param>
    private void RotateCharacter(float xAxis)
    {
        if (xAxis < 0)
        {
            transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
        else if (xAxis > 0)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
