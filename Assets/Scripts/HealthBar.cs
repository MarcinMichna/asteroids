using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthBar : MonoBehaviour 
{
    private Transform bar;
    private ReceiveDamage receiveDamage;
    // Start is called before the first frame update
    private void Start()
    {
        receiveDamage = GetComponent<ReceiveDamage>();
        bar = transform.Find("Bar");
        
    }
    void Update(){

    }
    public void SetSize(float sizeNormalized){
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

}
