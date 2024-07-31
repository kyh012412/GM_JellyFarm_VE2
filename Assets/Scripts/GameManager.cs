using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3[] pointList;

    public RuntimeAnimatorController[] LevelAc;

    public void ChangeAc(Animator anim,int level){
        anim.runtimeAnimatorController = LevelAc[level-1];
    }
}
