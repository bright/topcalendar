﻿using System;
using System.Windows.Controls;
using TopCalendar.UI.Modules.MonthViewer;

namespace TopCalendar.UI.Modules.MonthViewer
{
	/// <summary>
	/// Interaction logic for MonthView.xaml
	/// </summary>
	public partial class MonthView : IMonthView
	{
		public MonthView()
		{
			InitializeComponent();
		}

		public MonthViewPresentationModel ViewModel
		{ 
			get { return DataContext as MonthViewPresentationModel;}
			set { DataContext = value; }
		}
	}
}
