using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlockMove : MonoBehaviour
{

    [SerializeField] Vector2 difference;
    [SerializeField] Vector3 mousePos;
    Vector3 positionBlockStart;
     public bool movable = true;
    public GameObject Block;
    private GameController controller;
    private SetlayerBlock setlayerBlock;
    
    

    public Transform[] transformBlock = new Transform[10];
    private void Awake()
    {
        controller = FindObjectOfType<GameController>();
        setlayerBlock = FindObjectOfType<SetlayerBlock>();


    }
    // Start is called before the first frame update
    void Start()
    {
        setlayerBlock.OnSetLayerSprite();
        positionBlockStart = transform.position;// lay vi tri start cua block
    }

    // Update is called once per frame
    void Update()
    {

    }
    // function get posotion mouse up screen
    // functioj of MONOBIHAVIOUR
    private void GetMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if (movable)
        {
            GetMousePos();
            difference = (Vector2)(mousePos - transform.position);
        }
        
    }

    private void OnMouseDrag()
    {
        
        
        if (movable)
        {
            GetMousePos();
            transform.position = (Vector2)mousePos - difference;
            transform.localScale = new Vector3(2f, 2f, 1);
            
        }      
    }
    private void OnMouseUp()
    {
        if (movable)
        {
            if (!CheckTransform())
            {
                transform.position = positionBlockStart;
                transform.localScale = new Vector3(1f, 1f, 1);
            }
            else // vao dung cho
            {
                // lam cho block tu khit dung voi duong
                transform.position = new Vector3((int)transform.position.x, (int)transform.position.y, 0)
                    + new Vector3(0.5f, 0.5f, 0);
                // them block vao trong cac o vuong tren ban
                AddBlockTrans();
                setlayerBlock.OffSetLayerSprite();
                movable = false;
                controller.CheckBlockComplate();
                controller.ClearBlockComplate();


                controller.countBlockInGame--;

            }

        }
    }

    // Function of user

    public void GetTransform()
    {
        int count = 0;
        foreach (Transform subBlock in Block.transform)
        {
            transformBlock[count] = subBlock;

            count++;
        }
        Debug.Log("coudt" + count);//
    }
    public bool CheckTransform()
    {
        
        // check gameobject have out zone gameplay
        foreach (Transform subBlock in Block.transform)
        {
            if ((subBlock.transform.position.x >= 7.8f || subBlock.transform.position.x <= 0.3f) ||
            (subBlock.transform.position.y >= 7.8f || subBlock.transform.position.y <= 0.3f))
            {
                return false; // out gameplay
            }
            else if (controller.gird[(int)subBlock.position.x, (int)subBlock.position.y] != null)
            {
                return false;
            }
        }
            return true;// in gameplay
    }

    private void AddBlockTrans()
    {
        foreach (Transform subBlock in Block.transform)
        {
            controller.gird[(int)subBlock.position.x, (int)subBlock.position.y] = subBlock;
        }
    }


}
