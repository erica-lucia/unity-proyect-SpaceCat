using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{ 
    public static LevelManager sharedInstance;
    public List<LevelBlock>alltheLevelBlocks=new List<LevelBlock>();
    public List<LevelBlock>currentLevelBlocks=new List<LevelBlock>();
    public Transform LevelStartPosition;


    private void Awake(){ 
        if(sharedInstance==null){ 
            sharedInstance= this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }


    //Metodo para añadir un bloque
    public void AddLevelBlock(){ 

    } 

    public void RemoveLevelBlock(){ 

    } 

    public void RemoveAllLevelBlocks(){ 

    } 

    //metodo para generar los bloques iniciales 
    public void GenerateInitialBlocks(){ 
        for (int i =0; i<2; i++){ 
            AddLevelBlock();
        }
    }
}
