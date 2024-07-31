using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColor : MonoBehaviour
{
    [SerializeField] private List<Material> _colorsCubes;
    [SerializeField] private EmittedBeam _emittedBeam;

    public void GenerateColor(GameObject gameObject)
    {
        int randomValue = Random.Range(0, _colorsCubes.Count);

       //  _colorsCubes[randomValue];
    }

    private void OnEnable()
    {
       _emittedBeam.OnCollisionChange += GenerateColor;
    }

    private void OnDisable()
    {
        
    }
}