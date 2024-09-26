namespace ComarchTestExplorer.Data.Model
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        public decimal NetValue { get; set; }

        public decimal Tax { get; set; }

        public decimal GrossValue { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }
    }
}