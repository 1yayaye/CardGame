using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum GamePhase
{
    gameStart, playerDraw, playerAction,enemyDraw,enemyAction
}

public class BattleManager : MonoSingleton<BattleManager>
{
    //public static BattleManager Instance;

    public PlayerData playerData;
    public PlayerData enemyData;//数据

    public List<Card> playerDeckList = new List<Card>();
    public List<Card> enemyDeckList = new List<Card>();//卡组

    public GameObject cardPrefab;//卡牌

    public GameObject arrowPrefab;//召唤指示箭头
    public GameObject attackPrefab;//攻击指示箭头
    private GameObject arrow;

    public Transform canvas;
    public Transform playerHand;
    public Transform enemyHand;//手牌

    public GameObject[] PlayerBlocks;
    public GameObject[] enemyBlocks;//格子

    public GameObject[] PlayerDelBlocks;
    public GameObject[] enemyDelBlocks;//弃牌格子

    public GameObject playerIcon;
    public GameObject enemyIcon;//头像

    public GamePhase GamePhase = GamePhase.gameStart;

    public UnityEvent PhaseChangeEvent = new UnityEvent();

    private GameObject waitingMonster;//选择的怪兽
    private int waitingID;
    public GameObject attackingMonster;
    private int attackingID;

    private int waitingPlayer;//选择的玩家

    public int playerHealthPoint;
    public int enemyHealthPoint;
    
