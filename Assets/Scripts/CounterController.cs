﻿using UnityEngine;
using UnityEngine.UI;

public class CounterController : MonoBehaviour {

    public ScoreManager.Items type;


    void OnEnable()
    {
		PlayerController.UpdateScoreEvent += UpdateCounter;
        PlayerController.PlayerDiedEvent += Reset;
    }

    void OnDisable()
    {
		PlayerController.UpdateScoreEvent -= UpdateCounter;
        PlayerController.PlayerDiedEvent -= Reset;
    }

    void UpdateCounter(ScoreManager.Items item)
    {
        Text counterTxt = GetComponentInChildren<Text>();
        string text = "0";
        
        if (type.Equals(ScoreManager.Items.banana))
        {
            text = ScoreManager.Instance.bananaCounter.ToString();
        } else if (type.Equals(ScoreManager.Items.brain))
        {
            text = ScoreManager.Instance.brainCounter.ToString() + "X";
        } else if (type.Equals(ScoreManager.Items.score))
        {
            text = ScoreManager.Instance.scoreCounter.ToString();
        }
        counterTxt.text = text;
    }

    void Reset()
    {
        UpdateCounter(type);
    }


}
