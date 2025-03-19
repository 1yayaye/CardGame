using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteBlock : MonoBehaviour, IPointerDownHandler
{
    public GameObject DelBlock;
    public GameObject card; // 添加 card 变量

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"DeleteBlock.OnPointerDown called on {gameObject.name}");
        if (DelBlock.activeInHierarchy)
        {
            Debug.Log($"Transform passed to DeleteConfirm: {transform.name}");
            BattleManager.Instance.DeleteConfirm(transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
