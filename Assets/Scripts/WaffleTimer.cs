using UnityEngine;
using UnityEngineInternal;
using System.Collections;

public class WaffleTimer : MonoBehaviour {
    public GameObject wafflePrefab;
    public GameObject wafflePourPanel;
    public AudioClip dingSound;
    AudioSource source;
    bool runningTimer;
    bool cooking;
    bool cooked;
    bool overCooked;
    int time;
    int overcookedTime;
    int timeCooked;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        if(source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //print("Cooking: " + cooking);
        //print("Time: " + time);

        if (Input.GetKey(KeyCode.Space))
            cooking = true;
        else
        {
            if(cooked)
            {
                GetComponent<WaffleManager>().WaffleReady();
            }

            cooked = false;
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
            time = Random.Range(2, 7);
            overcookedTime = time + 5;
            //for(int i = overcookedTime; i > 0; i--)
            while(cooking)
            {
                //print("Time Cooked: " + timeCooked);
                if (timeCooked >= time && timeCooked < overcookedTime)
                {
                    cooked = true;
                    source.PlayOneShot(dingSound);
                }
                else if (timeCooked <= time)
                {
                    cooked = false;
                    //print("Waffle Undercooked");
                }
                else if (timeCooked > overcookedTime)
                {
                    overCooked = true;
                    //print("Waffle Overcooked");
                }
                yield return new WaitForSeconds(1f);
                timeCooked++;
            }
            runningTimer = false;
        }
    }
}