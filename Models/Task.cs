﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tasks.Models
{
    public class Task : INotifyPropertyChanged
    {

        private string _name;

        public string Name
        {
            get => _name;
            set 
            { 
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get => _description;
            set 
            { 
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _priority;

        public string Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}