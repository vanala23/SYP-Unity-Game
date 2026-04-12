using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour{
    public GameObject menuCanvas;

    void Start(){
        menuCanvas.SetActive(false);
    }

    public void OnToggleMenu(InputAction.CallbackContext context){
        if(context.performed) menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
}