﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrainDisruptionHandler
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class WindowMain : Window
	{
		private static Dictionary<string, Label> labels = new Dictionary<string, Label>();
		private static Dictionary<string, Button> buttons = new Dictionary<string, Button>();
		private static Dictionary<string, PasswordBox> pwBoxes = new Dictionary<string, PasswordBox>();
		private static Dictionary<string, TextBox> txtBoxes = new Dictionary<string, TextBox>();
		
		void Btn_Local_Click(object sender, RoutedEventArgs e)
		{
			int accessLevel = UtilsDB.AuthLocalAccount(txtBoxes["username"].Text, pwBoxes["password"].Password);
			switch (accessLevel)
			{
				case 0:
					MessageBox.Show("User account login success.");
					break;
				case 1:
					MessageBox.Show("Admin account login success.");
					WindowAdminPanel adminPanel = new WindowAdminPanel();
					adminPanel.Show();
					this.Close();
					break;
				default:
					MessageBox.Show("Incorrect username / password.","Login Failed",MessageBoxButton.OK,MessageBoxImage.Warning);
					break;
			}

		}

		void Btn_OAuth_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("This feature is not implemented.\nPlease login using a local account.","Not Available",MessageBoxButton.OK,MessageBoxImage.Error);
		}

		void Btn_SignUp_Click(object sender, RoutedEventArgs e)
		{
			WindowSignUp SignUp = new WindowSignUp();
			SignUp.Show();
			this.Close();
		}
		public WindowMain()
		{
			InitializeComponent();

			// Dynamic construction of login window

			labels["username"] = UtilsGUI.CreateLabel("Username", "username");
			StackPanelAuthenticate.Children.Add(labels["username"]);

			txtBoxes["username"] = UtilsGUI.CreateTextBox("username");
			StackPanelAuthenticate.Children.Add(txtBoxes["username"]);

			labels["password"] = UtilsGUI.CreateLabel("Password", "password");
			StackPanelAuthenticate.Children.Add(labels["password"]);

			pwBoxes["password"] = UtilsGUI.CreatePasswordBox("password");
			StackPanelAuthenticate.Children.Add(pwBoxes["password"]);

			buttons["local"] = UtilsGUI.CreateButton("Login", "Local");
			buttons["oauth"] = UtilsGUI.CreateButton("Sign in with Google", "OAuth");
			buttons["signup"] = UtilsGUI.CreateButton("Sign Up", "SignUp");
			foreach (var item in buttons)
			{
				StackPanelAuthenticate.Children.Add(item.Value);
			}

			buttons["local"].Click += new RoutedEventHandler(Btn_Local_Click);
			buttons["oauth"].Click += new RoutedEventHandler(Btn_OAuth_Click);
			buttons["signup"].Click += new RoutedEventHandler(Btn_SignUp_Click);

			pwBoxes["password"].Password = "Airways__3";
			txtBoxes["username"].Text = "agentsquash";
		}
	}
}
