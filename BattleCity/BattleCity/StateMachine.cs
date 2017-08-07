using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BattleCity.Elements;
using BattleCity.Elements.Tanks;
using BattleCity.Helpers;

namespace BattleCity
{
	public class StateMachine
	{
		enum State { MainScreen, LevelEditor, Level }
		enum Options { ONE_PLAYER, TWO_PLAYERS, EDITOR }

		private const String PATH_TO_LEVEL = "Levels/";
		private static StateMachine instance;
		private Options selectedOption = Options.ONE_PLAYER;
		private State currentState = State.MainScreen;
		private MainWindow window;
		private Canvas mainCanvas;

		private int level = -1;

		private Image background = new Image();
		private Image pointer = new Image();

		private List<DrawableElement> elements;
		private Field[,] fields = new Field[13,13];

		private PlayerTank playerOneTank;
		private PlayerTank playerTwoTank;

		private StateMachine(MainWindow window) {
			background.Source = new BitmapImage(new Uri("Images/startScreen.bmp", UriKind.Relative));
			pointer.Source = new BitmapImage(new Uri("Images/icon.bmp", UriKind.Relative));
			this.window = window;
			mainCanvas = window.mainCanvas;
		}

		public static StateMachine GetInstance(MainWindow window)
		{
			if(instance == null)
			{
				instance = new StateMachine(window);
			}
			return instance;
		}

		public static StateMachine GetInstance()
		{
			return instance;
		}

		public void DrawScreen()
		{
			switch(currentState)
			{
				case State.MainScreen:
					DrawMainScreen(250);
					break;
				case State.Level:
					DrawLevelScreen();
					break;
			}
		}

		public void Move(KeyEventArgs e)
		{
			switch (currentState)
			{
				case State.MainScreen:
					MovementOnMainScreen(e);
					break;
				case State.Level:
					MovementOnLevelScreen(e);
					break;
			}
		}

		internal void Stop(KeyEventArgs e)
		{
			if (currentState == State.Level)
			{
				if (e.Key == Key.Up)
				{
					playerOneTank.StopTank();
				}
				else if (e.Key == Key.Down)
				{
					playerOneTank.StopTank();
				}
				else if (e.Key == Key.Left)
				{
					playerOneTank.StopTank();
				}
				else if (e.Key == Key.Right)
				{
					playerOneTank.StopTank();
				}
			}
		}

		private void MovementOnMainScreen(KeyEventArgs e)
		{
			if (e.Key == Key.Down)
			{
				if (pointer.Margin.Top < 314)
				{
					MoveDown(32);
					selectedOption = selectedOption == Options.ONE_PLAYER ? Options.TWO_PLAYERS : Options.EDITOR;
				}
			}
			if (e.Key == Key.Up)
			{
				if (pointer.Margin.Top > 250)
				{
					MoveDown(-32);
					selectedOption = selectedOption == Options.EDITOR ? Options.TWO_PLAYERS : Options.ONE_PLAYER;
				}
			}
			if (e.Key == Key.Space)
			{
				switch (selectedOption)
				{
					case Options.ONE_PLAYER:
						level = 1;
						playerOneTank = TankFactory.CreatePlayerOneTank();
						currentState = State.Level;
						LoadLevel();
						LevelLoop();
						break;
					case Options.TWO_PLAYERS:
						level = 1;
						playerOneTank = TankFactory.CreatePlayerOneTank();
						playerTwoTank = TankFactory.CreatePlayerTwoTank();
						currentState = State.Level;
						LoadLevel();
						LevelLoop();
						break;
					case Options.EDITOR:
						var levelEditor = new LevelEditor();
						currentState = State.LevelEditor;
						levelEditor.Show();
						window.Hide();
						break;
				}
			}
		}

		private void LevelLoop()
		{
			Task.Run(() =>
			{
				while (currentState == State.Level)
				{
					Stopwatch w = new Stopwatch();
					w.Start();
					window.Dispatcher.Invoke(() => DrawScreen());
					w.Stop();
					TimeSpan ts = w.Elapsed;
					int restOfFrameLength = 40 - (int)w.ElapsedMilliseconds;
					if(restOfFrameLength > 0)
					Thread.Sleep(restOfFrameLength);
				}
			});
		}

		private void MoveDown(int pixels)
		{
			DrawMainScreen(pixels);
		}

		private void DrawMainScreen(int pixels)
		{
			mainCanvas.Children.Clear();
			mainCanvas.Children.Add(background);
			Thickness margin = pointer.Margin;
			pointer.Margin = new Thickness(100, margin.Top + pixels, 0, 0);
			mainCanvas.Children.Add(pointer);
		}

		private void MovementOnLevelScreen(KeyEventArgs e)
		{
			if(e.Key == Key.Up)
			{
				playerOneTank.IsMoving = true;
				playerOneTank.Direction = Direction.Up;
			}
			else if(e.Key == Key.Down)
			{
				playerOneTank.IsMoving = true;
				playerOneTank.Direction = Direction.Down;
			}
			else if(e.Key == Key.Left)
			{
				playerOneTank.IsMoving = true;
				playerOneTank.Direction = Direction.Left;
			}
			else if(e.Key == Key.Right)
			{
				playerOneTank.IsMoving = true;
				playerOneTank.Direction = Direction.Right;
			}
		}

		private void DrawLevelScreen()
		{
			mainCanvas.Children.Clear();
			foreach (var e in elements)
			{
				var field = e as Field;
				if (field != null)
				{
					mainCanvas.Children.Add(field.Draw());
				}
			}
			mainCanvas.Children.Add(TexturesFactory.DrawEagle());
			mainCanvas.Children.Add(playerOneTank.Draw());
			if (selectedOption == Options.TWO_PLAYERS)
			{
				mainCanvas.Children.Add(playerTwoTank.Draw());
			}
		}

		public void LoadLevel()
		{
			elements = LevelSerializer.GetInstance().DeserializeLevel(PATH_TO_LEVEL + "level" + level + ".xml");
			fields = new Field[13, 13];
			mainCanvas.Children.Clear();

			foreach (var e in elements)
			{
				var field = e as Field;
				if (field != null) {
					fields[field.row, field.column] = field;
					field.SetState();
				}
			}
			MovementHelper.GetInstance().levelElements = fields;
		}

		public void MoveFromEditorToMainScreen()
		{
			currentState = State.MainScreen;
			window.Show();
		}
	}
}
