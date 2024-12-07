package com.example.aims.product.models;

public class Product {
    private String id;
    private String title;
    private String category;
    private double value;
    private double price;
    private String description;
    private int quantity;
    private String barcode;
    private String condition; // e.g., new, used
    private String dimensions;
    private double weight;

    public Product(String id, String title, String category, double value, double price, String description, int quantity, String barcode, String condition, String dimensions, double weight) {
        this.id = id;
        this.title = title;
        this.category = category;
        this.value = value;
        this.price = price;
        this.description = description;
        this.quantity = quantity;
        this.barcode = barcode;
        this.condition = condition;
        this.dimensions = dimensions;
        this.weight = weight;
    }

    public double getPrice() {
        return this.price;
    }
}
