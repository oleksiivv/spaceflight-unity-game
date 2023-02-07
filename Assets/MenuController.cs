using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Text hi;
    public GameObject loadingPanel;

    void Start(){
        hi.text="Best: "+PlayerPrefs.GetInt("best").ToString();
    }
    public void openScene(int id){
        loadingPanel.SetActive(true);
        Application.LoadLevelAsync(id);
    }
}
