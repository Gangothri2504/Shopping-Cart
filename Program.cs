using System;
            {
            {
                else
            }

        }
        public static void shoppingcart(string username)
                {
                    string validateQuery = "select count(*) from products where ProductID=@ProductID";
                    SqlCommand validateCmd = new SqlCommand(validateQuery, scon);
                    validateCmd.Parameters.AddWithValue("@ProductID", productId);
                    scon.Open();
                    int productExists = (int)validateCmd.ExecuteScalar();
                    scon.Close();

                    if (productExists == 0)
                    {
                        Console.WriteLine("Invalid ProductID. Please try again.");
                        continue; 
                    }
                }
                {
                    string checkQuery = "select Quantity from products where ProductID=@ProductID";
                    SqlCommand cmd = new SqlCommand(checkQuery, scon);
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    scon.Open();
                    int availableQty = (int)cmd.ExecuteScalar();
                    scon.Close();

                    if (qty > availableQty)
                    {
                        Console.WriteLine("Requested quantity exceeds available stock.\n Please try again.");
                        continue;
                    }
                }
                    //int pricee = cmd.ExecuteNonQuery();
                    int pricee = (int)cmd.ExecuteScalar();
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    }
                cmd.Parameters.AddWithValue("@Username", username);
                
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["CartID"]}\t\t{dr["ProductID"]}\t\t{dr["ProductName"]}\t\t{dr["FinalPrice"]}");
                    //Console.WriteLine($"CartID: {dr["CartID"]}, ProductID: {dr["ProductID"]}, ProductName: {dr["ProductName"]}, FinalPrice: {dr["FinalPrice"]}");
                }
                conn.Close();
            Console.WriteLine($"Thanks for shopping with us! ");

}



class Program : Shopping
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
            Console.WriteLine("Invalid login.\nPlease Enter Valid Credentials.");
        }
        Console.ReadLine();
    }
}