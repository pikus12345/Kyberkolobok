using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotationAround : MonoBehaviour
{

    StarterAssetsInputs input;
    [SerializeField] private float movespeed;
    [SerializeField] private Transform rotatingObject;

    private void Start()
    {
       input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        float v = 0;
        if(input.move.y != 0 || input.move.x != 0)
        {
            v = 1;
        }
        v *= movespeed;
        rotatingObject.Rotate(new Vector3(v, 0, 0) * Time.deltaTime * 30, Space.Self);
    }
}
