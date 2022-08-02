using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    partial class EditarProducto
    {
        [Inject] IProductoServicio productoServicio { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }

        [Parameter] public string Codigo { get; set; }
        [Inject] private SweetAlertService Swal { get; set; }

        private Producto producto = new Producto();

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Codigo))
            {
                producto = await productoServicio.GetPorCodigo(Codigo);
            }
        }
        protected async Task Guardar()
        {
            if (string.IsNullOrEmpty(producto.Codigo) || string.IsNullOrEmpty(producto.Descripcion))
            {
                return;
            }

            bool actualizo = await productoServicio.Actualizar(producto);
            if (actualizo)
            {
                await Swal.FireAsync("Advertencia", "Producto guardado con exito", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("Advertencia", "No se pudo guardar el producto", SweetAlertIcon.Error);
            }

        }

        protected async Task Cancelar()
        {
            navigationManager.NavigateTo("/Productos");
        }

        protected async Task Eliminar()
        {
            bool elimino = false;

            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "¿Seguro que desea eliminar el producto?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Aceptar",
                CancelButtonText = "Cancelar"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                elimino = await productoServicio.Eliminar(producto);
                if (elimino)
                {
                    await Swal.FireAsync("Felicidades", "Producto eliminado con exito", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Productos");
                }
                else
                {
                    await Swal.FireAsync("Error", "No se pudo eliminar el producto", SweetAlertIcon.Error);
                }
            }


        }
    }
}
