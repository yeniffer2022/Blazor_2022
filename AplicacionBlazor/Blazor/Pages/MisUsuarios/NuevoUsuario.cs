using Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;
using CurrieTechnologies.Razor.SweetAlert2;

namespace Blazor.Pages.MisUsuarios
{
    partial class NuevoUsuario
    {
        [Inject] private IUsuarioServicio _usuarioServicio { get; set; }

        [Inject] private SweetAlertService Swal { get; set; }

        [Inject] NavigationManager _navigationManager { get; set; }

        private Usuario user = new Usuario();



        protected async void Guardar()
        {
            if (string.IsNullOrEmpty(user.Codigo) || string.IsNullOrEmpty(user.Nombre) 
                || string.IsNullOrEmpty(user.Clave) || string.IsNullOrEmpty(user.Rol) || user.Rol =="Seleccionar")
    
            {
                return;
            }

            bool inserto = await _usuarioServicio.Nuevo(user);

            if (inserto)
            {
                await Swal.FireAsync("Felicidades" , "Usuario guardado con exito", SweetAlertIcon.Success);
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

    }
}

enum Roles
{
    Seleccionar,
    Administrador,
    Usuario
}
