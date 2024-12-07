package com.example.aims.product.models;

public class Product {

    private int id;
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

    public Product(int id, String title, String category, double value, double price, String description, int quantity, String barcode, String condition, String dimensions, double weight) {
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

    public int getId() { return id; }
    public void setId(int id) { this.id = id; }
    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }
    public String getCategory() { return category; }
    public void setCategory(String category) { this.category = category; }
    public double getValue() { return value; }
    public void setValue(double value) { this.value = value; }
    public double getPrice() { return price; }
    public void setPrice(double price) { this.price = price; }
    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }
    public int getQuantity() { return quantity; }
    public void setQuantity(int quantity) { this.quantity = quantity; }
    public String getBarcode() { return barcode; }
    public void setBarcode(String barcode) { this.barcode = barcode; }
    public String getCondition() { return condition; }
    public void setCondition(String condition) { this.condition = condition; }
    public String getDimensions() { return dimensions; }
    public void setDimensions(String dimensions) { this.dimensions = dimensions; }
    public double getWeight() { return weight; }
    public void setWeight(double weight) { this.weight = weight; }
}