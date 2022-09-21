using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DatosPortal
    {
        public static ML.Result Add(ML.DatosPortal datosPortal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "DatosAdd";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[13];

                    collection[0] = new SqlParameter("@IdFolioDeServicio", System.Data.SqlDbType.VarChar);
                    collection[0].Value = datosPortal.IdFolioDeServicio;

                    collection[1] = new SqlParameter("@IdFolioDeServicio", System.Data.SqlDbType.VarChar);
                    collection[1].Value = datosPortal.Prioridad;

                    collection[2] = new SqlParameter("@TipoServicio", System.Data.SqlDbType.VarChar);
                    collection[2].Value = datosPortal.TipoServicio;

                    collection[3] = new SqlParameter("@SucursalConsignatario", System.Data.SqlDbType.VarChar);
                    collection[3].Value = datosPortal.SucursalConsignatario;

                    collection[4] = new SqlParameter("@FechaCaptura", System.Data.SqlDbType.VarChar);
                    collection[4].Value = datosPortal.FechaCaptura;

                    collection[5] = new SqlParameter("@FechaRealizarServicio", System.Data.SqlDbType.VarChar);
                    collection[5].Value = datosPortal.FechaRealizarServicio;

                    collection[6] = new SqlParameter("@OrdenServicio", System.Data.SqlDbType.VarChar);
                    collection[6].Value = datosPortal.OrdenServicio;

                    collection[7] = new SqlParameter("@Importe", System.Data.SqlDbType.Decimal);
                    collection[7].Value = datosPortal.Importe;

                    collection[8] = new SqlParameter("@Divisa", System.Data.SqlDbType.VarChar);
                    collection[8].Value = datosPortal.Divisa;

                    collection[9] = new SqlParameter("@Ter", System.Data.SqlDbType.VarChar);
                    collection[9].Value = datosPortal.Ter;

                    collection[10] = new SqlParameter("@HoraEnvio", System.Data.SqlDbType.VarChar);
                    collection[10].Value = datosPortal.HoraEnvio;

                    collection[11] = new SqlParameter("@Actualización", System.Data.SqlDbType.VarChar);
                    collection[11].Value = datosPortal.Actualización;

                    collection[12] = new SqlParameter("@Estatus", System.Data.SqlDbType.VarChar);
                    collection[12].Value = datosPortal.Estatus;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result GetById(string IdFolioDeServicio)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "GetByIdDatos";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        ML.DatosPortal datosPortal = new ML.DatosPortal();
                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("@IdFolioDeServicio", System.Data.SqlDbType.VarChar);
                        collection[0].Value = datosPortal.IdFolioDeServicio;

                        cmd.Parameters.AddRange(collection);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            DataTable datosTable = new DataTable();
                            da.Fill(datosTable);
                            cmd.Connection.Open();

                            if (datosTable.Rows.Count > 0)
                            {

                                DataRow row1 = datosTable.Rows[0];

                                datosPortal.IdFolioDeServicio = row1[0].ToString();
                                datosPortal.Prioridad = row1[1].ToString();
                                datosPortal.TipoServicio = row1[2].ToString();
                                datosPortal.SucursalConsignatario = row1[3].ToString();
                                datosPortal.FechaCaptura = row1[4].ToString();
                                datosPortal.FechaRealizarServicio = row1[5].ToString();
                                datosPortal.OrdenServicio = row1[6].ToString();
                                datosPortal.Importe = decimal.Parse(row1[7].ToString());
                                datosPortal.Divisa = row1[8].ToString();
                                datosPortal.Ter = row1[9].ToString();
                                datosPortal.HoraEnvio = row1[10].ToString();
                                datosPortal.Actualización = row1[11].ToString();
                                datosPortal.Estatus = row1[12].ToString();
                                result.Object = datosPortal;

                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontraron registros en la tabla Materia";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;

        }
        public static ML.Result Update(ML.DatosPortal datosPortal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "DatosUpdate";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[13];

                    collection[0] = new SqlParameter("@IdFolioDeServicio", System.Data.SqlDbType.VarChar);
                    collection[0].Value = datosPortal.IdFolioDeServicio;

                    collection[1] = new SqlParameter("@Prioridad", System.Data.SqlDbType.VarChar);
                    collection[1].Value = datosPortal.Prioridad;

                    collection[2] = new SqlParameter("@TipoServicio", System.Data.SqlDbType.VarChar);
                    collection[2].Value = datosPortal.TipoServicio;

                    collection[3] = new SqlParameter("@SucursalConsignatario", System.Data.SqlDbType.VarChar);
                    collection[3].Value = datosPortal.SucursalConsignatario;

                    collection[4] = new SqlParameter("@FechaCaptura", System.Data.SqlDbType.VarChar);
                    collection[4].Value = datosPortal.FechaCaptura;

                    collection[5] = new SqlParameter("@FechaRealizarServicio", System.Data.SqlDbType.VarChar);
                    collection[5].Value = datosPortal.FechaRealizarServicio;

                    collection[6] = new SqlParameter("@OrdenServicio", System.Data.SqlDbType.VarChar);
                    collection[6].Value = datosPortal.OrdenServicio;

                    collection[7] = new SqlParameter("@Importe", System.Data.SqlDbType.Decimal);
                    collection[7].Value = datosPortal.Importe;

                    collection[8] = new SqlParameter("@Divisa", System.Data.SqlDbType.VarChar);
                    collection[8].Value = datosPortal.Divisa;

                    collection[9] = new SqlParameter("@Ter", System.Data.SqlDbType.VarChar);
                    collection[9].Value = datosPortal.Ter;

                    collection[10] = new SqlParameter("@HoraEnvio", System.Data.SqlDbType.VarChar);
                    collection[10].Value = datosPortal.HoraEnvio;

                    collection[11] = new SqlParameter("@Actualización", System.Data.SqlDbType.VarChar);
                    collection[11].Value = datosPortal.Actualización;

                    collection[12] = new SqlParameter("@Estatus", System.Data.SqlDbType.VarChar);
                    collection[12].Value = datosPortal.Estatus;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;

        }
    }
}



