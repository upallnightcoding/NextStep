using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICntrl : MonoBehaviour
{
    [SerializeField] private Button[] pathButtons;
    [SerializeField] private Material[] colors;
    [SerializeField] private GameData gameData;
    [SerializeField] private BoardCntrl boardCntrl;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameData.numberOfMoves; i++) {
            pathButtons[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string GetMove(int btnIndex)
    {
        return(pathButtons[btnIndex].transform.Find("Movement").GetComponent<TMP_Text>().text);
    }

    public void SetBtn(int btnIndex, string text) 
    {
        pathButtons[btnIndex].transform.Find("Movement").GetComponent<TMP_Text>().SetText(text);
    }

    public void OnClickGreenBtn() 
    {
        boardCntrl.ShowMove(GetMove(0), colors[0]);
    }

    public void OnClickBlueBtn() 
    {
        boardCntrl.ShowMove(GetMove(1), colors[1]);
    }

    public void OnClickOrangeBtn() 
    {
        boardCntrl.ShowMove(GetMove(2), colors[2]);
    }

    public void OnClickPurpleBtn() 
    {
        boardCntrl.ShowMove(GetMove(3), colors[3]);
    }

    public void OnClickRedBtn() 
    {
        boardCntrl.ShowMove(GetMove(4), colors[4]);
    }

    public void OnClickSkyBtn() 
    {
        boardCntrl.ShowMove(GetMove(5), colors[5]);
    }

    public void OnClickBlueGrayGBtn() 
    {
        boardCntrl.ShowMove(GetMove(6), colors[6]);
    }

    public void OnClickGrayBtn() 
    {
        boardCntrl.ShowMove(GetMove(7), colors[7]);
    }
}
