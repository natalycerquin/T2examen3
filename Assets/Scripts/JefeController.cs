using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeController : MonoBehaviour
{
    bool puedoCrearZombie = true;
    public GameObject Zombie;
    public int randon = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puedoCrearZombie == true)
        {
            puedoCrearZombie = false;
            Invoke("CrearZombie", randon);
        }
    }
    void CrearZombie()
    {
        puedoCrearZombie = true;
        Instantiate(Zombie, transform.position, Quaternion.Euler(0, 0, 0));
        if(randon == 3)
        {
            randon = 5;
        }else if(randon == 5)
        {
            randon = 3;
        }
    }
}
