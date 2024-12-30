using System;using System.Data;using System.Data.SqlClient;using System.Data.SqlTypes;using ShoppingCart1;namespace ShoppingCart1{    public class Shopping    {        public static void Register()        {            Console.WriteLine("Register");            Console.WriteLine("Enter Name");            string name = Console.ReadLine();            Console.WriteLine("Enter UserName");            string Username = Console.ReadLine();            Console.WriteLine("Enter Password");            string password = Console.ReadLine();            Console.WriteLine("Enter ConfirmPassword");            string confirmpassword = Console.ReadLine();            Console.WriteLine("Enter Mobile Number");            string mobile = Console.ReadLine();            if (confirmpassword != password)
            {                Console.WriteLine("Enter correct password");            }            using (SqlConnection conn = new SqlConnection("server=.;integrated security=true;database=CDB"))
            {                string query = "insert into Registration (Name, Username, Password, ConfirmPassword, MobileNumber) VALUES (@Name, @Username, @Password, @ConfirmPassword, @Mobile)";                SqlCommand cmd = new SqlCommand(query, conn);                cmd.Parameters.AddWithValue("@Name", name);                cmd.Parameters.AddWithValue("@Username", Username);                cmd.Parameters.AddWithValue("@Password", password);                cmd.Parameters.AddWithValue("@ConfirmPassword", confirmpassword);                cmd.Parameters.AddWithValue("@Mobile", mobile);                conn.Open();                cmd.ExecuteNonQuery();                conn.Close();                Console.WriteLine("Registration Success.");            }        }        public static string Login()        {            Console.WriteLine("Enter Username");            string username = Console.ReadLine();            Console.WriteLine("Enter password");            string password = Console.ReadLine();            using (SqlConnection sconn = new SqlConnection("server=.;integrated security=true;database=CDB"))            {                string query = "EXEC ShoppingCart @ActionType='Login',@Username=@Username,@Password=@Password";                SqlCommand cmd = new SqlCommand(query, sconn);                cmd.Parameters.AddWithValue("@Username", username);                cmd.Parameters.AddWithValue("@Password", password);                sconn.Open();                var result = cmd.ExecuteScalar();                sconn.Close();                if (result.ToString() == "Login Successful")                    return username; 
                else                    return null; 
            }

        }
        public static void shoppingcart(string username)        {            int totalcost = 0;            int totalprice = 0;            while (true)            {                Console.WriteLine("ProductId\tProductName\tPrice\tQuantity");                string query = "select * from products";                using (SqlConnection scon = new SqlConnection("server=.;integrated security=true;database=CDB"))                {                    SqlCommand cmd = new SqlCommand(query, scon);                    scon.Open();                    SqlDataReader dr = cmd.ExecuteReader();                    if (dr.HasRows)                    {                        while (dr.Read())                        {                            Console.WriteLine($"{dr["ProductID"]}\t\t{dr["ProductName"]}\t\t{dr["Price"]}\t\t{dr["Quantity"]}");                        }                    }                    scon.Close();                }                Console.Write("Enter ProductID to add to cart: ");                int productId = Convert.ToInt32(Console.ReadLine());                using (SqlConnection scon = new SqlConnection("server=.;integrated security=true;database=CDB"))
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
                }                Console.Write("Enter Quantity: ");                int qty = Convert.ToInt32(Console.ReadLine());                using (SqlConnection scon = new SqlConnection("server=.;integrated security=true;database=CDB"))
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
                }                                using (SqlConnection scon = new SqlConnection("server=.;integrated security=true;database=CDB"))                {                    string squery = "select price from products where productid=@productid";                    SqlCommand cmd = new SqlCommand(squery, scon);                    cmd.Parameters.AddWithValue("@ProductID", productId);                    scon.Open();
                    //int pricee = cmd.ExecuteNonQuery();
                    int pricee = (int)cmd.ExecuteScalar();                    totalprice = pricee * qty;                    scon.Close();                }                using (SqlConnection conn = new SqlConnection("server=.;integrated security=true;database=CDB"))                {                    string addquery = "EXEC ShoppingCart @ActionType = 'AddToCart', @ProductID = @ProductID, @Quantity = @Quantity, @Username = @Username, @TotalPrice = @TotalPrice";                    SqlCommand cmd = new SqlCommand(addquery, conn);                
                    cmd.Parameters.AddWithValue("@ProductID", productId);                    cmd.Parameters.AddWithValue("@Quantity", qty);                    cmd.Parameters.AddWithValue("@Username", username);                    cmd.Parameters.AddWithValue("@TotalPrice", totalprice);                    conn.Open();                    try                    {                        cmd.ExecuteNonQuery();                        totalcost += totalprice;
                    }                    catch (SqlException ex)                    {                        Console.WriteLine("Error: " + ex.Message);                    }                    conn.Close();                }                Console.Write("Do you want to add more? (Yes/No): ");                string more = Console.ReadLine();                if (more.Equals("No", StringComparison.OrdinalIgnoreCase))                {                    break;                }            }            Console.WriteLine("\nYour Cart (Products added in this session):");            Console.WriteLine();            Console.WriteLine("CartID\tProductID\tProductName\tFinalPrice");            using (SqlConnection conn = new SqlConnection("server=.;integrated security=true;database=CDB"))            {                string cartQuery = "EXEC ShoppingCart @ActionType = 'GetCart', @Username = @Username";                SqlCommand cmd = new SqlCommand(cartQuery, conn);
                cmd.Parameters.AddWithValue("@Username", username);                conn.Open();                SqlDataReader dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["CartID"]}\t\t{dr["ProductID"]}\t\t{dr["ProductName"]}\t\t{dr["FinalPrice"]}");
                    //Console.WriteLine($"CartID: {dr["CartID"]}, ProductID: {dr["ProductID"]}, ProductName: {dr["ProductName"]}, FinalPrice: {dr["FinalPrice"]}");
                }
                conn.Close();            }
            Console.WriteLine($"Thanks for shopping with us! ");            Console.WriteLine($"Your total cost is: {totalcost}");        }    }

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