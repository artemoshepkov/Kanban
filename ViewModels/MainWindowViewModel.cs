using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using MyKanban.Models;
using System.IO;

namespace MyKanban.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<NoteTask>? _notesTaskPlanned;

        public ObservableCollection<NoteTask> NotesTaskPlanned
        {
            get
            {
                return _notesTaskPlanned;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _notesTaskPlanned, value);
            }
        }


        private ObservableCollection<NoteTask>? _notesTaskInProgress;

        public ObservableCollection<NoteTask> NotesTaskInProgress
        {
            get 
            { 
                return _notesTaskInProgress; 
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _notesTaskInProgress, value);
            }
        }


        private ObservableCollection<NoteTask>? _notesTaskCompleted;

        public ObservableCollection<NoteTask> NotesTaskCompleted
        {
            get
            {
                return _notesTaskCompleted;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _notesTaskCompleted, value);
            }
        }

        public MainWindowViewModel()
        {
            NotesTaskPlanned = new ObservableCollection<NoteTask>(BuildListNotesTask());
            NotesTaskInProgress = new ObservableCollection<NoteTask>();
            NotesTaskCompleted = new ObservableCollection<NoteTask>();

            AddNoteTaskCommand = ReactiveCommand.Create<string>(AddNoteTask);

            DeleteItemListCommand = ReactiveCommand.Create<NoteTask>(DeleteItemList);
        }

        private NoteTask[] BuildListNotesTask()
        {
            return new NoteTask[]
            {
                new NoteTask("Visual Prog", "Make next lab", StatusNoteTask.Planned),
                new NoteTask("Shopping", "Buy new clothes", StatusNoteTask.Planned),
                new NoteTask("Hospital", "Check health", StatusNoteTask.Planned)
            };
        }


        public ReactiveCommand<string, Unit> AddNoteTaskCommand { get; }

        public ReactiveCommand<NoteTask, Unit> DeleteItemListCommand { get; }


        private void AddNoteTask(string titleCol)
        {
            switch(titleCol)
            {
                case "Planned":
                    NotesTaskPlanned.Add(new NoteTask("Planned task", "Description", StatusNoteTask.Planned));
                    break;
                case "InProgress":
                    NotesTaskInProgress.Add(new NoteTask("In progress task", "Description", StatusNoteTask.Progress));
                    break;
                case "Completed":
                    NotesTaskCompleted.Add(new NoteTask("Completed task", "Description", StatusNoteTask.Completed));
                    break;
            }
        }

        private void DeleteItemList(NoteTask note)
        {
            switch(note.Status)
            {
                case StatusNoteTask.Planned:
                    NotesTaskPlanned.Remove(note);
                    break;
                case StatusNoteTask.Progress:
                    NotesTaskInProgress.Remove(note);
                    break;
                case StatusNoteTask.Completed:
                    NotesTaskCompleted.Remove(note);
                    break;
            }
        }

        public void CreateNewKanban()
        {
            NotesTaskPlanned.Clear();
            NotesTaskInProgress.Clear();
            NotesTaskCompleted.Clear();
        }

        public void SaveKanbanToFile(string path)
        {
            SaveListToFile(path, "Planned task", NotesTaskPlanned);
            SaveListToFile(path, "In progress task", NotesTaskInProgress);
            SaveListToFile(path, "Completed task", NotesTaskCompleted);

        }

        private void SaveListToFile(string path, string titleList, ObservableCollection<NoteTask> list)
        {
            StreamWriter sw = new StreamWriter(path, true);

            sw.WriteLine(titleList);
            foreach (var task in list)
            {
                sw.WriteLine(task.Header);
                sw.WriteLine(task.TaskNote);
            }

            sw.Close();
        }

        public void LoadKanbanFromFile(string path)
        {
            string line;
            StreamReader sr = new StreamReader(path);

            line = sr.ReadLine();

            string title, noteTask;

            try
            {
                while (line != "In progress task")
                {
                    if (line != "Planned task")
                    {
                        title = line;

                        line = sr.ReadLine();

                        noteTask = line;

                        NotesTaskPlanned.Add(new NoteTask(title, noteTask, StatusNoteTask.Planned));
                    }

                    line = sr.ReadLine();
                }

                while (line != "Completed task")
                {
                    if (line != "In progress task")
                    {
                        title = line;

                        line = sr.ReadLine();

                        noteTask = line;

                        NotesTaskInProgress.Add(new NoteTask(title, noteTask, StatusNoteTask.Progress));
                    }

                    line = sr.ReadLine();
                }

                while (line != null)
                {
                    if (line != "Completed task")
                    {
                        title = line;

                        line = sr.ReadLine();

                        noteTask = line;

                        NotesTaskCompleted.Add(new NoteTask(title, noteTask, StatusNoteTask.Completed));
                    }

                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch
            {
                sr.Close ();
            }
        }
    }
}
