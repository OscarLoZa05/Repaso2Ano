using UnityEngine;
using System.Collections.Generic;

public class Repaso : MonoBehaviour
{

    [SerializeField]private int _variableInt = 5;
    public float variableFloat = 6.0f;

    public string variableString = "Hola mundi";

    public bool variableBool = false;

    public int[] arrayInt = new int[5] {12, 4, 8, 9, 0};

    public List<int> listInt = new List<int>(9) {8, 9, 0};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        int numero = 5;

        if (numero == 7)
        {
            //
            //
            //
            //
        }
        else if (numero == 3)
        {

        }
        else
        {

        }


        if (numero == 7)
            transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        /*for (int i = 0; i < length; i++)
        {

        }
        
        foreach (var item in collection)
        {
            
        }*/
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
