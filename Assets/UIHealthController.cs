using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    public HuntressStateController stateController;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((float)stateController.hp / (float)stateController.maxHp);

        image.fillAmount = (float)stateController.hp/ (float)stateController.maxHp;
    }
}
