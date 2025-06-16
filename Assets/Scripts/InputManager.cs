using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera _camera;
    // public static event Action<Vector3> OnSceneClick;
    // public static event Action<Item> OnInventoryItemClick;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Привет, Unity!");
            }
        }
    }
}