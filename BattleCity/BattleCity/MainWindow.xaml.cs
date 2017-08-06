using System.Windows;
using System.Windows.Input;

namespace BattleCity
{
	/// <summary>
	/// Interaction logic for the main window
	/// </summary>
	public partial class MainWindow : Window
	{
		StateMachine stateMachine;
		public MainWindow()
		{
			InitializeComponent();
			stateMachine = StateMachine.GetInstance(this);
		}

		private void InitializeMainScreen()
		{
			stateMachine.DrawScreen();
		}

		private void mainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			InitializeMainScreen();
		}

		private void mainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			stateMachine.Move(e);
		}
	}
}
