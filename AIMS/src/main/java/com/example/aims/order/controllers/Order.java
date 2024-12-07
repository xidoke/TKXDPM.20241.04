package com.example.aims.order.controllers;
import com.example.aims.product.models.Product;
import java.util.List;

public class Order {
    private int id;
    private String customerName;
    private String email;
    private String phoneNumber;
    private String deliveryAddress;
    private List<Product> products;
    private double totalAmount;
    private String transactionId;
    private String orderStatus;
 /*   private DeliveryInfo deliveryInfo;
    private Payment payment; */

   /* // Constructors
    public Order(int id, String customerName, String email, String phoneNumber, String deliveryAddress, List<Product> products, double totalAmount, String transactionId, String orderStatus, DeliveryInfo deliveryInfo, Payment payment) {
        this.id = id;
        this.customerName = customerName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.deliveryAddress = deliveryAddress;
        this.products = products;
        this.totalAmount = totalAmount;
        this.transactionId = transactionId;
        this.orderStatus = orderStatus;
        this.deliveryInfo = deliveryInfo;
        this.payment = payment;
    }*/

    // Getters and Setters
    public int getId() { return id; }
    public void setId(int id) { this.id = id; }

    public String getCustomerName() { return customerName; }
    public void setCustomerName(String customerName) { this.customerName = customerName; }

    public String getEmail() { return email; }
    public void setEmail(String email) { this.email = email; }

    public String getPhoneNumber() { return phoneNumber; }
    public void setPhoneNumber(String phoneNumber) { this.phoneNumber = phoneNumber; }

    public String getDeliveryAddress() { return deliveryAddress; }
    public void setDeliveryAddress(String deliveryAddress) { this.deliveryAddress = deliveryAddress; }

    public List<Product> getProducts() { return products; }
    public void setProducts(List<Product> products) { this.products = products; }

    public double getTotalAmount() { return totalAmount; }
    public void setTotalAmount(double totalAmount) { this.totalAmount = totalAmount; }

    public String getTransactionId() { return transactionId; }
    public void setTransactionId(String transactionId) { this.transactionId = transactionId; }

    public String getOrderStatus() { return orderStatus; }
    public void setOrderStatus(String orderStatus) { this.orderStatus = orderStatus; }

 /*   public DeliveryInfo getDeliveryInfo() { return deliveryInfo; }
    public void setDeliveryInfo(DeliveryInfo deliveryInfo) { this.deliveryInfo = deliveryInfo; }

    public Payment getPayment() { return payment; }
    public void setPayment(Payment payment) { this.payment = payment; }*/


    public void calculateTotalAmount() {
        this.totalAmount = products.stream().mapToDouble(Product::getPrice).sum();
    }

    public void addProduct(Product product) {
        this.products.add(product);
        calculateTotalAmount(); // Recalculate total amount after adding a product
    }

    public void removeProduct(Product product) {
        this.products.remove(product);
        calculateTotalAmount();
    }

    public String displayOrderDetails() {
        StringBuilder details = new StringBuilder();
        details.append("Order ID: ").append(id).append("\n")
                .append("Customer Name: ").append(customerName).append("\n")
                .append("Email: ").append(email).append("\n")
                .append("Phone Number: ").append(phoneNumber).append("\n")
                .append("Delivery Address: ").append(deliveryAddress).append("\n")
                .append("Products: \n");
        for (Product product : products) {
            details.append(" - ").append(product.getTitle()).append(" (Price: ").append(product.getPrice()).append(")\n");
        }
        details.append("Total Amount: ").append(totalAmount).append("\n")
                .append("Transaction ID: ").append(transactionId).append("\n")
                .append("Order Status: ").append(orderStatus).append("\n");
        return details.toString();
    }


    public void updateOrderStatus(String newStatus) {
        this.orderStatus = newStatus;
    }

    public void confirmOrder() {
        this.orderStatus = "Confirmed";
    }

    public void cancelOrder() {
        this.orderStatus = "Cancelled";
    }

    public boolean isPending() {
        return "Pending".equalsIgnoreCase(orderStatus);
    }

    public boolean isCompleted() {
        return "Completed".equalsIgnoreCase(orderStatus);
    }

    public boolean isCancelled() {
        return "Cancelled".equalsIgnoreCase(orderStatus);
    }
}