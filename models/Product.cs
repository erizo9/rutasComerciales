namespace rutasComerciales
{
    class Product
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public int StockQuantity { get; set; }
        public bool Available { get; set; }
        public DateTime LastUpdate { get; set; }

        public void ViewProduct()
        {
            Console.WriteLine($"{Name} - Precio: {Price:C} - Stock: {StockQuantity}");
        }
    }
}
