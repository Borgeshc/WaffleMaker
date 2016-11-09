using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaffleManager : MonoBehaviour
{
    public List<GameObject> familyMembers;
    int chooseMember;
    float lowestHunger = 1;

    public void WaffleReady()
    {
        CheckHunger();
        //Reset();

        for (int i = 0; i < familyMembers.Count; i++)
        {
            if(familyMembers[i].GetComponent<Hunger>().hungerValue < lowestHunger)
            {
                lowestHunger = familyMembers[i].GetComponent<Hunger>().hungerValue;
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
    }

    void CheckHunger()
    {
        for (int i = 0; i < familyMembers.Count; i++)
        {
            if (familyMembers[i].GetComponent<Hunger>().hungerValue <= 0)
            {
                familyMembers.Remove(familyMembers[i]);
            }
        }
    }
}
