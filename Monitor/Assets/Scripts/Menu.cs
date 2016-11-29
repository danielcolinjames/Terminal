using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;
using System.Collections;

public class Menu : MonoBehaviour {

    //public Button playButton;
    bool exitActive = false;

    public Canvas exitMenu;
    public Button Play;
    public Button Exit;

    public Button Yes;
    public Button No;

    // Use this for initialization
    void Start() {

        //playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<Button>();

        exitMenu = exitMenu.GetComponent<Canvas>();
        Play = Play.GetComponent<Button>();
        Play.Select();

        Exit = Exit.GetComponent<Button>();

        Yes = Yes.GetComponent<Button>();
        No = No.GetComponent<Button>();

        exitMenu.enabled = false;
    }

    public void ExitPress() {
        exitMenu.enabled = true;
        Play.enabled = false;
        Exit.enabled = false;
    }

    public void NoPress() {
        Play.Select();
        exitMenu.enabled = false;
        Play.enabled = true;
        Exit.enabled = true;
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        Application.Quit();
    }

    int selected = 1;

    int exitSelected = 2;

    public void Update() {
        
        if (exitActive == false && selected == 1) {
            if (Global.prevState.DPad.Down == ButtonState.Released && Global.state.DPad.Down == ButtonState.Pressed) {
                Exit.Select();
                selected = 2;
            }
            if (Global.prevState.DPad.Up == ButtonState.Released && Global.state.DPad.Up == ButtonState.Pressed) {
                Exit.Select();
                selected = 2;
            }
        }
        else if (exitActive == false && selected == 2) {
            if (Global.prevState.DPad.Down == ButtonState.Released && Global.state.DPad.Down == ButtonState.Pressed) {
                Play.Select();
                selected = 1;
            }
            if (Global.prevState.DPad.Up == ButtonState.Released && Global.state.DPad.Up == ButtonState.Pressed) {
                Play.Select();
                selected = 1;
            }
        }

        if (selected == 1) {
            if ((Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) {
                StartGame();
                print("start");
            }
        }
        else if (selected == 2) {
            if ((Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) {
                ExitPress();
                exitActive = true;
            }
        }

        if (exitActive == true) {
            if (Global.prevState.Buttons.B == ButtonState.Released && Global.state.Buttons.B == ButtonState.Pressed) {
                exitActive = false;
                exitSelected = 0;
                NoPress();
            }

            if (exitSelected == 2) {
                if (Global.prevState.DPad.Left == ButtonState.Released && Global.state.DPad.Left == ButtonState.Pressed) {
                    Yes.Select();
                    exitSelected = 1;
                }
                else if (Global.prevState.DPad.Right == ButtonState.Released && Global.state.DPad.Right == ButtonState.Pressed) {
                    Yes.Select();
                    exitSelected = 1;
                }
            }
            else if (exitSelected == 1) {
                if (Global.prevState.DPad.Left == ButtonState.Released && Global.state.DPad.Left == ButtonState.Pressed) {
                    No.Select();
                    exitSelected = 2;
                }
                else if (Global.prevState.DPad.Right == ButtonState.Released && Global.state.DPad.Right == ButtonState.Pressed) {
                    No.Select();
                    exitSelected = 2;
                }
            }
            else {
                exitSelected = 0;
            }
        } else {
            exitSelected = 0;
        }
    }
}