using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class PopularProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                                 "VALUES ('Coca-Cola Diet', 'Refrigerante de cola 350ml', 5.45, 'cocacola.jpg', 50, GETDATE(),1)"); 
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                                 "VALUES ('Cheeseburger', 'Pao, queijo, carne, salada, molho', 10.99, 'cheeseburger.jpg', 10, GETDATE(),2)"); 
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                                 "VALUES ('Pudim', 'pudim de leite condensado com calda 100g', 7.00, 'pudim.jpg', 15, GETDATE(),3)"); 
                                 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Produtos");

        }
    }
}
