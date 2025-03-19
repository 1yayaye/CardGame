using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerDownHandler
{
    public GameObject card;
    public GameObject SummonBlock;
    public GameObject attackBlock;
    public GameObject monsterCard;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SummonBlock.activeInHierarchy)
        {
            BattleManager.Instance.SummonConfirm(transform);
        }
        if (attackBlock.activeInHierarchy)
        {
            BattleManager.Instance.AttackCofirm(transform.gameObject);
            //hasMonster = true;
        }
        //Debug.Log("click block");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSummon()
    {
        SummonBlock.SetActive(true);
    }
    public void SetAttack()
    {
        attackBlock.SetActive(true);
    }
    public void CloseAll()
    {
        SummonBlock.SetActive(false);
        attackBlock.SetActive(false);
    }

   /* public void OnPointerClick(PointerEventData eventData)
    {
        if (SummonBlock.activeInHierarchy)
        {
            BattleManager.Instance.SummonConfirm(transform);
            //hasMonster = true;
        }
        if (attackBlock.activeInHierarchy)
        {
            BattleManager.Instance.AttackCofirm(transform);
            //hasMonster = true;
        }
        Debug.Log("click block");
    }*/
}
