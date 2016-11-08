using UnityEngine;
using UnityEngineInternal;
using System.Collections;

public class WaffleTimer : MonoBehaviour {

    bool runningTimer;
    bool cooking;
    bool cooked;
    bool overCooked;
    int time;
    int overcookedTime;
    int timeCooked;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        print("Cooking: " + cooking);
        print("Time: " + time);

        if (Input.GetKey(KeyCode.Space))
            cooking = true;
        else
        {            
            time = 0;
            overcookedTime = 0;
            runningTimer = false;
            timeCooked = 0;
            overCooked = false;
            StopAllCoroutines();
            cooking = false;

        }

	    if(cooking)
        {
            CallCoroutine("CookingTimer");
        }
	}

    void CallCoroutine(string name)
    {
        StartCoroutine(name);
    }

    IEnumerator CookingTimer()
    {
        if(!runningTimer)
        {
            runningTimer = true;
            time = Random.Range(5, 15);
            overcookedTime = time + 5;
            //for(int i = overcookedTime; i > 0; i--)
            while(cooking)
            {
                print("Time Cooked: " + timeCooked);
                if (timeCooked >= time && timeCooked < overcookedTime)
                {
                    cooked = true;
                    print("Waffle Cooked");
                }
                else if (timeCooked <= time)
                {
                    cooked = false;
                    print("Waffle Undercooked");
                }
                else if (timeCooked > overcookedTime)
                {
                    overCooked = true;
                    print("Waffle Overcooked");
                }
                yield return new WaitForSeconds(1f);
                timeCooked++;
            }
            runningTimer = false;
        }
    }
}