using Blazor.Data;
using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    public class FacturaServicio : IFacturaServicio
    {
        private readonly MySQLConfiguracion _configuracion;
        private IFacturaRepositorio facturaRepositorio;

        public FacturaServicio(MySQLConfiguracion configuracion)
        {
            _configuracion = configuracion;
            facturaRepositorio = new FacturaRepositorio(configuracion.CadenaConexion);
        }



        public async Task<int> Nueva(Factura factura)
        {
            return await facturaRepositorio.Nueva(factura);
        }
    }
}
