using UnityEngine;
using System.Collections;
namespace FMG
{
	public class LevelButton : MonoBehaviour {
		public int levelIndex=0;

		public void onClick()
		{
            switch (levelIndex)
            {
                case 0:
                    Application.LoadLevel("George_Scene");
                    break;
                case 1:
                    // show credits
                    break;
                case 2:
                default:
                    Application.Quit();
                    break;
            }
		}
        
    }
}