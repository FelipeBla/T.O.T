using System.IO;

namespace TripOverTime.Main
{
    static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args) //fonction principale
        {
            App app = new App();
            // Settings
            if (File.Exists("settings"))
            {
                string settingsFile = File.ReadAllText("settings");
                string[] st = settingsFile.Split("\n");
                Settings.Fullscreen = Convert.ToBoolean(st[0]);
                Settings.NbFPS = Convert.ToUInt16(st[1]);
                Settings.XResolution = Convert.ToUInt16(st[2]);
                Settings.YResolution = Convert.ToUInt16(st[3]);

            }
            else
            {
                string settingsFile = Settings.Fullscreen + "\n" + Settings.NbFPS + "\n" + Settings.XResolution + "\n" + Settings.YResolution;
                File.WriteAllText("settings", settingsFile);
            }

                    // Choose Map
                ChooseMapMenu:
                    else if (chooseMap == "editedMap")
                    {
                        engine.GetMenu.InitMapEditedMenu();
                        do
                        {
                            if (spGame.ElapsedMilliseconds >= 1000 / tps)
                            {
                                chooseMap = engine.GetMenu.ChooseMapEditedMenu();
                                spGame.Restart();
                            }
                        } while (chooseMap == "editedMap");

                        if (chooseMap == "back")
                        {
                            //back to map menu
                            goto ChooseMapMenu;
                        }
                    }
                    engine.GetGUI.ShowLoading(100);
                            while (engine.GetGame.GetPlayer.GetLife.GetCurrentPoint() > 0)
                        if (engine.GetGame.GetPlayer.KilledBy != "Void")
                        {
                            engine.DieMenu();
                        }
                    }
                    } while (chooseMap == "null");
                    Console.WriteLine(chooseMap);
                    engine.StartGame2(chooseMap); //map, player sprite
                    engine.GetGUI.InitGame2();
                    if (result == 2)
                    {
                        //WIN
                        Console.WriteLine("YOU WIN!");
                        engine.WinMenu2();
                    }
                            while (engine.GetGame.GetPlayer.GetLife.GetCurrentPoint > 0)
                        if (engine.GetGame2.GetPlayer2.KilledBy2 == "Trap")
                        {
                            while (engine.GetGame2.GetPlayer2.GetLife2.GetCurrentPoint2() > 0)
                            {
                                engine.GetGame2.GetPlayer2.GetLife2.DecreasedPoint2(1);
                                if (spGui.ElapsedMilliseconds >= 1000 / fps)
                                {
                                    //GUI
                                    engine.GetGUI.ShowMapMultiplayer();
                                    spGui.Restart();
                                }
                            }
                        }
                        if (engine.GetGame.GetPlayer.KilledBy != "void")
                        {
                            engine.DieMenu();
                        }
                      
                        if (engine.GetGame2.GetPlayer2.KilledBy2 != "void")
                        {
                            engine.DieMenu2();
                        }
                    }
                    MapEditor m = new MapEditor();
                    m.Run(window);

            app.StartApp();
        }

    }
}