    /*private void Awake()
    {
        Instance = this;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        //读取数据
        ReadDeck();
        //卡组洗牌
        ShuffletDeck(0);
        ShuffletDeck(1);
        //玩家抽卡3，敌人抽卡3
        //DrawCard(0, 3);
        //DrawCard(1, 3);

        GamePhase = GamePhase.playerDraw;
    }

    //public void ReadDeck() //读取卡组
    //{ 
    //    //加载玩家卡组
    //    for (int i = 0; i < playerData.playerCards.Length; i++)
    //    {
    //        if (playerData.playerCards[i] != 0)
    //        {
    //            int count = playerData.playerCards[i];
    //            for (int j = 0; j < count; j++)
    //            {
    //                playerDeckList.Add(playerData.CardStore.CopyCard(i));
    //            }
    //        }
    //    }
    //    //加载敌人卡组
    //    for (int i = 0; i < enemyData.playerCards.Length; i++)
    //    {
    //        if (enemyData.playerCards[i] != 0)
    //        {
    //            int count = enemyData.playerCards[i];
    //            for (int j = 0; j < count; j++)
    //            {
    //                enemyDeckList.Add(enemyData.CardStore.CopyCard(i));
    //            }
    //        }
    //    }
    //}  

    public void ReadDeck() //读取卡组
    {
        //加载玩家卡组
        for (int i = 0; i < playerData.playerCards.Length; i++)
        {
            if (playerData.playerCards[i] != 0)
            {
                int count = playerData.playerCards[i];
                for (int j = 0; j < count; j++)
                {
                    // 此处传递 rank 和 id
                    int rank = i;  // 假设 rank 是 i，可以根据你的数据结构调整
                    playerDeckList.Add(playerData.CardStore.CopyCard(rank, i));
                }
            }
        }

        //加载敌人卡组
        for (int i = 0; i < enemyData.playerCards.Length; i++)
        {
            if (enemyData.playerCards[i] != 0)
            {
                int count = enemyData.playerCards[i];
                for (int j = 0; j < count; j++)
                {
                    // 此处传递 rank 和 id
                    int rank = i;  // 假设 rank 是 i，可以根据你的数据结构调整
                    enemyDeckList.Add(enemyData.CardStore.CopyCard(rank, i));
                }
            }
        }
    }

    public void OnPlayerDraw() //玩家抽卡阶段
    {
        if (GamePhase == GamePhase.playerDraw)
        {
            DrawCard(0, 3);
        }
    }

    public void OnEnemyDraw() //玩家2抽卡阶段
    {
        if (GamePhase == GamePhase.enemyDraw)
        {
            DrawCard(1, 3);
        }
    }

    public void DrawCard(int _player,int _count)  //抽卡
    {
        List<Card> drawDeck = new List<Card>();
        Transform hand = transform; // 抽到谁的手里坐标
        if (_player == 0) 
        {
            drawDeck = playerDeckList;
            hand = playerHand;
            for (int i = 0; i < _count; i++)
            {
                //Debug.Log(drawDeck.Count);
                GameObject card = GameObject.Instantiate(cardPrefab, hand.transform);
                card.GetComponent<CardDisplay>().card = drawDeck[0];
                card.GetComponent<BattleCard>().playerID = _player;
                drawDeck.RemoveAt(0);
                card.GetComponent<BattleCard>().state = CardState.inPlayerHand;
                GamePhase = GamePhase.playerAction;
                PhaseChangeEvent.Invoke();
            }
        }
        else if(_player == 1)
        {
            drawDeck = enemyDeckList;
            hand = enemyHand;
            
            for (int i = 0; i < _count; i++)
            {
                //Debug.Log(drawDeck.Count);
                GameObject card = GameObject.Instantiate(cardPrefab, hand.transform);
                card.GetComponent<CardDisplay>().card = drawDeck[0];
                card.GetComponent<BattleCard>().playerID = _player;
                drawDeck.RemoveAt(0);
                card.GetComponent<BattleCard>().state = CardState.inEnemyHand;
                GamePhase = GamePhase.enemyAction;
                PhaseChangeEvent.Invoke();
            }
        }


    }

    // 点击结束回合按钮
    public virtual void OnClickTurnEnd()
    {
        TurnEnd();
    }

    public void TurnEnd() //回合结束
    {
        if (GamePhase == GamePhase.playerAction)
        {
            GamePhase = GamePhase.enemyDraw;
            PhaseChangeEvent.Invoke();
        }
        else if (GamePhase == GamePhase.enemyAction)
        {
            GamePhase = GamePhase.playerDraw;
            PhaseChangeEvent.Invoke();
        }
    }

    // 召唤请求，点击手牌时触发
    public void SummonRequst(int _player, GameObject _monster)
    {
        Debug.Log($"SummonRequst called with _player={_player}, _monster={_monster}");

        GameObject[] blocks;
        GameObject[] DeleteBlocks;
        bool hasEmptyBlock = false;

        if (_player == 0)
        {
            blocks = PlayerBlocks;
            DeleteBlocks = PlayerDelBlocks;
        }
        else
        {
            blocks = enemyBlocks;
            DeleteBlocks = enemyDelBlocks;
        }

        foreach (var block in blocks)
        {
            Block blockComponent = block.GetComponent<Block>();

            if (blockComponent.card == null)
            {
                blockComponent.SummonBlock.SetActive(true);
            }
            hasEmptyBlock = true;
        }

        foreach (var block in DeleteBlocks)
        {
            DeleteBlock blockComponent = block.GetComponent<DeleteBlock>();
            blockComponent.DelBlock.SetActive(true);
        }

        if (hasEmptyBlock)
        {
            waitingMonster = _monster;
            waitingPlayer = _player;
        }
        else
        {
            Debug.Log("No empty block found.");
        }
    }


    // 召唤确认，点击格子时触发
    public void SummonConfirm(Transform _block)
    {
        Summon(waitingPlayer, waitingMonster, _block);
        GameObject[] blocks;
        if (waitingPlayer == 0)
        {
            blocks = PlayerBlocks;
        }
        else
        {
            blocks = enemyBlocks;
        }
        foreach (var block in blocks)
        {
            block.GetComponent<Block>().SummonBlock.SetActive(false);
        }
    }

    //召唤怪兽
    public void Summon(int _player, GameObject _monster, Transform _block)
    {

        // 获取怪兽的价值
        int monsterValue = _monster.GetComponent<CardDisplay>().cost;

        // 扣除怪兽价值的金币
        if (_player == 0)
        {
            //_monster.GetComponent<BattleCard>().state = CardState.inPlayerBlock;
            playerData.playerCoins -= monsterValue;
        }
        else
        {
            //_monster.GetComponent<BattleCard>().state = CardState.inEnemyHand;
            enemyData.playerCoins -= monsterValue;
        }

        Block blockComponent = _block.GetComponent<Block>();
        if (blockComponent == null)
        {
            Debug.LogError("Error: Block component is missing on _block!");
            return;
        }

        if (_monster == null)
        {
            Debug.LogError("Error: _monster is null!");
            return;
        }

        _monster.transform.SetParent(_block);
        _monster.transform.localPosition = Vector3.zero;
        if (_player == 0)
        {
            _monster.GetComponent<BattleCard>().state = CardState.inPlayerBlock;
        }
        else
        {
            _monster.GetComponent<BattleCard>().state = CardState.inEnemyBlock;
        }
        blockComponent.card = _monster;
    }


    // 丢弃确认，点击格子时触发
    public void DeleteConfirm(Transform _block)
    {
        Debug.Log($"DeleteConfirm called with _block: {_block.name}");
        if (waitingMonster == null)
        {
            Debug.LogError("Error: waitingMonster is null!");
            return;
        }

        if (_block == null)
        {
            Debug.LogError("Error: _block is null!");
            return;
        }

        Delete(waitingPlayer, waitingMonster, _block);
        GameObject[] blocks;
        if (waitingPlayer == 0)
        {
            blocks = PlayerBlocks;
        }
        else
        {
            blocks = enemyBlocks;
        }
        
    }

    //丢弃怪兽
    public void Delete(int _player, GameObject _monster, Transform _block)
    {
        // 获取怪兽的价值
        int monsterValue = _monster.GetComponent<CardDisplay>().cost;

        // 返还怪兽价值一半的金币
        if (_player == 0)
        {
            playerData.playerCoins += monsterValue / 2 + 1;
        }
        else
        {
            enemyData.playerCoins += monsterValue / 2 + 1;
        }

        DeleteBlock blockComponent = _block.GetComponent<DeleteBlock>();

        // 将怪兽从当前格子移除
        _monster.transform.SetParent(null);

        // 销毁怪兽对象
        Destroy(_monster);

        // 更新格子状态
        blockComponent.card = null;
    }
    public void ShuffletDeck(int _player)// 将卡组洗牌，输入玩家编号，0代表player，1代表Enemy
    {
        switch (_player)
        {
            case 0:
                for (int i = 0; i < playerDeckList.Count; i++)
                {
                    int rad = Random.Range(0, playerDeckList.Count);
                    Card temp = playerDeckList[i];
                    playerDeckList[i] = playerDeckList[rad];
                    playerDeckList[rad] = temp;
                }
                break;
            case 1:
                for (int i = 0; i < enemyDeckList.Count; i++)
                {
                    int rad = Random.Range(0, enemyDeckList.Count);
                    Card temp = enemyDeckList[i];
                    enemyDeckList[i] = enemyDeckList[rad];
                    enemyDeckList[rad] = temp;
                }
                break;
        }
    }

    public void AttackRequst(int _player, GameObject _monster)
    {
        Debug.Log($"AttackRequst called with _player={_player}, _monster={_monster}");
        /*   if (arrow == null)
           {
               arrow = GameObject.Instantiate(attackPrefab, canvas);
           }

           arrow.GetComponent<ArrowFollow>().SetStartPoint(_startPoint);*/

