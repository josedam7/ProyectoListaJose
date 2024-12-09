using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ProyectoListaJose.MVVM.Models;
using ProyectoListaJose.MVVM.Views;

namespace ProyectoListaJose.MVVM.ViewModels
{
    public class DataViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Tarea> _tareas;
        private Tarea _tareaSeleccionada;

        public ObservableCollection<Tarea> Tareas
        {
            get => _tareas;
            set
            {
                if (_tareas != null)
                {
                   _tareas.CollectionChanged -= OnTareasCollectionChanged;
                }

                _tareas = value;

                if (_tareas != null)
                {
                   _tareas.CollectionChanged += OnTareasCollectionChanged;
                }

                OnPropertyChanged(nameof(Tareas));
                OnPropertyChanged(nameof(HayTareas));
            }
        }

        public Tarea TareaSeleccionada
        {
            get => _tareaSeleccionada;
            set
            {
                _tareaSeleccionada = value;
                OnPropertyChanged(nameof(TareaSeleccionada));
            }
        }

        public bool HayTareas => Tareas != null && Tareas.Count > 0;

        public ICommand AgregarTareaCommand { get; }
        public ICommand EliminarTareaCommand { get; }
        public ICommand IrADetalleTareaCommand { get; }
        public ICommand GuardarCambiosCommand { get; }

        public DataViewModel()
        {
            Tareas = new ObservableCollection<Tarea>();
            AgregarTareaCommand = new Command(AgregarTarea);
            EliminarTareaCommand = new Command(EliminarTarea);
            IrADetalleTareaCommand = new Command<Tarea>(IrADetalleTarea);
            GuardarCambiosCommand = new Command(GuardarCambios);
        }

        private void AgregarTarea()
        {
            Tareas.Add(new Tarea
            {
                Nombre = "Nueva Tarea",
                Completada = false
            });
        }

        private void EliminarTarea()
        {
            if (TareaSeleccionada != null)
            {
                Tareas.Remove(TareaSeleccionada);
                TareaSeleccionada = null; 
            }
        }

        private async void IrADetalleTarea(Tarea tarea)
        {
            TareaSeleccionada = tarea;
            await Application.Current.MainPage.Navigation.PushAsync(new TareaDetallePage(TareaSeleccionada));
        }

        private async void GuardarCambios()
        {
            if (TareaSeleccionada != null)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private void OnTareasCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HayTareas));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

