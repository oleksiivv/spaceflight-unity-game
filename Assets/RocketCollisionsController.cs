using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketCollisionsController : MonoBehaviour
{
    public ParticleSystem asteroidCollisionEffect;
    public Text score,best;
    private int scoreCnt;

    public GameObject newRecordPanel;

    private int showBestScore=0;

    void Start(){
        showBestScore=0;

        scoreCnt=0;
        score.text="Score: 000";

        best.text="Best: "+PlayerPrefs.GetInt("best").ToString();
    }


    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Enemy"){
            asteroidCollisionEffect.Play();

            Destroy(other.gameObject);
            scoreCnt++;
            score.text="Score: "+scoreCnt.ToString();

            if(scoreCnt>PlayerPrefs.GetInt("best") && PlayerPrefs.GetInt("best",0)!=0 && showBestScore==0){
                newRecordPanel.SetActive(true);
                Invoke(nameof(cleanNewRecordPanel),2f);

                showBestScore=1;
            }

            if(scoreCnt>PlayerPrefs.GetInt("best")){
                PlayerPrefs.SetInt("best",scoreCnt);

                best.text="Best: "+PlayerPrefs.GetInt("best").ToString();

                showBestScore=1;
            }

            
        }
    }

    void cleanNewRecordPanel(){
        newRecordPanel.SetActive(false);
    }
}
