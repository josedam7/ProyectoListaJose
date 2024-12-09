using ProyectoListaJose.MVVM.Models;
using ProyectoListaJose.MVVM.ViewModels;

namespace ProyectoListaJose.MVVM.Views;

public partial class TareaDetallePage : ContentPage
{
	public TareaDetallePage(Tarea tarea)
	{
		InitializeComponent();
        var viewModel = new DataViewModel();
        viewModel.TareaSeleccionada = tarea;
        BindingContext = viewModel;
    }
}