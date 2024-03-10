using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArea : MonoBehaviour
{
    public List<Bullets> bullets = new List<Bullets>();
    public Animator animator;
    public GameObject myCam;


    public IEnumerator StartShowing(bool showAnim)
    {
        myCam.SetActive(true);

        yield return new WaitForSeconds(3);
        if (showAnim)
        {
            animator.SetTrigger("Open");
            yield return new WaitForSeconds(4f);

            animator.SetTrigger("Close");
            yield return new WaitForSeconds(2f);
        }

        myCam.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

    public void SetBullets(int totalRounds, int empty)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            Debug.LogError("Disabling All Bullets");
            bullets[i].fullBullet.SetActive(false);
            bullets[i].emptyBullet.SetActive(false);
        }
        for (int i = 0; i < totalRounds-empty; i++)
        //for (int i = 0; i < empty; i++)
        {
            Debug.LogError("Enabling Full Bullets = " + i);
            bullets[i].fullBullet.SetActive(true);
        }
        for (int i = totalRounds - empty; i < totalRounds; i++)
        {
            Debug.LogError("Enabling Empty Bullets = " + i);
            bullets[i].emptyBullet.SetActive(true);
        }
    }



    [System.Serializable]
    public struct Bullets
    {
        public GameObject emptyBullet, fullBullet;
    }
}
