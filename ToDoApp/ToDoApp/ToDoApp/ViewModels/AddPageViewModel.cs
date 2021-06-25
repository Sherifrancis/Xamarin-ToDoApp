﻿using Prism.Navigation;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class AddPageViewModel : BaseViewModel
    {
        #region Private & Protected

        private IFirestoreRepository<TaskModel> _tasksRepository;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region Properties

        public string Type { get; set; }
        public ObservableCollection<string> ItemList { get; set; }
        public ObservableCollection<Color> ColorList { get; set; }
        public TaskModel AddTask { get; set; }

        #endregion

        #region Commands

        public ICommand ChangeTypeCommand { get; set; }
        public ICommand CreateCommand { get; }

        #endregion

        #region Constructors

        public AddPageViewModel(
            INavigationService navigationService,
            IFirestoreRepository<TaskModel> tasksRepository) : base(navigationService)
        {
            _tasksRepository = tasksRepository;

            ChangeTypeCommand = new Command<string>(ChangeTypeCommandHandler);
            CreateCommand = ReactiveCommand.Create(CreateCommandHandler);

            AddTask = new TaskModel();

            ItemList = Constants.AddOptions;
            ColorList = Constants.ListColorList;

            Type = "task";
        }

        #endregion

        #region Command Handlers

        private void ChangeTypeCommandHandler(string type)
        {
            Type = type;
        }

        private async Task CreateCommandHandler()
        {
            try
            {
                var auth = DependencyService.Get<IFirebaseAuthentication>();
                var userId = auth.GetUserId();
                var model = new TaskModel()
                {
                    archived = false,
                    list = "Inbox",
                    task = AddTask.task,
                    userId = auth.GetUserId(),
                    date = DateTime.Parse(AddTask.date).ToString("dd/MM/yyyy")
                };
                await _tasksRepository.Add(model);
                await _navigationService.GoBackAsync();
            }
            catch(Exception ex)
            {
                //display error message
                Debug.Write(ex.Message);
            }
        }

        #endregion
    }
}
