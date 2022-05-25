using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tasks.Models
{
    public class Task : INotifyPropertyChanged
    {

        public static readonly string[] Priorities =
        {
            "Low",
            "Medium",
            "High"
        };

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

        private DateTime _creationTime;

        public DateTime CreationTime
        {
            get => _creationTime;
            private set => _creationTime = value;
        }

        private DateTime? _finishTime;

        public DateTime? FinishTime
        {
            get => _finishTime;
            set 
            { 
                _finishTime = value;
                OnPropertyChanged();
            }
        }


        public Task()
        {
            CreationTime = DateTime.Now.Date;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}