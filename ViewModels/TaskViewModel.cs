using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tasks.Models;
using ViewModels.Commands;

namespace Tasks.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Task> _tasks;
        private string _name;
        private string _description;
        private string _priority;
        private bool _shouldClear;
        private bool _shouldSelectNext;

        private Task _selectedItem;

        public Task SelectedTask
        {
            get { return _selectedItem; }
            set 
            { 
                Name = value?.Name;
                Description = value?.Description;
                Priority = value?.Priority;

                _selectedItem = value;
                (RemoveTaskCommand as GeneralCommand)?.OnCanExecuteChanged();
                (ModifyTaskCommand as GeneralCommand)?.OnCanExecuteChanged();
                (ClearInputsCommand as GeneralCommand)?.OnCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddTaskCommand { get; set; }

        public ICommand RemoveTaskCommand { get; set; }

        public ICommand ClearInputsCommand { get; set; }

        public ICommand ModifyTaskCommand { get; set; }


        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                (AddTaskCommand as GeneralCommand)?.OnCanExecuteChanged();
                (ClearInputsCommand as GeneralCommand)?.OnCanExecuteChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
                (AddTaskCommand as GeneralCommand)?.OnCanExecuteChanged();
                (ClearInputsCommand as GeneralCommand)?.OnCanExecuteChanged();
            }
        }

        public string Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged();
                (AddTaskCommand as GeneralCommand)?.OnCanExecuteChanged();
                (ClearInputsCommand as GeneralCommand)?.OnCanExecuteChanged();
            }
        }

        public bool ShouldClear 
        { 
            get => _shouldClear; 
            set
            {
                _shouldClear = value;
                OnPropertyChanged();
            }
        }

        public bool ShouldSelectNext 
        { 
            get => _shouldSelectNext;
            set
            {
                _shouldSelectNext = value;
                OnPropertyChanged();
            }
        }

        public TaskViewModel()
        {
            SetInitialData();
            AddTaskCommand = new GeneralCommand(ExecuteAddTask, CanExecuteAddTask);
            RemoveTaskCommand = new GeneralCommand(ExecuteRemoveTask, CanExecuteRemoveTask);
            ClearInputsCommand = new GeneralCommand(ExecuteClearInputs, CanExecuteClearInputs);
            ModifyTaskCommand = new GeneralCommand(ExecuteModifyTask, CanExecuteModifyTask);
        }

        private void SetInitialData()
        {
            _tasks = new ObservableCollection<Task>()
            {
                new Task()
                {
                    Name = "Initial",
                    Description = "Initial",
                    Priority = "Low"
                }
            };


            SelectedTask = null;
            Name = string.Empty;
            Description = string.Empty;
            Priority = string.Empty;
        }

        
        private void ExecuteAddTask(object obj)
        {
            var newTask = new Task()
            {
                Name = Name,
                Description = Description,
                Priority = Priority,
            };

            Tasks.Add(newTask);

            if (ShouldClear)
            {
                ClearInputs();
            }
        }

        private void ClearInputs()
        {
            Name = string.Empty;
            Description = string.Empty;
            Priority = string.Empty;
            SelectedTask = null;
        }

        private void ExecuteModifyTask(object obj)
        {
            SelectedTask.Name = Name;
            SelectedTask.Description = Description;
            SelectedTask.Priority = Priority;
        }

        private void ExecuteRemoveTask(object obj)
        {

            Task nextTask = null;

            if (ShouldSelectNext)
            {
                nextTask = SelectNext();
            }

            Tasks.Remove(SelectedTask);

            SelectedTask = nextTask;
        }

        private void ExecuteClearInputs(object obj)
        {
            Name = null;
            Description = null;
            Priority = null;
        }

        private bool CanExecuteAddTask(object obj)
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(Priority);
        }

        private bool CanExecuteClearInputs(object obj)
        {
            return (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Description) || !string.IsNullOrEmpty(Priority)) && (SelectedTask is null);
        }

        private bool CanExecuteModifyTask(object obj)
        {
            return !(SelectedTask is null);
        }

        private bool CanExecuteRemoveTask(object obj)
        {
            return !(SelectedTask is null);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Task SelectNext()
        {
            if(Tasks.Count <= 1)
            {
                return null;
            }

            int currentSelectedIndex = Tasks.IndexOf(SelectedTask);

            if(currentSelectedIndex == Tasks.Count - 1)
            {
                return Tasks[currentSelectedIndex - 1];
            }

            return Tasks[currentSelectedIndex + 1];

        }
    }
}