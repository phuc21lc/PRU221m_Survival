using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashSkill : MonoBehaviour
{

    [SerializeField]
    private Image imgCd;
    [SerializeField]
    private TMP_Text txtCd;


    private bool isCd = false;
    public float cdTime = 10.0f;
    public float cdTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        txtCd.gameObject.SetActive(false);
        imgCd.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCd)
        {
            ApplyCoolDown();
        }
    }

    void ApplyCoolDown()
    {
        cdTimer -= Time.deltaTime;
        
        if (cdTimer < 0.0f)
        {
            isCd = false;
            txtCd.gameObject.SetActive(false);
            imgCd.fillAmount = 0.0f;
        }
        else
        {
            txtCd.text = Mathf.RoundToInt(cdTimer).ToString();
            imgCd.fillAmount = cdTimer / cdTime;
        }
    }
    
    public void UseSkill()
    {
        if (isCd)
        {

        }
        else
        {
            FindObjectOfType<Player>().isDashButtonDown = true;
            
            isCd = true;
            txtCd.gameObject.SetActive(true);
            cdTimer = cdTime;
            
        }
    }
}
