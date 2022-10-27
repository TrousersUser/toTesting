using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalVersionOfCourceWork
{
    public class TypeOfUser
    {
        /*Tasks to 'complete' the course work: 
          1) add EXIT buttons to all, except of IAP's, windows [In Proccess]
          2)Create sorting buttons to de-facto, all of the windows [In Proccess]
        */
        public static string User { get; set; }
        public static string CurrentNameOfSupplier { get; set; }
        public static string CurrentNameOfBuyer { get; set; }
        public static string LastPosition { get; set; }

        public static void RoadToTheHome()
        {
            switch (LastPosition)
            {
                case "Entering":
                    Entering _enter = new Entering();
                    _enter.Show();
                    break;
                case "RegistrationWindow":
                    MainWindow _main = new MainWindow();
                    _main.Show();
                    break;
                case "StorageWindow":
                    Storage _storage = new Storage();
                    _storage.Show();
                    break;
                case "ProductsWindow":
                    ProductsForSellingxaml _products = new ProductsForSellingxaml();
                    _products.Show();
                    break;
            }
        }  // Method for return_button's && Except of, placed inside of an IAP's window


        // ExistingTypesOfUsers : 1 - Administrator, 2 - Supplier, 3 - Buyer \\
        // AllPositions : 1) Entering, 2) RegistrationWindow, 3) StorageWindow, 4) ProductsWindow \\

    }
}
