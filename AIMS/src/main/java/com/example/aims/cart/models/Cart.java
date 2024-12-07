package com.example.aims.cart.models;
import com.example.aims.product.models.Product;
import java.util.List;

public class Cart {
    private List<Product> products;
    private double totalPrice;

    // Constructors, getters, and setters
    public Cart(List<Product> products) {
        this.products = products;
        this.totalPrice = calculateTotalPrice();
    }

    private double calculateTotalPrice() {
        return products.stream().mapToDouble(Product::getPrice).sum();
    }

    // Getters and Setters
    // ...
}
