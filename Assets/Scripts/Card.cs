using System.Security.Cryptography;

public class Card
{
    public int id;
    public int rank;
    public string cardName;
    public Card(int _rank,int _id,string _cardName)//构造函数
    {
        this.rank = _rank;
        this.id = _id;
        this.cardName = _cardName;
    }
}

public class MonsterCard : Card //继承Card    类继承
{
    public int attack;
    public int defence;
    public int defenceMax;
    public int healthPoint; //显示掉血情况
    public int healthPointMax; //显示最大血量
    public int cost;
    //public string effect;

    //等级、属性
    public MonsterCard(int _rank, int _id, string _cardName, int _attack, int _defenceMax, int _healthPointMax, int cost) : base( _rank, _id, _cardName)
    {
        this.attack = _attack;
        this.defence = _defenceMax;
        this.defenceMax = _defenceMax;
        this.healthPoint = _healthPointMax;
        this.healthPointMax = _healthPointMax;
        this.cost = cost;
    }

    public void GetDamage(int _damagePoint)
    {
        if ((healthPoint + defence) > _damagePoint)
        {
            if(_damagePoint > defence)
            {
                _damagePoint = _damagePoint - defence;
                healthPoint = healthPoint - _damagePoint;
            }
            else
            {
                if (_damagePoint <= (defence / 2))
                    defence = defence - 1;
                else
                    defence =(int)(defence / 2);
            }
        }
        else
        {
            healthPoint = 0;
            damageDestroy();
        }
    }

    public void damageDestroy()
    {
        // do some thing...
    }
}

//public class SpellCard : Card   //魔法卡
//{
//    public string effect;

//    public SpellCard(int _id, string _cardName, string _effect) : base( _id, _cardName)
//    {
//        this.effect = _effect;
//    }
//}