using Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;
using CurrieTechnologies.Razor.SweetAlert2;
namespace Blazor.Pages.MisUsuarios
{
    partial class EditarUsuario
    {
        [Inject] private IUsuarioServicio _usuarioServicio { get; set; }

        [Inject] private SweetAlertService Swal { get; set; }

        [Inject] NavigationManager _navigationManager { get; set; }

        private Usuario user = new Usuario();

        [Parameter] public string Codigo { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Codigo))
            {
                user = await _usuarioServicio.GetPorCodigo(Codigo);
            }
        }

        protected async void Guardar()
        {
            if (string.IsNullOrEmpty(user.Codigo) || string.IsNullOrEmpty(user.Nombre)
                || string.IsNullOrEmpty(user.Clave) || string.IsNullOrEmpty(user.Rol) || user.Rol =="Seleccionar")

            {
                return;
            }

            bool edito = await _usuarioServicio.Actualizar(user);

            if (edito)
            {
                await Swal.FireAsync("Felicidades", "Usuario guardado con exito", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("Error", "No se pudo guardar el usuario", SweetAlertIcon.Error);
            }
            _navigationManager.NavigateTo("/Usuarios");
        }


        protected void Cancelar()
        {
            _navigationManager.NavigateTo("/Usuarios");
        }
        protected async Task Eliminar()
        {
            bool elimino = false;

            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "¿Seguro que desea eliminar el usuario?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Aceptar",
                CancelButtonText = "Cancelar"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                elimino = await _usuarioServicio.Eliminar(user);
                if (elimino)
                {
                    await Swal.FireAsync("Felicidades", "Usuario eliminado con exito", SweetAlertIcon.Success);
                    _navigationManager.NavigateTo("/Usuarios");
                }
                else
                {
                    await Swal.FireAsync("Error", "No se pudo eliminar el usuario", SweetAlertIcon.Error);
                }
            }


        }
    }
}
