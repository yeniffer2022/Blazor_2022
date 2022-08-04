using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Datos.Repositorios
{
    public class DetalleFacturaRepositorio : IDetalleFacturaRepositorio
    {
        private string CadenaConexion;

        public DetalleFacturaRepositorio(string cadenaConexion)
        {
            CadenaConexion=cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }


        public async Task<bool> Nuevo(DetalleFactura detalleFactura)
        {
            bool inserto = false;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"INSERT INTO detallefactura (IdFactura, CodigoProducto, Precio, Cantidad, Total) 
                                 VALUES (@IdFactura, @CodigoProducto, @Precio, @Cantidad, @Total);"; 

                inserto = Convert.ToBoolean(await conexion.ExecuteAsync(sql, detalleFactura));


            }
            catch (Exception ex)
            {
            }
            return inserto;
        }
    }
}
