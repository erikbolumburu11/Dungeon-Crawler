using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitflash : MonoBehaviour
{
    [SerializeField] float duration;

    Material defaultMaterial;
    public Material flashMaterial;

    void Start() {
        defaultMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void Invoke(){
        GetComponent<SpriteRenderer>().material = flashMaterial;
        StartCoroutine(DisableFlash());
    }

    IEnumerator DisableFlash(){
        yield return new WaitForSeconds(duration);
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }
}