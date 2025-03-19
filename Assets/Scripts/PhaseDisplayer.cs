using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseDisplayer : MonoBehaviour
{
    public Text PhaseText;
    // Start is called before the first frame update
    void Start()
    {
        BattleManager.Instance.PhaseChangeEvent.AddListener(UpdateText);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateText();
    }

    void UpdateText()
    {
        PhaseText.text = BattleManager.Instance.GamePhase.ToString();
    }
}
