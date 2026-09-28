using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using RayGUI_cs;
using RayGuiCreator;

namespace Valikkopeli
{
    /// <summary>
    /// Enum jossa on pelin eri tilat
    /// 
    /// </summary>
    enum GameState
    {
        MainMenu,   //Päävalikko
        GameLoop,   //Pelin pelaaminen
        PauseMenu,  //Taukovalikko
        OptionsMenu,

        Quit //Peli loppuu

    }

    //PauseMenu pauseMenu;

    internal class Game
    {
        GameState currentState;

        //OptionsMenu options;

        public void Init()
        {
            currentState = GameState.MainMenu;
        }
        public void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false && currentState != GameState.Quit)
            {
                switch (currentState)
                {
                    case GameState.MainMenu:
                        DrawMainMenu();
                        break;
                    case GameState.GameLoop:
                        UpdateGame();
                        DrawGame();
                        break;
                }
            }
        }

        public void UpdateGame()
        {

        }
        public void DrawGame()
        {

        }
        public void DrawMainMenu()
        {
            //Muuttujat valikkoon
            int menuStartX = 10;
            int menuStartY = 0;
            int rowHeight = Raylib.GetScreenHeight();
            int rowWidth = Raylib.GetScreenWidth();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);

            //Luodaan uusi menuCreator
            MenuCreator creator = new MenuCreator(menuStartX, menuStartY, rowHeight, rowWidth);
            creator.Label("MainMenu");

            if (creator.Button("Options"))
            {

            }
            if (creator.Button("Other button"))
            {

            }
            if (creator.Button("Start Game"))
            {
                currentState = GameState.GameLoop;
            }
            
            Raylib.EndDrawing();
        }
    }
}
