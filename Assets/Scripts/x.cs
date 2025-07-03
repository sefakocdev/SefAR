using UnityEngine;
using UnityEngine.InputSystem;

public class x : MonoBehaviour
{
    public Material selectedMaterial;
    private Material originalMaterial;
    private GameObject selectedObject;

    private InputAction clickAction;

    // Materials
    public Material material1;
    public Material material2;
    public Material material3;
    private Material[] materials;
    private int currentMaterialIndex = 0;

    void Awake()
    {
        // Initialize the materials array
        materials = new Material[] { material1, material2, material3 };

        var inputActions = new InputActionMap("Player");
        clickAction = inputActions.AddAction("Click", binding: "<Mouse>/leftButton");
    }

    void OnEnable()
    {
        clickAction.Enable();
        clickAction.performed += OnClickPerformed;
    }

    void OnDisable()
    {
        clickAction.Disable();
        clickAction.performed -= OnClickPerformed;
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        SelectObject();
    }

    void SelectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            
            // Check if the hit object has the tag "SelectableObject"
            if (hitObject.tag == "SelectableObject")
            {
                // Reset the material of the previously selected object, if there is one
                if (selectedObject != null)
                {
                    Renderer[] previousRenderers = selectedObject.GetComponentsInChildren<Renderer>();
                    foreach (var renderer in previousRenderers)
                    {
                        renderer.material = originalMaterial;
                    }
                }

                // Select the new object and store its original material
                selectedObject = hitObject;
                Renderer[] renderers = selectedObject.GetComponentsInChildren<Renderer>();

                if (renderers.Length > 0)
                {
                    originalMaterial = renderers[0].material; // Save original material from the first renderer

                    // Apply the selected material to all renderers
                    foreach (var renderer in renderers)
                    {
                        renderer.material = selectedMaterial;
                    }
                }
            }
            // Check if the hit object has the tag "colorButton"
            else if (hitObject.tag == "colorButton")
            {
                // Change the material of the previously selected object, if there is one
                if (selectedObject != null && materials.Length > 0)
                {
                    // Cycle to the next material
                    currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
                    Material nextMaterial = materials[currentMaterialIndex];

                    // Apply the next material to the previously selected object's renderers
                    Renderer[] previousRenderers = selectedObject.GetComponentsInChildren<Renderer>();
                    foreach (var renderer in previousRenderers)
                    {
                        renderer.material = nextMaterial;
                    }
                }
            }
            // Check if the hit object has the tag "sizeUpButton"
            else if (hitObject.tag == "sizeUpButton")
            {
                // Scale up the selected object
                if (selectedObject != null)
                {
                    selectedObject.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                }
            }
            // Check if the hit object has the tag "sizeDownButton"
            else if (hitObject.tag == "sizeDownButton")
            {
                // Scale down the selected object
                if (selectedObject != null)
                {
                    selectedObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                }
            }
            else if (hitObject.tag == "rotateButton")
            {
                // Rotate the selected object 90 degrees on the Y-axis
                if (selectedObject != null)
                {
                    selectedObject.transform.Rotate(0, 90, 0);
                }
            }
        }
    }
}
