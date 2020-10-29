using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectroSharp.DA.Entities;
using ElectroSharp.DA;
using System.ComponentModel;
using ElectroSharp.UI.Models;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace ElectroSharp.UI.ViewModels
{
    public class ElementsViewModel : Screen
    {
        public BindableCollection<ElectronicElement> ElectronicElementsDBTable { get; set; }
        private string imagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\Image\\";
        private ElectronicElement selectedItem;
        public ElectronicElement SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                if (SelectedItem.Type == ElementType.Source)
                {
                    ParallelBtnEnabled = false;
                    SeriesBtnEnabled = false;
                }
                else if (electronicElements.Where(x => x.ConnectedType == ConnectionType.Parallel).Count() > 0)
                {
                    ParallelBtnEnabled = true;
                    SeriesBtnEnabled = false;
                }
                else
                {
                    ParallelBtnEnabled = true;
                    SeriesBtnEnabled = true;
                }
            }
        }

        private bool parallelBtnEnabled = true;
        public bool ParallelBtnEnabled
        {
            get { return parallelBtnEnabled; }
            set
            {
                parallelBtnEnabled = value;
                NotifyOfPropertyChange(() => ParallelBtnEnabled);
                NotifyOfPropertyChange(() => SourceBtnEnabled);
            }
        }
        private bool seriesBtnEnabled = true;
        public bool SeriesBtnEnabled
        {
            get { return seriesBtnEnabled; }
            set
            {
                seriesBtnEnabled = value;
                NotifyOfPropertyChange(() => SeriesBtnEnabled);
                NotifyOfPropertyChange(() => SourceBtnEnabled);
            }
        }
        public bool SourceBtnEnabled
        {
            get { return !ParallelBtnEnabled; }
        }

        private BindableCollection<ElectronicElement> electronicElements;

        public ElementsViewModel(BindableCollection<ElectronicElement> electronicElements_)
        {
            DomainContext context = new DomainContext();
            ElectronicElementsDBTable = new BindableCollection<ElectronicElement>(context.ElementsTable);
            electronicElements = electronicElements_;
        }
        public void btn_AddParallelElement()
        {
            try
            {
                SelectedItem.ConnectedType = ConnectionType.Parallel;
                selectedItem.ImagePath = imagePath + "parallel.png";
                SetPosition(selectedItem);
                electronicElements.Add(SelectedItem);
                TryClose();
            }
            catch(NullReferenceException ex)
            {

            }
        }
        public void btn_AddSeriesElement() 
        {
            try
            {
                SelectedItem.ConnectedType = ConnectionType.Series;
                selectedItem.ImagePath = imagePath + "series.png";
                SetPosition(selectedItem);
                electronicElements.Add(SelectedItem);
                TryClose();
            }
            catch(Exception ex)
            {

            }
        }
        public void btn_AddSource()
        {
            try
            {
                if (electronicElements.Where(x => x.Type == ElementType.Source).Count() == 0)
                {
                    selectedItem.ImagePath = imagePath + "source.png";
                    SetPosition(selectedItem);
                    electronicElements.Add(selectedItem);
                }
                else
                    electronicElements.Where(x => x.Type == ElementType.Source).First().ElementValue += SelectedItem.ElementValue;

                TryClose();
            }
            catch(Exception ex)
            {

            }
        }
        private void SetPosition(ElectronicElement element)
        {
            if (element.ConnectedType == ConnectionType.Series)
            {
                element.X = (electronicElements.Count+2)*85;
                element.Y = 18;
            }
            else if (element.ConnectedType == ConnectionType.Parallel)
            {
                element.X = (electronicElements.Count+2) *90; //to trzeba po ustawiac
                element.Y = 29;
            }
            else if (element.Type == ElementType.Source && electronicElements.Where(x=>x.Type == ElementType.Source ).Count() == 0)
            {
                element.X = 12;
                element.Y = 100;
            }
        }
    }

}
