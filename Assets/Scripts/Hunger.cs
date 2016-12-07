using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    public Image hungerBar;
    public GameObject eyes;
    public AudioClip deathSound;

    [Range(1,10)]
    public int minHungerTime;
    [Range(1,10)]
    public int maxHungerTime;
    [Range(.01f, 1), Tooltip("How much 1 waffle would fill the hunger bar.")]
    public float waffleAmount;
    [Range(.01f, 1), Tooltip("How much will the hunger bar go down everytime the person gets hungry.")]
    public float hungerAmount;
    [HideInInspector]
    public float hungerValue;

    AudioSource source;
    Animator anim;
    int hungerTime;
    float timer;
    bool isStarving;
    bool deathSoundPlayed;

    void Start()
    {
        hungerTime = Random.Range(minHungerTime, maxHungerTime);
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        source.clip = deathSound;
        hungerValue = hungerBar.fillAmount;
    }

	void Update ()
    {
        timer = (int)Time.time;
       
	    if(timer % hungerTime == 0 && timer != 0 && !isStarving)
        {
            StartCoroutine(Starving());
        }
	}

    IEnumerator Starving()
    {
        isStarving = true;
        hungerBar.fillAmount = hungerBar.fillAmount - hungerAmount;
        hungerValue = hungerBar.fillAmount;
        yield return new WaitForSeconds(1);
        hungerTime = Random.Range(minHungerTime, maxHungerTime);
        isStarving = false;

        if(hungerBar.fillAmount <= 0)
        {
            eyes.SetActive(true);
            anim.SetBool("died", true);
            if (!deathSoundPlayed)
            {
                source.Play();
                deathSoundPlayed = true;
            }
            Destroy(gameObject, 7);
        }
    }

    public void Eat()
    {
        hungerBar.fillAmount = hungerBar.fillAmount + waffleAmount;
    }
}