using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStore : MonoBehaviour
{
    public TextAsset cardData;
    public List<Card> cardList = new List<Card>();  //储存 把cardData数据转入cardList

    // Start is called before the first frame update
    void Start()
    {
        //LoadCardData();
        //TestLoad();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadCardData()
    {
        string[] dataRow = cardData.text.Split('\n');
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "monster")
            {
                //新建怪兽卡
                int rank = int.Parse(rowArray[1]);
                int id = int.Parse(rowArray[2]);
                string name = rowArray[3];
                int atk = int.Parse(rowArray[4]);
                int dtf = int.Parse(rowArray[6]);
                int health = int.Parse(rowArray[5]);
                int cost = int.Parse(rowArray[7]);
                //string effect = null;
                //if (rowArray[8] != null)
                //    effect = rowArray[8];
                MonsterCard monsterCard = new MonsterCard(rank, id, name, atk, dtf, health, cost);
                cardList.Add(monsterCard);

                //测试脚本功能
                Debug.Log("读取到怪兽卡:" + monsterCard.cardName);//在Unity下面的Console能读取到数据，说明正常运行
            }
            else if (rowArray[0] == "spell")
            {
                //新建魔法卡
                //int id = int.Parse(rowArray[1]);
                //string name = rowArray[2];
                //string effect = rowArray[3];
                //SpellCard spellCard = new SpellCard(id, name, effect);
                //cardList.Add(spellCard);
            }
        }
    }

    //另外写测试读取情况
    public void TestLoad()
    {
        foreach (var item in cardList)
        {
            Debug.Log("卡牌:" + item.rank.ToString() + item.id.ToString() + item.cardName);  //因为id是int类型，得转string类型查看
        }
    }

    //供给CardStore场景的OpenPackage的OnClickCard功能
    public Card RandomCard()
    {
        Card card = cardList[Random.Range(0, cardList.Count)];   //随机整个卡包,像正常游戏中，也是在这个构造函数里补足概率功能
        return card;
    }

    public Card CopyCard(int _rank,int _id)
    {
        Card copyCard = new Card(_rank,_id, cardList[_id].cardName);
        if (cardList[_id] is MonsterCard)
        {
            MonsterCard monsterCard = cardList[_id] as MonsterCard;
            copyCard = new MonsterCard(monsterCard.rank,_id, monsterCard.cardName, monsterCard.attack, monsterCard.defence, monsterCard.healthPoint, monsterCard.cost);
        }
        //else if (cardList[_id] is SpellCard)
        //{
        //    SpellCard spellCard = cardList[_id] as SpellCard;
        //    copyCard = new SpellCard(_id, spellCard.cardName, spellCard.effect);
        //}
        return copyCard;
    }
}
