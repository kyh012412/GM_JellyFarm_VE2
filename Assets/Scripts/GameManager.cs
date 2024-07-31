using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 추천 받은 값 100 200 500 1000 1500 2000 3000 5000 7500 10000 25000 50000
    public int[] jellyGoldList;
    public Vector3[] pointList;

    public RuntimeAnimatorController[] LevelAc;

    public void ChangeAc(Animator anim,int level){
        anim.runtimeAnimatorController = LevelAc[level-1];
    }
}
