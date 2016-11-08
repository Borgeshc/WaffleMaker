using UnityEngine;
using System.Collections;

public class WaffleManager : MonoBehaviour
{
    public GameObject[] familyMembers;
    int chooseMember;

    public void WaffleReady()
    {
        chooseMember = Random.Range(0, 2);
        familyMembers[chooseMember].GetComponent<Hunger>().Eat();
    }
}
