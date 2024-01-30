# DatabaseProject

**Summary: IT Equipment Wholesale Support System**.

1 **Introduction:**.
  The work focuses on the development of a comprehensive system supporting an IT hardware warehouse, based on robust technologies and a carefully designed database. Business processes, modules, as well as functional and non-functional requirements were described in detail, creating a solid foundation for further implementation.

2. **Technologies Used:**.
   - **Relational Database:** The relational database model, key assumptions, and application in storing persistent data are described.
   - ** SQL Language:** Structured Query Language as a tool for manipulating data in databases is presented.
   - **MS SQL Server:** A database management system, supported by Microsoft, using the Transact-SQL language is discussed.
   - **C# Language:** C# as an object-oriented programming language used in applications on the .NET Framework is presented.

3 **System Design:**.
   - **General Description:**.
     The functions of the system are presented, including data management, report generation, backup and data export.
   - **Requirements Analysis:**.
     - *Functional:* System functions such as assortment management, customer registration, transaction processing, and price management were identified.
     - *Non-functional:* Performance, security, compatibility, and usability requirements were identified.
   - **Module Descriptions:**.
     Key modules of the system, such as Employees, Merchandise, Contractors, Sales, and Warehouse, are presented, with descriptions of the functions and relationships between them.

4 **Action Concept and Logic Diagram:**.
   - **Workflow Diagrams:** The dynamic formation of activities within the enterprise is shown.
   - **Business Process Diagrams:** Include the processes of sales, orders, adding supplier, and receiving goods.

5. **Database Design:**.
   - **Conceptual Model:** An ERD diagram was used to model the main entities, attributes, and relationships between them.
   - **Physical Model:** A model tailored to a specific database engine is presented.
   - **Security and Access:** User roles, use case diagrams, and permission table were established.

6. **Database Realization**
  Tables in the database are described and sample procedures for the "address" and "contractors" tables are presented. 
  Example procedures include Select, Insert, Update, and Delete operations.
  **Example procedures for the "address" table:**
    - *usp_addressSelect*
        Parameter: @id_address
        Select from the "address" table based on id_address, or null if the parameter is null.
    - *usp_addressInsert*
        Parameters: @state, @city, @street, @postcode, @home_number, @apartment_number, @id_village
        Insert into the "address" table and return the added record.
    - *usp_addressUpdate*
        Parameters: @id_address, @state, @city, @street, @postcode, @nr_home, @nr_apartment, @id_village
        Update a record in the "address" table based on id_address and returns the updated record.
    - *usp_addressDelete*
        Parameter: @id_address
        Deletes a record from the "address" table based on id_address.