using Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Blazor.Pages.Facturacion
{
    partial class NuevaFactura
    {
        [Inject] private IFacturaServicio facturaServicio { get; set; }
        [Inject] private IProductoServicio productoServicio { get; set; }

        [Inject] private IDetalleFacturaServicio detalleFacturaServicio { get; set; }

        [Inject] private SweetAlertService Swal { get; set; }

        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private IHttpContextAccessor httpContextAccessor { get; set; }
        [Inject] private IUsuarioServicio _usuarioServicio { get; set; }

        public Factura factura = new Factura();
        public List<DetalleFactura> listaDetalleFactura = new List<DetalleFactura>();
        private Usuario user = new Usuario();

        public Producto producto = new Producto();
        private string codigoProducto { get; set; }
        private string cantidad { get; set; }

        protected override async Task OnInitializedAsync()
        {
            user = await _usuarioServicio.GetPorCodigo(httpContextAccessor.HttpContext.User.Identity.Name);
            factura.Fecha= DateTime.Now;
        }

        private async void BuscarProducto(KeyboardEventArgs args)
        {
            if (args.Key == "Enter")
            {
                producto = await productoServicio.GetPorCodigo(codigoProducto);
            }
        }

        protected async Task AgregarProducto(MouseEventArgs args)
        {
            if (args.Detail !=0)
            { 
                if (producto != null)
                {
                    DetalleFactura detalle = new DetalleFactura();
                    detalle.Producto = producto.Descripcion;
                    detalle.CodigoProducto = producto.Codigo;
                    detalle.Cantidad = Convert.ToInt32(cantidad);
                    detalle.Precio= producto.Precio;
                    detalle.Total = producto.Precio * Convert.ToInt32(cantidad);
                    listaDetalleFactura.Add(detalle);

                    producto.Codigo = string.Empty;
                    producto.Descripcion = string.Empty;
                    producto.Precio = 0;
                    producto.Existencia = 0;
                    cantidad = string.Empty;
                    codigoProducto =string.Empty;

                    factura.SubTotal = factura.SubTotal + detalle.Total;
                    factura.ISV = factura.SubTotal * 0.15M;
                    factura.Total = factura.SubTotal + factura.ISV - factura.Descuento;
                }
                
            }
        }

        protected async Task Guardar()
        {
            factura.CodigoUsuario = httpContextAccessor.HttpContext.User.Identity.Name;
            int idFactura = await facturaServicio.Nueva(factura);
          

            if (idFactura != 0)
            {
                foreach (var item in listaDetalleFactura)
                {
                item.IdFactura = idFactura;
                await detalleFacturaServicio.Nuevo(item);
                }
                await Swal.FireAsync("Felicidades", "Factura guardada con exito", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("Error", "No se pudo guardar la factura", SweetAlertIcon.Error);
            }
        }
    }
}
