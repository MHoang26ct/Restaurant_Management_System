# 🍽️ Restaurant Management System

> A powerful, free, and open-source solution to streamline your F&B business operations

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-WinForms-512BD4)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927)](https://www.microsoft.com/sql-server)

## Overview

The Restaurant Management System is a comprehensive desktop application designed to simplify restaurant operations. Built with C# WinForms and MS SQL Server, this solution provides an intuitive interface for managing every aspect of your restaurant—from orders and inventory to customer relationships and business analytics.

**Key Benefits:**
- Complete control in a single application
- Zero licensing costs—completely free
- Easy deployment on Windows devices
- Real-time business insights

---

## Features

### Core Functionality
- **Multi-Role Authentication System** — Secure login with role-based access control
- **Complete CRUD Operations** — Manage customers, tables, dishes, orders, and more
- **Business Analytics Dashboard** — Track total visits, revenue, reservations, and key performance metrics
- **Order Management** — Streamlined order creation, modification, and tracking
- **Inventory Control** — Monitor stock levels and menu items

### Technical Highlights
- Optimized for Windows environments
- Responsive WinForms UI
- Robust SQL Server backend
- Scalable architecture for growing businesses

---

## Getting Started

### Prerequisites

Ensure your system meets the following requirements:

| Component | Requirement |
|-----------|-------------|
| **Operating System** | Windows 7 or later |
| **Development IDE** | [Visual Studio 2022 Community](https://visualstudio.microsoft.com/downloads/) (or newer) |
| **Database Engine** | [SQL Server 2022 Express](https://www.microsoft.com/sql-server/sql-server-downloads) (or newer) |
| **Database Tools** | [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) |

### Installation Guide

#### Step 1: Database Setup

**Option A: Local Server (Recommended for testing)**
1. Launch SQL Server Management Studio (SSMS)
2. Connect using Windows Authentication
3. Create a new database named `RestaurantManagementDB`

**Option B: Remote Server (For production deployments)**
1. Set up SQL Server with SQL Server Authentication enabled
   - Follow this [configuration guide](https://www.youtube.com/watch?v=fqLR-1Kngj0)
2. Configure Windows Firewall rules
   - Enable TCP port 1433 (or your custom port)
3. Set up port forwarding on your router
   - Reference: [Port forwarding tutorial](https://www.youtube.com/watch?v=zoFcqt9Yufw)
4. Note your server's public IP address and credentials

<<<<<<< HEAD
=======
> [!NOTE]
> For a stable remote connection, consider [setting up a static IP address](https://youtu.be/-G3ePnXAoHc?si=ATj-qD86jkQih7Ox) and [registering a DDNS (Dynamic DNS) service](https://youtu.be/Qbad9pYSVwg?si=2_E7ZEwJxPA3__8j). This ensures your server remains accessible even if your public IP changes

>>>>>>> features
#### Step 1.5: Initialize Database Structure

After creating the database, you need to build its structure by executing SQL scripts:

1. In SSMS, connect to your SQL Server instance
2. Select the `RestaurantManagementDB` database
3. Execute the SQL scripts in the following order:

   **a) Create Tables:**
   - Open `Database/Schema.sql`
   - Click **Execute** (or press F5) to create all tables and relationships

   **b) Create Stored Procedures:**
   - Open `Database/StoredProcedure.sql`
   - Click **Execute** to create all stored procedures

   **c) Create Triggers:**
   - Open `Database/Trigger.sql`
   - Click **Execute** to create all database triggers

> **💡 Tip:** Execute each script one at a time and verify there are no errors before proceeding to the next one. Check the Messages tab in SSMS for confirmation.

#### Step 2: Clone the Repository

#### Step 3: Configure Connection String

1. In Visual Studio, add an **App.config** file to your project (if not already present)
2. Replace the contents with the appropriate configuration:

**For Local Server:**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="DefaultConnection"
         connectionString="Server=localhost;Database=RestaurantManagementDB;Integrated Security=True;TrustServerCertificate=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

**For Remote Server:**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="DefaultConnection"
         connectionString="Server=YOUR_PUBLIC_IP,1433;Database=RestaurantManagementDB;User Id=YOUR_SQL_USERNAME;Password=YOUR_SQL_PASSWORD;TrustServerCertificate=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

> [!NOTE]
> - `YOUR_PUBLIC_IP` — Your server's public IP address
> - `1433` — Your SQL Server port (default is 1433)
> - `YOUR_SQL_USERNAME` — Your SQL authentication username
> - `YOUR_SQL_PASSWORD` — Your SQL authentication password
>   
> ⚠️ **Ensure no spaces exist between parameters. Example: `Server=192.168.1.100,1433`**

#### Step 4: Build and Run

1. Open the solution in Visual Studio
2. Restore NuGet packages (if prompted)
3. Press **F5** or click the **Start** button to build and launch the application
4. Log in with your credentials and start managing your restaurant!

---

## 🤝 Contributing

We welcome contributions from the community! Here's how you can help:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Development Guidelines
- Follow C# coding conventions
- Write clear commit messages
- Add comments for complex logic
- Test thoroughly before submitting PRs

---

## Support & Troubleshooting

### Common Issues

**Connection Timeout Error**
- Verify SQL Server is running
- Check firewall settings
- Confirm connection string accuracy

**Authentication Failed**
- Ensure SQL Server Authentication is enabled (for remote servers)
- Verify username and password
- Check user permissions in SQL Server

**Application Won't Start**
- Verify all prerequisites are installed
- Check .NET Framework version compatibility
- Review Visual Studio error logs

### Need Help?

If you encounter any issues or have questions:

**Email:** mhoang26ct@gmail.com

**Bug Reports:** [Create an issue](https://github.com/MHoang26ct/Restaurant_Management_System/issues)

We aim to respond to all inquiries within 24-48 hours.

---

## Team

This project is developed and maintained by the HHH Team:

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/MHoang26ct">
        <img src="https://github.com/MHoang26ct.png" width="100px;" alt="Mai Minh Hoàng"/><br />
        <sub><b>Mai Minh Hoàng</b></sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/Haibrosh">
        <img src="https://github.com/Haibrosh.png" width="100px;" alt="Nguyễn Hoàn Hải"/><br />
        <sub><b>Nguyễn Hoàn Hải</b></sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/hoanghaoz">
        <img src="https://github.com/hoanghaoz.png" width="100px;" alt="Nguyễn Lê Hoàng Hảo"/><br />
        <sub><b>Nguyễn Lê Hoàng Hảo</b></sub>
      </a>
    </td>
  </tr>
</table>

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🌟 Acknowledgments

Special thanks to:
- The open-source community for inspiration
- Microsoft for .NET and SQL Server tools
- All contributors and users of this project

---

<div align="center">

**Made with ❤️ by the HHH Team**

[⭐ Star this repo](https://github.com/MHoang26ct/Restaurant_Management_System) | [🐛 Report Bug](https://github.com/MHoang26ct/Restaurant_Management_System/issues) | [💡 Request Feature](https://github.com/MHoang26ct/Restaurant_Management_System/issues)

</div>
