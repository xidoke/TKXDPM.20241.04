# AIMS Project - TKXDPM.20241.04

This README provides an overview of the AIMS project, its technologies, setup instructions, development model, and team member contributions.

## 1. Project Overview

This project, named AIMS, is developed as part of the TKXDPM course in the 20241 semester. It appears to be an e-commerce platform for media products, with features such as browsing, searching, adding to cart, placing orders, and making payments.

## 2. Technologies

The project utilizes the following technologies:

**Client-Side:**

-   HTML
-   CSS
-   JavaScript

**Server-Side:**

-   **Database:** PostgreSQL (via Supabase)
-   **Backend Framework:** ASP.NET Core 8 MVC (C#)
-   **Payment Gateway:** VnPay

## 3. Development Environment Setup

**Prerequisites:**

-   **IDE:** Visual Studio Community 2022
-   **Workload:**  ASP.NET and web development (see installation instructions below)

**Installation Steps:**

1. **Install Visual Studio Community 2022:** Download and install from the official Microsoft website.
2. **Install ASP.NET and web development workload:**
    -   During installation, select the "ASP.NET and web development" workload.
    -   Alternatively, if Visual Studio is already installed, open the Visual Studio Installer, click "Modify" on your Visual Studio Community 2022 installation, and then select the "ASP.NET and web development" workload.
    -   ![ASP.NET and web development workload](https://github.com/user-attachments/assets/549863cb-1b2e-4fe8-8190-1c414b7c95eb)
3. **Clone the Project** : `git clone [<repository_url>](https://github.com/xidoke/TKXDPM.20241.04.git)` 
4. **Open the Project:** Open the `AIMS.csproj` file in Visual Studio.
5. **Database Setup:**
    -   Ensure you have a Supabase account and project set up.
    -   Configure the connection string in project's `Program.cs` or equivalent configuration file to point to your Supabase PostgreSQL database.
6. **VnPay Setup:**
   - Configure your VnPay account details, and secret key in your project's `appsettings.json` or equivalent configuration file.
7. **Build and Run:** Build the project and run it directly using your device's host.

## 4. Development Model

The project follows the **Model-View-Controller (MVC)** architectural pattern.

## 5. Team Member Contributions

| Team Member          | Student ID | Responsibilities                                                                                                                                                                                                                                                                         |
| --------------------- | -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Lê Hà Anh Đức         | 20215351 | - Project setup<br>- Designed and implemented Views: Home, Cart, PlaceOrder, Payment, SearchMediaResult, MediaDetails<br>- Designed and created Entities<br>- Designed and implemented Model Interfaces<br>- Integrated VnPay payment gateway<br>- Designed Graphic Interface documents for order placement use case<br>- Designed Subsystem Interface documents for VnPay<br>- Set up database interaction environment |
| Phạm Đình Đô         | 20200154 | - Designed and implemented the database<br>- Designed Class Design documents<br>- Designed Data Modeling documents                                                                                                                                                                  |
| Lê Văn Tuấn Đạt        | 20215341 | - Designed and implemented Order functionalities<br>- Managed project structure                                                                                                                                                                                                              |
| Phạm Quốc Đạt        | 20215345 | - Prepared project report<br>- Designed and implemented Views: Login, Register<br>- Designed and implemented Controllers for Login, Register<br>- Designed and implemented Unit Tests for: Login, Register, PlaceOrder                                                                     |
| Đặng Đình Diệp        | 20183495 | - Designed Concept Design documents<br>- Designed Use Case and Sequence Diagrams for order placement use case                                                                                                                                                                          |
