using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class EarthController : MonoBehaviour
{
    public Slider health;
    private int healthValue;
    public ParticleSystem damageEffect;
    public ParticleSystem deathEffect;

    private Animator animator;
    public MeshRenderer mesh;
    public static bool alive=true;

    public BaseUI ui;

#if UNITY_IOS
    private string gameID="4180650";
#else
    private string gameID="4180651";
#endif

    private bool showed=false;

    public AdmobController admob;

    // Start is called before the first frame update
    void Start()
    {
        showed=false;
        Advertisement.Initialize(gameID,false);

        alive=true;
        healthValue=100;
        health.value=healthValue;

        animator=gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        transform.Rotate(0,-1,0);
    }


    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Enemy"){
            

            if(healthValue>0){
                damageEffect.Play();
                animator.SetBool("damage",true);
                healthValue-=10;
                health.value=healthValue;

                if(other.gameObject.transform.localScale.x>=0.45f)healthValue-=10;
            }
            if(healthValue<=0){
                if(!showed){
                    deathEffect.Play();
                    mesh.enabled=false;
                    transform.position-=new Vector3(0,10,0);
                    alive=false;

                    Invoke(nameof(showDeathPanel),1.3f);

                    if(BaseUI.addCnt%2==1){
                        if(Advertisement.IsReady("video")){
                            Advertisement.Show("video");
                        }
                        else{
                            admob.showIntersitionalAd();
                        }
                    }
                    BaseUI.addCnt++;

                    showed=true;
                }
            }
            

            Destroy(other.gameObject);

        }
    }

    void showDeathPanel(){
        ui.deathPanel.SetActive(true);
    }
}