        // 直接攻击条件
        bool strightAttack = true;

        if (_player == 0 && GamePhase == GamePhase.playerAction)
        {
            foreach (var block in enemyBlocks)
            {
                Debug.Log($"Set block={block} to active");
                if (block.GetComponent<Block>().card != null)
                {
                    block.GetComponent<Block>().attackBlock.SetActive(true);   
                    strightAttack = false;
                }
            }
            if (strightAttack)
            {
                // 可以直接攻击对手玩家
            }
        }
        if (_player == 1 && GamePhase == GamePhase.playerAction)
        {
            foreach (var block in PlayerBlocks)
            {
                Debug.Log($"Set block={block} to active");
                if (block.GetComponent<Block>().card != null)
                {
                    block.GetComponent<Block>().attackBlock.SetActive(true);
                    strightAttack = false;
                }
            }
            if (strightAttack)
            {
                // 可以直接攻击对手玩家
            }
        }

        attackingMonster = _monster;
        attackingID = _player;

    }
    public void AttackCofirm(GameObject _object)
    {
        Debug.Log($"AttackCofirm called to _target={_object}");
        Attack(attackingMonster, attackingID, _object);
        GameObject[] blocks;
        if (waitingPlayer == 0)
        {
            blocks = enemyBlocks;
        }
        else
        {
            blocks = PlayerBlocks;
        }
        foreach (var block in blocks)
        {
            block.GetComponent<Block>().attackBlock.SetActive(false);
        }
        attackingMonster = null;
    }
    public void Attack(GameObject _monster, int _id, GameObject _target)
    {
        //结算伤害
        //处理销毁
        //恢复攻击状态，已攻击状态
        if (arrow != null)
        {
            Destroy(arrow);
        }
        _monster.GetComponent<BattleCard>().hasAttacked = true;
        Debug.Log("攻击成立");

        // 
        var attackMonster = _monster.GetComponent<CardDisplay>().card as MonsterCard;
        var targetMonster = _target.GetComponent<CardDisplay>().card as MonsterCard;
        //Debug.Log(targetMonster.healthPoint);
        targetMonster.GetDamage(attackMonster.attack);
        if (targetMonster.healthPoint > 0)
        {
            _target.GetComponent<CardDisplay>().ShowCard();
        }
        else
        {
            Destroy(_target);
        }


        foreach (var block in PlayerBlocks)
        {
            block.GetComponent<Block>().CloseAll();
        }
        foreach (var block in enemyBlocks)
        {
            block.GetComponent<Block>().CloseAll();
        }
    }
}
