using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICntrl : MonoBehaviour
{
    [SerializeField] private Button[] pathButtons;
    [SerializeField] private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Button button in pathButtons) {
            button.gameObject.SetActive(false);
        }

        for (int i = 0; i < gameData.numberOfMoves; i++) {
            pathButtons[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBtn(int btnIndex, string text) 
    {
        pathButtons[btnIndex].GetComponentInChildren<TMP_Text>().SetText(text);
    }

    public void OnClickGreenBtn() 
    {
        Debug.Log("OnClickGreenBtn");
    }

    public void OnClickBlueBtn() 
    {
        Debug.Log("OnClickGreenBtn");
    }

    public void OnClickOrangeBtn() 
    {
        Debug.Log("OnClickGreenBtn");
    }

    public void OnClickPurpleBtn() 
    {
        Debug.Log("OnClickGreenBtn");
    }

    public void OnClickRedBtn() 
    {
        Debug.Log("OnClickGreenBtn");
    }

    public void OnClickSkyBtn() 
    {
        Debug.Log("OnClickGreenBtn");
    }

    public void OnClickBGBtn() 
    {
        Debug.Log("OnClickGreenBtn");
    }

    public void OnClickGrayBtn() 
    {
        Debug.Log("OnClickGreenBtn");
    }
}
