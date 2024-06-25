using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI userRankTxt;
    [SerializeField]
    private TextMeshProUGUI userNameTxt;
    [SerializeField]
    private TextMeshProUGUI userScoreTxt;
    public Animator rankAnime;
    [SerializeField]
    private Image victoryStandImg;

    public void Init(SlotData data)
    {
        userRankTxt.text = data.userRank;
        userNameTxt.text = data.userName.Substring(0, 7);
        userScoreTxt.text = data.userScore;
        rankAnime.runtimeAnimatorController = data.rankAnime;
        rankAnime.SetTrigger(data.animeParam);
        if (victoryStandImg != null && data.animeParam != "Retire")
        {
            victoryStandImg.gameObject.SetActive(true);
            victoryStandImg.sprite = data.victoryStand;
        }
    }
}
