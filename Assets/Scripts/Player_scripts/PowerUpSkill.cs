using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSkill : MonoBehaviour
{
    [SerializeField]
    private Image imgCd;
    [SerializeField]
    private TMP_Text txtCd;

    public GameObject gun;

    private bool isCd = false;
    private bool isEffect = false;
    public float cdTime = 10.0f;
    public float cdTimer = 0.0f;

    public float effectTime = 5f;
    public float effectTimer = 0.0f;
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
        if (isEffect)
        {
            ApplySkill();
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
    void ApplySkill()
    {
        effectTimer -= Time.deltaTime;
        if (effectTimer < 0.0f)
        {
            isEffect = false;
            gun.SetActive(false);
        }
    }
    public void UseSkill()
    {
        if (isCd)
        {

        }
        else
        {
            isCd = true;
            txtCd.gameObject.SetActive(true);
            cdTimer = cdTime;

        }
        if (isEffect)
        {

        }
        {
            isEffect = true;
            gun.SetActive(true);
            effectTimer = effectTime;
        }
    }
}
