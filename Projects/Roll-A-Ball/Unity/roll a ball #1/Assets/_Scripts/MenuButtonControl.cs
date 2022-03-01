using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonControl : MonoBehaviour
{
   public void LoadOnClick()
   {
       SceneManager.LoadScene("Fun");
   }

   public void QuitOnClick()
   {
       Application.Quit();
   }
}
