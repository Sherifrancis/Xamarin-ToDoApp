﻿using Prism.Navigation;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Auth;
using ToDoApp.Helpers;
using ToDoApp.Models;
using ToDoApp.Repositories.FirestoreRepository;
using ToDoApp.Views.Dialogs;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoApp.ViewModels
{
    public class MorePageViewModel : 
        BaseViewModel,
        IInitialize
    {
        #region Private & Protected

        private IDialogService _dialogService;

        #endregion

        #region Properties

        public ObservableCollection<ListModel> ProjectList { get; set; }
        public string SelectedList { get; set; }

        #endregion

        #region Commands

        public ICommand BackCommand { get; set; }
        public ICommand OpenListDialogCommand { get; set; }

        #endregion

        #region Constructors

        public MorePageViewModel(
            INavigationService navigationService,
            IDialogService dialogService) : base(navigationService)
        {
            _dialogService = dialogService;

            BackCommand = new Command(BackCommandHandler);
            OpenListDialogCommand = new Command(OpenListDialogCommandHandler);
        }

        public  void Initialize(INavigationParameters parameters)
        {
            var list = Preferences.Get("taskFilterByList", "All lists");
            SelectedList = list;
        }

        #endregion

        #region Constructors

        private async void OpenListDialogCommandHandler()
        {
            await _dialogService.ShowDialogAsync(nameof(ListDialog));
        }

        #endregion
    }
}
