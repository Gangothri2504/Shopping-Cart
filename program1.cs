﻿using System;
            {
            {
                //string query = "insert into Registration (Name, Username, Password, ConfirmPassword, MobileNumber) VALUES (@Name, @Username, @Password, @ConfirmPassword, @Mobile)";
                //SqlCommand cmd = new SqlCommand(query, conn);
                SqlCommand cmd = new SqlCommand("ShoppingCart1", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "Register");
                else
            }

        }
        public static void shoppingcart(string username)
                    SqlCommand cmd = new SqlCommand("ShoppingCart1", scon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "DisplayProduct");
                    //SqlCommand cmd = new SqlCommand(query, scon);
                    scon.Open();
                {
                    //string checkQuery = "select Quantity from products where ProductID=@ProductID";
                    //SqlCommand cmd = new SqlCommand(checkQuery, scon);
                    SqlCommand cmd = new SqlCommand("ShoppingCart1", scon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "QuantityCheck");
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@ProductName", productname);
                    scon.Open();
                    var availableQty = cmd.ExecuteScalar();
                    scon.Close();

                    if(availableQty.ToString() == "Insufficient Stock")
                    {
                        Console.WriteLine("Requested quantity exceeds available stock. Please try again");
                    }

                    //if (qty > availableQty)
                    //{
                    //    Console.WriteLine("Requested quantity exceeds available stock. Please try again");
                    //    continue;
                    //}
                    //if (availableQty == 1)
                    //{
                    //    Console.WriteLine("not chjk");
                    //    continue;
                    //}
                    //if (availableQty == 2)
                    //{
                    //    Console.WriteLine("Enter Correct quantity.");
                    //    continue;
                    //}

                    //if (availableQty == 3)
                    //{
                    //    Console.WriteLine("Requested quantity exceeds available stock. Please try again.");
                    //    continue;
                    //}
                }

                using (SqlConnection scon = new SqlConnection("server=.;integrated security=true;database=CDB"))
                    //string squery = "select price from products where productid=@productid";
                    //SqlCommand cmd = new SqlCommand(squery, scon);
                    SqlCommand cmd = new SqlCommand("ShoppingCart1", scon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "PriceCalc");
                    //int pricee = cmd.ExecuteNonQuery();
                    int pricee = (int)cmd.ExecuteScalar();
                    //string addquery = "EXEC ShoppingCart @ActionType = 'AddToCart', @ProductID = @ProductID,@Productname = @Productname, @Quantity = @Quantity, @Username = @Username, @TotalPrice = @TotalPrice";
                    //SqlCommand cmd = new SqlCommand(addquery, conn);
                    SqlCommand cmd = new SqlCommand("ShoppingCart1", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "AddToCart");  
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    }
                SqlCommand cmd = new SqlCommand("ShoppingCart1", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "GetCart");
                cmd.Parameters.AddWithValue("@Username", username);

                while (dr.Read())
                {
                    Console.WriteLine($"{dr["CartID"]}\t\t{dr["ProductID"]}\t\t{dr["ProductName"]}\t\t{dr["FinalPrice"]}");
                    //Console.WriteLine($"CartID: {dr["CartID"]}, ProductID: {dr["ProductID"]}, ProductName: {dr["ProductName"]}, FinalPrice: {dr["FinalPrice"]}");
                }
                conn.Close();
            Console.WriteLine($"Thanks for shopping with us! ");

}



class Program1 : Shopping1
{
    SqlConnection sqlconn = new SqlConnection("server =.;integrated security = true;database=CDB");
    static void Main(string[] args)
    {
        Console.WriteLine("Do you have credentials to Login? (Yes/No): ");
        string YesNo = Console.ReadLine();
        while (!YesNo.Equals("Yes", StringComparison.OrdinalIgnoreCase) &&
               !YesNo.Equals("No", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Please enter 'Yes' or 'No'.");
            YesNo = Console.ReadLine();
        }
        if (YesNo.Equals("No", StringComparison.OrdinalIgnoreCase))
        {
            Register();
        }
        string loginn = Login();
        if (loginn != null)
        {
            shoppingcart(loginn);
        }
        else
        {
            Console.WriteLine("Invalid login.\nExiting.");
        }
        Console.ReadLine();
    }
}