namespace controleEstoque.Models
{
    public class ResponseModel
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string categoria { get; set; } = string.Empty;
        public decimal preco { get; set; }
        public int quantidade { get; set; }
    }
}
