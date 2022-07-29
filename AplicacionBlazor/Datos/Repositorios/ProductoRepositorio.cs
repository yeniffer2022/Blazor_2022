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
    public class ProductoRepositorio : IProductoRepositorio
    {
         private string CadenaConexion;

         public ProductoRepositorio(string cadenaConexion)
         {
             CadenaConexion=cadenaConexion;
         }

         private MySqlConnection Conexion()
         {
             return new MySqlConnection(CadenaConexion);
         }

        public async Task<bool> Actualizar(Producto producto)
        {
            bool result = false;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"UPDATE producto SET  Descripcion = @Descripcion , Existencia = @Existencia, Precio = @Precio
                                 WHERE Codigo=@Codigo;";

                result =Convert.ToBoolean(await conexion.ExecuteAsync(sql, producto));

            }
            catch (Exception ex)
            {
            }
            return result;
        }
    

        public async Task<bool> Eliminar(Producto producto)
        {
            bool result = false;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"DELETE  FROM producto WHERE Codigo=@Codigo;";
                result =Convert.ToBoolean(await conexion.ExecuteAsync(sql, new { producto.Codigo }));

            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public async Task<IEnumerable<Producto>> GetLista()
        {
            IEnumerable<Producto> lista = new List<Producto>();
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @" SELECT * FROM producto;";
                lista = await conexion.QueryAsync<Producto>(sql);
            }
            catch (Exception ex)
            {
            }
            return lista;
        }

        public async Task<Producto> GetPorCodigo(string codigo)
        {
            Producto producto = new Producto();
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @" SELECT * FROM producto WHERE Codigo = @Codigo;";
                producto = await conexion.QueryFirstAsync<Producto>(sql, new { codigo });
            }
            catch (Exception)
            {
            }
            return producto;
        }

        public async Task<bool> Nuevo(Producto producto)
        {
            bool result = false;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"INSERT INTO producto (Codigo, Descripcion, Existencia, Precio) 
                                 VALUES (@Codigo, @Descripcion, @Existencia, @Precio);";

                result =Convert.ToBoolean(await conexion.ExecuteAsync(sql, producto));

            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}
