using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particles;
    [SerializeField] TriggerScript[] trigger;

    bool blockWin = false;
    List<int> listTrg = new List<int>();


    private void Awake()
    {
        for (int i = 0; i < trigger.Length; i++)
        {
            trigger[i].OnAddToList += AddToList;
            trigger[i].OnRemoveFromList += RemoveFromList;
        }

        for (int i = 0; i < particles.Length; i++)
            particles[i].Stop();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < trigger.Length; i++)
        {
            trigger[i].OnAddToList -= AddToList;
            trigger[i].OnRemoveFromList -= RemoveFromList;
        }
    }

    private void AddToList(int trg)
    {
        listTrg.Add(trg);
    }

    private void RemoveFromList(int trg)
    {
        listTrg.Remove(trg);
    }

    private void Update()
    {
        if (listTrg.Count > 1)
        {
            if (listTrg[0] < listTrg[1])
                Win();
        }
    }

    private void Win()
    {
        listTrg.Clear();

        if (!blockWin)
        {
            Debug.Log("Win");
            blockWin = true;
            for (int i = 0; i < particles.Length; i++)
                particles[i].Play();
            StartCoroutine(StopBlock());
        }
    }

    IEnumerator StopBlock()
    {
        yield return new WaitForSeconds(1);
        blockWin = false;
        for (int i = 0; i < particles.Length; i++)
            particles[i].Stop();
    }
}
