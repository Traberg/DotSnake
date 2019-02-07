using System;
using System.Collections.Generic;

namespace DotSnake.Logic
{
    public class Game
    {
        private readonly GameBoard _gameBoard;
        private readonly UI _ui;
        private readonly UserController _userController;


        private readonly Dictionary<int, int> _levels;


        // game settings this? hmm
        private int _delaySpeed = 200;
        private int _minimumDelaySpeed = 60;
        private readonly int _numberOflevels = 40;
        private readonly int _speedIncreaseValue = 15;

        private int _currentLevel = 1;

        private GameStatus _gameStatus = GameStatus.Menu;

        public Game(GameBoard gameBoard, UI ui1, UserController controller)
        {
            _gameBoard = gameBoard;
            _ui = ui1;
            _userController = controller;

            _levels = new Dictionary<int, int>();


            // TODO FLYT TIL EGEN KLASSE
            int currentDelaySpeed = _delaySpeed;
            int currentSpeedIncreaseValue = _speedIncreaseValue;
            var decreaseConstant = 1;

            for (int i = 1; i <= _numberOflevels; i++)
            {
                if (currentDelaySpeed - currentSpeedIncreaseValue > _minimumDelaySpeed)
                {
                    currentDelaySpeed = currentDelaySpeed - currentSpeedIncreaseValue;

                    _levels.Add(i, currentDelaySpeed - currentSpeedIncreaseValue);

                    if (currentSpeedIncreaseValue > decreaseConstant)
                    {
                        currentSpeedIncreaseValue = currentSpeedIncreaseValue - decreaseConstant;
                    }
                    else if (currentSpeedIncreaseValue > 0 && currentSpeedIncreaseValue <= decreaseConstant)
                    {
                        decreaseConstant = 0;
                    }
                }
                else
                {
                    _levels.Add(i, currentDelaySpeed);
                }
            }

            _userController.NewUserInput += _gameBoard.DirectionChange;
            _gameBoard.GameStateChanged += _ui.Render;
        }

        public void Start()
        {
            while (true)
            {
                switch (_gameStatus)
                {
                    case GameStatus.Playing:
                        var result = _gameBoard.Tick();
                        if (!result.DidEat && !result.Died)
                        {
                            System.Threading.Thread.Sleep(_delaySpeed);
                            _userController.RegisterInput();
                        }
                        else if (result.DidEat)
                        {
                            if (_currentLevel != _numberOflevels)
                            {
                                _currentLevel++;
                                var currentSpeed = _levels[_currentLevel];
                                _delaySpeed = currentSpeed;
                            }
                        }
                        else if (result.Died)
                        {
                            _ui.ClearScreen();
                            _ui.Died();
                            _gameStatus = GameStatus.Menu;
                        }

                        break;

                    case GameStatus.Menu:
                        _ui.EnterMenu();
                        Console.ReadKey();
                        _ui.ClearScreen();
                        _delaySpeed = 200;
                        _gameStatus = GameStatus.Playing;
                        _currentLevel = 1;
                        _gameBoard.Reset();
                        break;
                }
            }
        }
    }
}