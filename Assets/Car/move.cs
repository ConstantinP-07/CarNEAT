using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.Networking;

public class move : NetworkBehaviour
{
    #region Variables
    [Range(-1, 1)]
    public int Horizontale;
    [Range(-1, 1)]
    public int Verticale;
    public bool playerControlled;

    public float maxSpeed;
    public float maxBackSpeed;
    [SerializeField]
    private float currentSpeed;
    public float turnSpeed = 20f;

    public float acceleration;
    public float friction;
    public float brakeSpeed;

    [SerializeField]
    private Rigidbody2D rb;
    #endregion    

    [SerializeField]
    private GameObject finishUI;
    [SerializeField]
    private Text finishTime;
    private float time;

    [SerializeField]
    private GameObject[] spawnPoints;

    public void FixedUpdate()
    {
        time += Time.deltaTime;

            if(currentSpeed != 0)
            {
                transform.Rotate(Vector3.back * turnSpeed * Time.deltaTime * Horizontale);
            }

            if(Verticale != Mathf.Sign(currentSpeed) && currentSpeed != 0 && Verticale != 0)
            {
                float braking = Mathf.Clamp(currentSpeed - brakeSpeed * Time.deltaTime * Mathf.Sign(currentSpeed), maxBackSpeed, maxSpeed);
                if (braking * currentSpeed < 0)
                    braking = 0;
                currentSpeed = braking;
            }

                if (Verticale != 0)
                    currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime * Verticale, maxBackSpeed, maxSpeed);
                else {
                    if (currentSpeed != 0)
                    {
                        float newSpeed = Mathf.Clamp(currentSpeed - friction * Time.deltaTime * Mathf.Sign(currentSpeed), maxBackSpeed, maxSpeed);
                        if (newSpeed * currentSpeed < 0)
                            newSpeed = 0;
                        currentSpeed = newSpeed;
                    }
                }
            
            rb.velocity = transform.up * currentSpeed;
        if (playerControlled)
        {
            Horizontale = (int)Input.GetAxisRaw("Horizontal");
            Verticale = (int)Input.GetAxisRaw("Vertical");
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Finish"))
        {
            finishUI.SetActive(true);
            finishTime.text = Mathf.RoundToInt(time) + "s";
        }
    }

    IEnumerator HideUI()
    {
        int playerID = int.Parse(GetComponent<NetworkIdentity>().netId);

        GetComponent<move>().enabled = false;
        yield return new WaitForSeconds(3f);
        GetComponent<move>().enabled = true;

        for (int i = 0; i < length; i++)
        {

        }

        time = 0;
        finishUI.SetActive(false);
    }
}
