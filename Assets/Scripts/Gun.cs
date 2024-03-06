using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Singleton<Gun>
{
    public bool canPick;
    public Transform playerGunPos;
    public Transform chooGunPos;
    public Transform tableGunPos;
    public Animator gunAnim;

    public bool checkToPlayer;
    public bool checkToChoo;
    public bool checkToTable;

    //private void OnValidate()
    //{
    //    if (checkToChoo)
    //    {
    //        checkToChoo = false;
    //        DOTween.To(() => transform.position, x => transform.position = x, chooGunPos.position, 1.5f).OnComplete(() =>
    //        {
    //            transform.rotation = chooGunPos.rotation;
    //        });
    //    }
    //    if (checkToPlayer)
    //    {
    //        checkToPlayer = false;
    //        DOTween.To(() => transform.position, x => transform.position = x, playerGunPos.position, 1.5f).OnComplete(() =>
    //        {
    //            transform.rotation = playerGunPos.rotation;
    //        });
    //    }
    //    if (checkToTable)
    //    {
    //        checkToTable = false;
    //        DOTween.To(() => transform.position, x => transform.position = x, tableGunPos.position, 1.5f).OnComplete(() =>
    //        {
    //            transform.rotation = tableGunPos.rotation;
    //        });
    //    }
    //}


    public void Pick()
    {
        Debug.Log("Pick");
        canPick = false;
    }

    public IEnumerator Picking()
    {
        checkToPlayer = false;
        DOTween.To(() => transform.position, x => transform.position = x, playerGunPos.position, 1.5f).OnComplete(() =>
        {
            transform.rotation = playerGunPos.rotation;
            checkToPlayer = true;
        });

        yield return new WaitUntil(() => checkToPlayer);
        checkToPlayer = false;

        gunAnim.SetTrigger("Reload");

        yield return new WaitForSeconds(1.5f);
    }
    public IEnumerator ToChoo()
    {
        checkToChoo = false;
        DOTween.To(() => transform.position, x => transform.position = x, chooGunPos.position, 1.5f).OnComplete(() =>
        {
            transform.rotation = chooGunPos.rotation;
            checkToChoo = true;
        });

        yield return new WaitUntil(() => checkToChoo);
        checkToChoo = false;

        gunAnim.SetTrigger("Reload");

        yield return new WaitForSeconds(1.5f);
    }
    public IEnumerator ToTable()
    {
        checkToTable = false;
        DOTween.To(() => transform.position, x => transform.position = x, tableGunPos.position, 1.5f).OnComplete(() =>
        {
            transform.rotation = tableGunPos.rotation;
            checkToTable = true;
        });

        yield return new WaitUntil(() => checkToTable);
        checkToTable = false;

        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator Shoot()
    {
        gunAnim.SetTrigger("Shoot");

        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator ShootY()
    {
        gunAnim.SetTrigger("ShootY");

        yield return new WaitForSeconds(1.5f);
    }
}
