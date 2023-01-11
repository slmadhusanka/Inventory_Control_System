
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.IO;

namespace Inventory_Control_System
{
   

     class User_Cotrol
    {
         string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;


        

 
        public SqlDataReader User_Setting()
        {
            string LoginID = User_ID.UserID;

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();

            string Select_Details = @"SELECT      Auto_ID, User_ID, Add_New_User, Sk_New_Item, Sk_GRN, Sk_Change_Selling_Price, Sk_Barcode, In_Selling_Itm, In_Re_Print_Sell, In_Job, In_Re_Job, 
                         In_GRN_Credit, In_Customer_Credit, Re_New_Note, Re_Paymet_Add, Re_JOB_Pay_Add, Cus_add, Ven_Add, Acc_Petty_Cash, Acc_Set_Off, Acc_Bank, 
                         Rpt_Items_In_Stock, Rpt_Re_Order_Itms, Rpt_Sold_Inv, Rpt_Pay_Bal, Rpt_Profit_Lost, Rpt_Customer, Rpt_Vender, Rpt_Paymet_Details,User_Setting,
                         In_Return_O_Cancel, Acc_Bank_Acc_Create, Acc_DB_Backup, Rpt_Petty_cash, Rpt_Banking_Details, Rpt_Printing_Details,In_ManualChangePrice,Re_CreditPaymentBalanceRe
                         FROM   User_Settings WHERE User_ID='" + LoginID+"'";

            SqlCommand com = new SqlCommand(Select_Details, Conn);
            SqlDataReader dr = com.ExecuteReader();
            return dr;
        }
    }
}
