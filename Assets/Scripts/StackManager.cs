using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public Transform cubesParent;
    private List<Cube> cubeStack = new List<Cube>();
    public Cube lastCube {  get; private set; }


    public static StackManager Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetLastCube(Cube cube)
    {
        lastCube = cube;
        cubeStack.Add(cube);
    }


    
}
