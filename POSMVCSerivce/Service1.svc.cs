using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace POSMVCSerivce
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Items GetItemDetails(String ItemId)
        {

            SqlConnection connection = new SqlConnection(@"Data Source=WINJN185117-Y81\SQLEXPRESS;Initial Catalog=POS;Integrated Security=True");
            SqlCommand command = new SqlCommand("GetItemDetails", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ItemId", ItemId);
            SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
            mySqlDataAdapter.SelectCommand = command;
            DataSet myDataSet = new DataSet();

            connection.Open();

            Items items = new Items();

            mySqlDataAdapter.Fill(myDataSet);

            connection.Close();
            if (myDataSet.Tables[0].Rows.Count == 0)
            {
                items = null;

                return items;
            }

            else
            {
                items.ItemId = myDataSet.Tables[0].Rows[0]["ItemId"].ToString();
                items.Name = myDataSet.Tables[0].Rows[0]["Name"].ToString();
                items.Price = decimal.Parse(myDataSet.Tables[0].Rows[0]["Price"].ToString());

                return items;

            }


        }
    }
}
