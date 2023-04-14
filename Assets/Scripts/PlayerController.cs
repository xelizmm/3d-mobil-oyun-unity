using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public float speed = 5f;
    CurrentDirection cr;
    public bool isPlayerDead;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cr = CurrentDirection.left;
        isPlayerDead = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerDead)
        {
            RayCastDtector();

            //touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began
            if (Input.GetKeyDown("f1"))
            {
                ChangeDirection();
                PlayerStop();
            }
            else
            {
                return;
            }
        }
    }

    private void RayCastDtector()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position,Vector3.down, out hit))
        {
            MovePlayer();
        }
        else
        {
            PlayerStop();
            isPlayerDead = true;
            this.gameObject.SetActive(false);
            gameManager.LevelEnd();
        }
    }


    private enum CurrentDirection
    {
        right,
        left
    }

    private void ChangeDirection()
    {
        MovePlayer();

        if (cr ==  CurrentDirection.right)
        {
            cr = CurrentDirection.left;
        }
        else if (cr == CurrentDirection.left)
        {
            cr = CurrentDirection.right;
        }
    }

    private void MovePlayer()
    {
        if (cr == CurrentDirection.right)
        {
            rb.AddForce(Vector3.forward.normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (cr == CurrentDirection.left)
        {
            rb.AddForce((Vector3.right).normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    private void PlayerStop()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndTrigger"))
        {
            gameManager.WinLevel();
            PlayerStop();
            this.gameObject.SetActive(false);
            
        }
    }
}
