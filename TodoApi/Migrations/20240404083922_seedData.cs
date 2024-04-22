using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PizzaSpecials",
                columns: new[] { "Id", "BasePrice", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 9.99m, "It's cheesy and delicious. Why wouldn't you want one?", "img/pizzas/cheese.jpg", "Basic Cheese Pizza" },
                    { 2, 11.99m, "It has EVERY kind of bacon", "img/pizzas/bacon.jpg", "The Baconatorizor" },
                    { 3, 10.50m, "It's the pizza you grew up with, but Blazing hot!", "img/pizzas/pepperoni.jpg", "Classic pepperoni" },
                    { 4, 12.75m, "Spicy chicken, hot sauce and bleu cheese, guaranteed to warm you up", "img/pizzas/meaty.jpg", "Buffalo chicken" },
                    { 5, 11.00m, "It has mushrooms. Isn't that obvious?", "img/pizzas/mushroom.jpg", "Mushroom Lovers" },
                    { 7, 11.50m, "It's like salad, but on a pizza", "img/pizzas/salad.jpg", "Veggie Delight" },
                    { 8, 9.99m, "Traditional Italian pizza with tomatoes and basil", "img/pizzas/margherita.jpg", "Margherita" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PizzaSpecials",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PizzaSpecials",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PizzaSpecials",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PizzaSpecials",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PizzaSpecials",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PizzaSpecials",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PizzaSpecials",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
