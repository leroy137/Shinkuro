using System;
using System.Collections.Generic;
using System.Text;
using Shinkuro.ViewModels.Base;
using Shinkuro.Models;
using Shinkuro.Infrastracture.Commands;
using Shinkuro.Infrastracture.Commands.Base;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using Shinkuro.Services;
using System.Windows.Media.Imaging;
using System.IO;

namespace Shinkuro.ViewModels
{
    internal class SettingsPageViewModel : ViewModelBase
    {

        private BitmapImage _fileLogo;
        public ApplicationCoreContext Context { get; set; }

        public BitmapImage FileLogo 
        {
            get { return _fileLogo; }
            set { Set<BitmapImage>(ref _fileLogo, value); } 
        }

        public BitmapImage FileLogoOpen { get; set; }

        private Competition _innerCompetition;
        private Competition _cloneCompetition;

        private string _competitionName;

        public Competition Competition 
        {   
            get { return _innerCompetition; } 
            set { Set<Competition>(ref _innerCompetition, value); } 
        }
        public Competition CompetitionCloneView
        {
            get { return _cloneCompetition; }
            set { Set<Competition>(ref _cloneCompetition, value); }
        }

        public String CompetitionName
        {
            get { return _competitionName; }
            set { Set<String>(ref _competitionName, value); }
        }

        public ICommand SaveSettingsCommand { get; set; }
        public ICommand ResetSettingsCommand { get; set; }
        public ICommand UploadLogoCompetition { get; set; }
        public ICommand CancelChangeLogo { get; set; }

        public SettingsPageViewModel(ApplicationCoreContext context)
        {
            SaveSettingsCommand = new RelayCommand(SaveSettingsCommandExecute, SaveSettingsCommandCanExecute);
            ResetSettingsCommand = new RelayCommand(ResetSettingsCommandExecute, ResetSettingsCommandCanExecute);
            UploadLogoCompetition = new RelayCommand(UploadLogoCompetitionExecute, UploadLogoCompetitionCanExecute);
            CancelChangeLogo = new RelayCommand(CancelChangeLogoExecute, CancelChangeLogoCanExecute);
            Context = context;
            Competition = context.CurrentCompetition;
            CompetitionName = context.CurrentCompetition.Name;
            CompetitionCloneView = new Competition(Competition.Name, Competition.StartDate, Competition.FinishDate, Competition.Description, Competition.Place, Competition.Organizator, Competition.Contacts);

            FileLogo = new BitmapImage();
            FileLogoOpen = new BitmapImage();
            if(!String.IsNullOrEmpty(Competition.LogoPath))
            {
                InitLogotip(Competition.LogoPath);
            }
        }

        private bool InitLogotip(String Logo)
        {
            if(String.IsNullOrEmpty(Logo))
            {
                FileLogoOpen = new BitmapImage();
                return true;
            }

            var stream = File.OpenRead(Logo);
            FileLogoOpen.BeginInit();
            FileLogoOpen.CacheOption = BitmapCacheOption.OnLoad;
            FileLogoOpen.DecodePixelWidth = 150;
            FileLogoOpen.StreamSource = stream;
            FileLogoOpen.UriSource = new Uri(Logo,UriKind.Absolute);
            FileLogoOpen.EndInit();
            stream.Close();
            return true;
        }

        private void SaveSettingsCommandExecute(object obj)
        {
            try
            {
                CompetitionCloneView.Name = CompetitionName;
                Context.UpdateCompetition(CompetitionCloneView);

                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool SaveSettingsCommandCanExecute(object obj)
        {
            return !(Competition.Equals(CompetitionCloneView) && CompetitionName == Competition.Name);
        }

        private void ResetSettingsCommandExecute(object vobj)
        {
            try
            {
                Competition c = new Competition(Competition.Name, Competition.StartDate, Competition.FinishDate, Competition.Description, Competition.Place, Competition.Organizator, Competition.Contacts);
                c.LogoPath = Competition.LogoPath;
                CompetitionCloneView = c;
                CompetitionName = c.Name;
                if (InitLogotip(Competition.LogoPath))
                {
                    FileLogo = FileLogoOpen;
                    FileLogoOpen = new BitmapImage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool ResetSettingsCommandCanExecute(object obj)
        {
            return !(Competition.Equals(CompetitionCloneView)&&CompetitionName==Competition.Name);
        }

        private void UploadLogoCompetitionExecute(Object obj)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = false };
                openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png, *.jpeg)|*.bmp;*.jpg;*.png;*.jpeg";
                openFileDialog.ShowDialog();
                string[] imagesPath = openFileDialog.FileNames; // пути к файлам
                if(imagesPath.Length!=0)
                {
                    String imagePath = imagesPath[0];
                    CompetitionCloneView.LogoPath = imagePath;
                    if(InitLogotip(imagePath))
                    {
                        FileLogo = FileLogoOpen;
                        FileLogoOpen = new BitmapImage();
                    }
                }
            }
            catch(Exception ex)
            {
                FileLogoOpen = new BitmapImage();
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private bool UploadLogoCompetitionCanExecute(Object obj)
        {
            return true;
        }

        private void CancelChangeLogoExecute(Object obj)
        {

        }

        private bool CancelChangeLogoCanExecute(Object obj)
        {
            return true;
        }

    }
}
