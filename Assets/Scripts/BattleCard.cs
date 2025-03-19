using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum CardState //位置和所属状态
{
    inPlayerHand, inPlayerBlock, inEnemyHand, inEnemyBlock
}

public class BattleCard : MonoBehaviour, IPointerDownHandler
{
    public CardState state = CardState.inPlayerHand;
    public int playerID;
    //public MonsterCard card;

    public bool hasAttacked=false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 获取 CardDisplay 组件
        var cardDisplay = GetComponent<CardDisplay>();
        if (cardDisplay == null)
        {
            Debug.LogError("CardDisplay component is missing!");
            return;
        }

        // 检查 card 属性是否为 null
        if (cardDisplay.card == null)
        {
            Debug.LogError("CardDisplay.card is null!");
            return;
        }


        //当在手牌中点击时，发起召唤请求
        if (state == CardState.inPlayerHand && BattleManager.Instance.GamePhase == GamePhase.playerAction)
        {
            if (cardDisplay.card is MonsterCard)
            {
                BattleManager.Instance.SummonRequst(0, gameObject);
            }
        }
        else if (state == CardState.inEnemyHand && BattleManager.Instance.GamePhase == GamePhase.enemyAction)
        {
            if (cardDisplay.card is MonsterCard)
            {
                BattleManager.Instance.SummonRequst(1, gameObject);
            }
        }
        else if (state == CardState.inPlayerBlock && BattleManager.Instance.GamePhase == GamePhase.playerAction)
        {
            if (!hasAttacked)
            {
                BattleManager.Instance.AttackRequst(0, gameObject);
            }

        }
        else if (state == CardState.inEnemyBlock && BattleManager.Instance.GamePhase == GamePhase.enemyAction)
        {
            if (!hasAttacked)
            {
                BattleManager.Instance.AttackRequst(1, gameObject);
            }
        }
    }
}
