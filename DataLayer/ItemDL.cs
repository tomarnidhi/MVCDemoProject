using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using BusinessLayer;

namespace DataLayer
{
    public class ItemDL
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mycon"].ConnectionString.ToString());
        public ItemDL()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        public List<ItemBL> ListAll()
        {
            List<ItemBL> lst = new List<ItemBL>();
            SqlConnection oConnection = con;
            SqlCommand oCommand = new SqlCommand("SP_GetAllItem", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlDataReader rdr = oCommand.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new ItemBL
                    {
                        ID = rdr.GetString(rdr.GetOrdinal("Id")),
                        Name = rdr.GetString(rdr.GetOrdinal("Name")),
                        Description = rdr.GetString(rdr.GetOrdinal("Description")),
                        Price = rdr.GetString(rdr.GetOrdinal("Price"))

                    });
                }
            }
            catch (Exception oException)
            {

                throw oException;
            }
            finally
            {
                oConnection.Close();
            }
            return lst;
            //}
        }

        public void InsertItem(string Name, string Desc, double Price)
        {
            SqlConnection oConnection = con;
            SqlCommand oCommand = new SqlCommand("SP_InsertItem", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            paraName.Value = Name;
            oCommand.Parameters.Add(paraName);
            SqlParameter paraDescription = new SqlParameter("@Desc", SqlDbType.NVarChar, 1000);
            paraDescription.Value = Desc;
            oCommand.Parameters.Add(paraDescription);
            SqlParameter paraPrice = new SqlParameter("@Price", SqlDbType.Decimal);
            paraPrice.Value = Price;
            oCommand.Parameters.Add(paraPrice);
            try
            {
                oCommand.ExecuteNonQuery();
            }
            catch (Exception oException)
            {

                throw oException;
            }
            finally
            {
                oConnection.Close();
            }
        }

        public void UpdateItem(string Name, string Desc, double Price,Guid Id)
        {
            SqlConnection oConnection = con;
            SqlCommand oCommand = new SqlCommand("SP_UpdateItem", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            paraName.Value = Name;
            oCommand.Parameters.Add(paraName);
            SqlParameter paraDescription = new SqlParameter("@Desc", SqlDbType.NVarChar, 1000);
            paraDescription.Value = Desc;
            oCommand.Parameters.Add(paraDescription);
            SqlParameter paraPrice = new SqlParameter("@Price", SqlDbType.Decimal);
            paraPrice.Value = Price;
            oCommand.Parameters.Add(paraPrice);
            SqlParameter paraId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            paraId.Value = Id;
            oCommand.Parameters.Add(paraId);
            try
            {
                oCommand.ExecuteNonQuery();
            }
            catch (Exception oException)
            {

                throw oException;
            }
            finally
            {
                oConnection.Close();
            }
        }

        public void DeleteItem(Guid Id)
        {
            SqlConnection oConnection = con;
            SqlCommand oCommand = new SqlCommand("SP_DeleteItem", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter paraId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            paraId.Value = Id;
            oCommand.Parameters.Add(paraId);
            try
            {
                oCommand.ExecuteNonQuery();
            }
            catch (Exception oException)
            {

                throw oException;
            }
            finally
            {
                oConnection.Close();
            }
        }

    }
}
