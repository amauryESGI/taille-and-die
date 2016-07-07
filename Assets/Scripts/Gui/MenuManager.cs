using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class MenuManager : MonoBehaviour
    {
        public Menu CurrentMenu;

        public void Start()
        {
            ShowMenu(CurrentMenu);
        }
        public void ShowMenu(Menu menu)
        {
            HideCurrentMenu();

            CurrentMenu = menu;
            CurrentMenu.IsOpen = true;
        }
        public void HideCurrentMenu()
        {
            if ( CurrentMenu != null )
                CurrentMenu.IsOpen = false;
            CurrentMenu = null;
        }
        public void DebugMenu(Menu menu) {
            Debug.Log("Ask to display menu : \"" + menu.name + "\".");
        }
    }
}
