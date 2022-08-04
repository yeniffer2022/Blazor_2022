using Blazor.Data;
using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    public class DetalleFacturaServicio : IDetalleFacturaServicio
    {
        private readonly MySQLConfiguracion _configuracion;
        private IDetalleFacturaRepositorio detalleFacturaRepositorio;

        public DetalleFacturaServicio(MySQLConfiguracion configuracion)
        {
            _configuracion = configuracion;
            detalleFacturaRepositorio = new DetalleFacturaRepositorio(configuracion.CadenaConexion);
        }




        public async Task<bool> Nuevo(DetalleFactura detalleFactura)
        {
            return await detalleFacturaRepositorio.Nuevo(detalleFactura);
        }
    }
}
