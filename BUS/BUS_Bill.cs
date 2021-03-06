﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BUS
{
   public class BUS_Bill
    {
        DAL_layer da = new DAL_layer();

        public DataTable getSpecBill(string id)
        {
            string sql = "SELECT * FROM Bill WHERE bill_id = '" + id + "'";
            return da.GetTable(sql);
        }
        public DataTable getAllBill()
        {
            string sql = "SELECT * FROM Bill";
            return da.GetTable(sql);
        }


        public void updateSpecBill(string adminID, string bill_ID,string ten,string sdt,string dc,string note,string pttt,string tt)
        {
            string sql = "UPDATE Bill SET admin_ID='"+adminID+"', username = N'"+ten+"', phone = '"+sdt+"', address = N'"+dc+"', note = N'"+note+"',payment_method = N'"+pttt+"', status_bill = '"+tt+"' WHERE bill_ID = '"+bill_ID+"'";
            da.ExcuteNonQuery(sql);
        }
        public void deleteBill(string id)
        {
            string sql_product_bill = "delete from Product_Bill where bill_ID = '"+id+"'";
            string sql_bill = "delete from bill where bill_ID = '"+id+"'";
            da.ExcuteNonQuery(sql_product_bill);
            da.ExcuteNonQuery(sql_bill);
        }

        public DataTable findBill(string keyword, string status, string timeStart, string timeEnd)
        {
            string sql = "select * from Bill where (bill_ID like N'%" + keyword + "%' or username like N'%" + keyword + "%' " +
                "or phone like N'%" + keyword + "%' " +
                "or admin_ID like N'%" + keyword + "%' " +
                "or address like N'%" + keyword + "%') ";
            if (status != "none") sql += " and status_bill='" + status + "'";
            if (timeStart != "none") sql += " and bill_date>='" + timeStart + "'";
            if (timeEnd != "none") sql += " and bill_date<='" + timeEnd + "'";
            return da.GetTable(sql);
        }

        public DataTable findProductBill(string billID)
        {
            string sql = "select Products.price, Products.product_ID, Products.product_name,Product_Bill.quanitity,Products.photo,ISNULL(Sale.min_product,0) as 'min_product', ISNULL(Sale.sale_price,0) as 'sale_price'" +
                "from Bill join Product_Bill on Bill.bill_ID = Product_Bill.bill_ID " +
                "join Products on Product_Bill.product_ID = Products.product_ID " +
                "left join Product_Sale on Product_Sale.product_ID = Products.product_ID " +
                "left join Sale on Product_Sale.sale_ID = Sale.sale_ID " +
                "where Bill.bill_ID = '"+billID+"'";
            return da.GetTable(sql);
        }

    }
}
