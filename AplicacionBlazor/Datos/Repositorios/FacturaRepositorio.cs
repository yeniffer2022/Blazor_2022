using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        private string CadenaConexion;

        public FacturaRepositorio(string cadenaConexion)
        {
            CadenaConexion=cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }


        public async Task<int> Nueva(Factura factura)
        {
            int idFactura = 0;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"INSERT INTO factura (Fecha, Cliente, ISV, Descuento, SubTotal, Total, CodigoUsuario) 
                                 VALUES (@Fecha, @Cliente, @ISV, @Descuento, @SubTotal, @Total, @CodigoUsuario); SELECT LAST_INSERT_ID()";

                idFactura = Convert.ToInt32( await conexion.ExecuteScalarAsync(sql, factura));


            }
            catch (Exception ex)
            {
            }
            return idFactura;
        }
    }
}
