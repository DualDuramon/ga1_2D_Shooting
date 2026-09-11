using UnityEngine;


[System.Serializable]
public class EnemySpawnData
{
    //순수 데이터 클래스라 로직이 있어선 안된다.
    //기획서 받아서 만들곤 한다
    public GameObject EnemyPreafb;
    public int Weight; //가중치
}
