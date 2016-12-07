using UnityEngine;
using System.Collections;

public class HandLocation : MonoBehaviour {

    public bool gameOn;
    public BodySourceView bSV;
    public GameObject hand;
    public Vector3 targetLocation;
    public GameObject syrupBottle;
    public float syrupRange;
    public bool holdingSyrup;
    public float syrupTimer;
    public float timeInRange;
    public float syrupPickupTime;
    public bool syrupPoured;
    public bool handLeftTarget;
    public Vector3 aboveWaffle;
    public float waffleDiameter;
    

	// Use this for initialization
	void Start () {

        bSV = FindObjectOfType<BodySourceView>();
        holdingSyrup = false;
        syrupPoured = false;
        handLeftTarget = true;
        gameOn = false;

	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameOn)
            {
                gameOn = true;
                targetLocation = bSV.handLocation;
                aboveWaffle.x = bSV.headLocation.x;
            }
            else
            {
                gameOn = false;
                //reset game or change to main menu
            }
        }

        if (gameOn)
        {
            hand.transform.position = bSV.handLocation;
            if (Vector3.Distance(bSV.handLocation, targetLocation) <= syrupRange)
            {
                timeInRange += Time.deltaTime;
                if ((timeInRange > syrupPickupTime) && handLeftTarget)
                {
                    if (!holdingSyrup && !syrupPoured)
                    {
                        holdingSyrup = true;
                        timeInRange = 0;
                        handLeftTarget = false;
                    }
                    else if (holdingSyrup && syrupPoured)
                    {
                        holdingSyrup = false;
                        syrupPoured = false;
                        timeInRange = 0;
                        handLeftTarget = false;
                    }
                }
            }
            else
            {
                handLeftTarget = true;
            }

            if (holdingSyrup)
            {
                syrupBottle.transform.position = bSV.handLocation;
                if (syrupBottle.transform.position.x > (aboveWaffle.x - waffleDiameter) && syrupBottle.transform.position.x < (aboveWaffle.x + waffleDiameter))
                {
                    syrupPoured = true;
                    //print("pouring syrup");
                }
            }
            else
            {
                syrupBottle.transform.position = targetLocation;
            }

        }
    }
}
