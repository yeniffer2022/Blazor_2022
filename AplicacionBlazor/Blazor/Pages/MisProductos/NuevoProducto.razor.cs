using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    partial class NuevoProducto
    {
        [Inject] private IProductoServicio productoServicio { get; set; }

        private Producto prod = new Producto();
        [Inject] private SweetAlertService Swal { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
           
        }


        protected async Task Guardar()
        {
            if (string.IsNullOrEmpty(prod.Codigo) || string.IsNullOrEmpty(prod.Descripcion))
            {
                return;
            }

            Producto productoExistente= new Producto();

            productoExistente = await productoServicio.GetPorCodigo(prod.Codigo);

            if (!string.IsNullOrEmpty(productoExistente.Codigo))
            {
                await Swal.FireAsync("Advertencia", "Ya existe un producto con este código", SweetAlertIcon.Warning);
                return;
            }

            bool inserto = await productoServicio.Nuevo(prod);

            if (inserto)
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
            _navigationManager.NavigateTo("/Productos");
        }
    }
}
