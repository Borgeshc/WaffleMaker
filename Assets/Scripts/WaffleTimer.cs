using UnityEngine;
using UnityEngineInternal;
using System.Collections;

public class WaffleTimer : MonoBehaviour {
    public GameObject wafflePrefab;
    public GameObject wafflePourPanel;
    public GameObject waffleContainer;
    public AudioClip dingSound;
    public AudioClip sizzle;
    public Animator wafflePourPanelAnimator;
    AudioSource source;
    bool runningTimer;
    bool cooking;
    bool cooked;
    bool overCooked;
    int time;
    int overcookedTime;
    int timeCooked;
    GameObject currentWaffle;

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
        if (ArduinoSerial.isPressed)
        {

            if(currentWaffle == null)
            {
                source.clip = sizzle;
                source.Play();
                wafflePourPanelAnimator.SetBool("isPouring", true);

                currentWaffle = Instantiate(wafflePrefab, waffleContainer.transform) as GameObject;
                time = Random.Range(2, 7);
                overcookedTime = time + 5;
            }

            cooking = true;
        }
        else
        {
            if (cooked)
            {
                GetComponent<WaffleManager>().WaffleReady();
                wafflePourPanelAnimator.SetBool("isPouring", false);
                currentWaffle = null;
                cooked = false;
                time = 0;
                overcookedTime = 0;                
                timeCooked = 0;
                overCooked = false;                
            }

            runningTimer = false;
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
            
            //for(int i = overcookedTime; i > 0; i--)
            while(cooking)
            {
                //print("Time Cooked: " + timeCooked);
                if (timeCooked >= time && timeCooked < overcookedTime)      //waffle is cooked
                {
                    cooked = true;
                    source.PlayOneShot(dingSound);
                    currentWaffle.GetComponent<WaffleStatus>().currentStatus = WaffleStatus.Status.Cooked;
                    GetComponent<WaffleManager>().waffle = currentWaffle;
                }
                else if (timeCooked <= time)                                //waffle is undercooked
                {
                    cooked = false;
                }
                else if (timeCooked > overcookedTime)                       //waffle is overcooked
                {
                    overCooked = true;
                    currentWaffle.GetComponent<WaffleStatus>().currentStatus = WaffleStatus.Status.Overcooked;
                    GetComponent<WaffleManager>().waffle = currentWaffle;
                }
                yield return new WaitForSeconds(1f);
                timeCooked++;
            }
            runningTimer = false;
        }
    }
}