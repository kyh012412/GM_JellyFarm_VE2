using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite[] jellySpriteList;
    public string[] jellyNameList;

    // 추천 받은 값 
    public int[] jellyJelatinList; // 언락을 위한 젤라틴요구량

    // 추천 받은 값 100 200 500 1000 1500 2000 3000 5000 7500 10000 25000 50000
    public int[] jellyGoldList; // 젤리를 팔았을때 얻는 골드량


    public int[] numGoldList; // 아파트 구매 골드비용
    public int[] clickGoldList; // 클릭 효율 구매 골드비용
    
    public Vector3[] pointList; // 젤리가 외곽일경우 다시 보낼 장소의 좌표
    public RuntimeAnimatorController[] LevelAc;

    public void ChangeAc(Animator anim,int level){
        anim.runtimeAnimatorController = LevelAc[level-1];
    }
}
