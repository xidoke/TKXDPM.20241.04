package com.example.aims.cart.models;
import com.example.aims.product.models.Product;
import java.util.List;

public class Cart
{

    private int id;
    private List<Product> products;
    private double totalPrice;

    public Cart(int id, List<Product> products) {
        this.id = id;
        this.products = products;
        this.totalPrice = calculateTotalPrice();
    }

    private double calculateTotalPrice() {
        return products.stream().mapToDouble(Product::getPrice).sum();
    }
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }
    public List<Product> getProducts() { return products; }
    public void setProducts(List<Product> products)
    {
        this.products = products;
        this.totalPrice = calculateTotalPrice(); // Recalculate total price when products change
    }
    public double getTotalPrice() { return totalPrice; }
}
