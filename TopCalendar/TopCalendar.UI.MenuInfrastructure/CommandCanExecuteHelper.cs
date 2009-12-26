using System;

namespace TopCalendar.UI.MenuInfrastructure
{
	/// <summary>
	/// Klasa pomocnicza do przekazywania do menu informacji o tym, czy dany wpis w menu
	/// moze sie w biezacym momencie wykonac. Zmiana wartosci CanExecute w tym obiekcie
	/// pociaga za soba zmiane stanu wpisu w menu, ktorego bezposrednio nie mozemy modyfikowac.
	/// </summary>
	public class CommandCanExecuteHelper
	{
		private bool _canExecute = true;

		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Tworzy obiekt klasy do zarzadzania stanem wpisu w menu
		/// i ustawia stan na dostepny
		/// </summary>
		public CommandCanExecuteHelper() : this(true)
		{
		}

		/// <summary>
		/// Tworzy obiekt klasy do zarzadzania stanem wpisu w menu
		/// </summary>
		/// <param name="value">Domyslny stan wpisu - true, jesli dostepny</param>
		public CommandCanExecuteHelper(bool value)
		{
			_canExecute = value;
		}

		/// <summary>
		/// Wajcha do zarzadzania stanem wpisu w menu. Ustawienie na false powoduje wyszarzenie.
		/// </summary>
		public bool CanExecute
		{
			get
			{
				return _canExecute;
			}
			set
			{
				_canExecute = value;
				if(CanExecuteChanged != null)
					CanExecuteChanged(null, EventArgs.Empty);
			}
		}
	}
}