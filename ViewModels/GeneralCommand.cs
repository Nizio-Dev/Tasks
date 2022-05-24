using System;
using System.Windows.Input;

namespace ViewModels.Commands
{
	public class GeneralCommand : ICommand
	{
		private readonly Action<object> _execute = null;
		private readonly Predicate<object> _canExecute = null;

		public GeneralCommand(Action<object> execute)
			: this(execute, null) { }

		public GeneralCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return _canExecute?.Invoke(parameter) ?? true;
		}

		public void Execute(object parameter)
		{
			_execute?.Invoke(parameter);
		}

		public void OnCanExecuteChanged()
		{
			if(CanExecuteChanged != null)
            {
				CanExecuteChanged(this, EventArgs.Empty);
			}
			
		}
	}
}
