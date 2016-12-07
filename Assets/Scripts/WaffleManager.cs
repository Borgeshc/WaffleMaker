using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaffleManager : MonoBehaviour
{
    public List<GameObject> familyMembers;
    public GameObject waffle;
    int chooseMember;
    float lowestHunger = 1;

    public void WaffleReady()
    {
       // CheckHunger();

        for (int i = 0; i < familyMembers.Count; i++)
        {
            if(familyMembers[i].GetComponent<MyHungerBar>().hungerBar.GetComponent<Image>().fillAmount < lowestHunger)
            {
                lowestHunger = familyMembers[i].GetComponent<MyHungerBar>().hungerBar.GetComponent<Image>().fillAmount;
                chooseMember = i;
            }
        }

        familyMembers[chooseMember].GetComponent<Hunger>().Eat();
        Reset();
    }

    void Reset()
    {
        chooseMember = 0;
        lowestHunger = 1;
        Destroy(waffle);
        waffle = null;
    }

    //void CheckHunger()
    //{
    //    for (int i = 0; i < familyMembers.Count; i++)
    //    {
    //        if (familyMembers[i].GetComponent<MyHungerBar>().hungerBar.GetComponent<Image>().fillAmount <= 1.5f)
    //        {
    //            familyMembers.Remove(familyMembers[i]);
    //        }
    //    }
    //}
}
