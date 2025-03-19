using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Text nameText;
    public Text attackText;
    public Text healthText;
    public Text effectText;
    public Text defenceText;
    public int cost;

    public Image backgroundImage;

    public Card card;

    // Start is called before the first frame update
    void Start()
    {
        ShowCard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void ShowCard()
    //{
    //    if (card == null)
    //    {
    //        Debug.LogError("Card is not assigned!");
    //        return;
    //    }

    //    nameText.text = card.cardName;

    //    // 判断卡片是否为 MonsterCard
    //    if (card is MonsterCard)
    //    {
    //        var monster = card as MonsterCard;

    //        // 显示攻击力、防御力和生命值
    //        attackText.text = monster.attack.ToString();
    //        defenceText.text = monster.defence.ToString();
    //        healthText.text = monster.healthPoint.ToString();
    //        cost = monster.cost;
    //        // 隐藏法术卡的效果文本
    //        effectText.gameObject.SetActive(false);
    //    }
    //    //else if (card is SpellCard)
    //    //{
    //    //    var spell = card as SpellCard;

    //    //    // 显示法术效果
    //    //    effectText.text = spell.effect;

    //    //    // 隐藏怪物卡的攻击力和生命值
    //    //    attackText.gameObject.SetActive(false);
    //    //    healthText.gameObject.SetActive(false);
    //    //}
    //}

    public void ShowCard()
    {
        if (card == null)
        {
            Debug.LogError("Card is not assigned!");
            return;
        }

        nameText.text = card.cardName;

        // 获取卡片对应的图片路径
        string imageName = card.cardName;  // 假设卡片的名称就是图片的名称

        // 加载图片，假设图片文件格式为 .jpeg
        Sprite cardImage = Resources.Load<Sprite>(imageName);

        // 设置背景图片
        if (cardImage != null)
        {
            backgroundImage.sprite = cardImage;  // 显示卡牌图像
        }
        else
        {
            Debug.LogWarning("Card image not found for: " + imageName);
        }

        // 判断卡片是否为 MonsterCard
        if (card is MonsterCard)
        {
            var monster = card as MonsterCard;

            // 显示攻击力、防御力和生命值
            attackText.text = "<color=red>" + monster.attack.ToString() + "</color>";
            defenceText.text = "<color=blue>" + monster.defence.ToString() + "</color>";
            healthText.text = "<color=yellow>" + monster.healthPoint.ToString() + "</color>";
            cost = monster.cost;

            // 隐藏法术卡的效果文本
            effectText.gameObject.SetActive(false);
        }
        //else if (card is SpellCard)
        //{
        //    var spell = card as SpellCard;

        //    // 显示法术效果
        //    effectText.text = spell.effect;

        //    // 隐藏怪物卡的攻击力和生命值
        //    attackText.gameObject.SetActive(false);
        //    healthText.gameObject.SetActive(false);
        //}
    }

}
