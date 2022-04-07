using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using Avalonia;
using Avalonia.Platform;
using System.Reflection;
using System.IO;

namespace MyKanban.Models
{
    public enum StatusNoteTask
    {
        Planned = 0,
        Progress = 1,
        Completed = 2
    }


    public class NoteTask : INotifyPropertyChanged
    {
        private StatusNoteTask _status;

        public StatusNoteTask Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;

                NotifyPropertyChanged();
            }
        }

        private string? _header;

        public string Header
        {
            get 
            {
                return _header; 
            }
            set
            {
                if (value != null)
                {
                    _header = value;

                    NotifyPropertyChanged();
                }
            }
        }


        private string? _taskNote;

        public string TaskNote
        {
            get
            {
                return _taskNote;
            }
            set
            {
                if (value != null)
                {
                    _taskNote = value;

                    NotifyPropertyChanged();
                }
            }
        }


        private string _pathImage;

        public string PathImage
        {
            get
            {
                return _pathImage;
            }
            set
            {
                if (value != null)
                {
                    _pathImage = value;

                    NotifyPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private Bitmap _image;

        public Bitmap Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                NotifyPropertyChanged();
            }
        }

        public NoteTask(string header)
        {
            Header = header;
        }
        public NoteTask(string header, string taskNote, StatusNoteTask status) : this(header)
        {
            TaskNote = taskNote;
            Status = status;
        }
        public NoteTask(string header, string taskNote, StatusNoteTask status, string pathImage) : this(header, taskNote, status)
        {
            PathImage = pathImage;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
