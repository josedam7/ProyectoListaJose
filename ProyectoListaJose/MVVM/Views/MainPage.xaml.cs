using ProyectoListaJose.MVVM.Models;
using ProyectoListaJose.MVVM.ViewModels;

namespace ProyectoListaJose.MVVM.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new DataViewModel();
    }
    private void OnTareaTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item != null)
        {
            var tarea = (Tarea)e.Item;
            var viewModel = (DataViewModel)BindingContext;
            viewModel.IrADetalleTareaCommand.Execute(tarea);
        }
    }
}
